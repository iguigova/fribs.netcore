using fribs.netcore.api.clients.ss;
using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    [TestClass]
    public class Tests  // https://pdsa.com/BlogPosts/04-UnitTesting-Attributes.pdf, https://pdsa.com/BlogPosts/03-UnitTesting-InitCleanup.pdf, https://blog.fairwaytech.com/add-attributes-to-unit-tests
    {
        protected const string TESTCATEGORY_API = "API";

        protected IClient _client;

        protected const string BASE_URL = "base_url";
        protected const string GATEWAY_ID = "gateway_id";
        protected const string GATEWAY_SECRET = "gateway_secret";
        protected const string GOOGLE_SANDBOX_ID = "google_sandbox_id";
        protected const string EMAIL_ADDRESS = "email_address";
        protected const string EMAIL_ADDRESS2 = "email_address2";
        protected const string PASSWORD = "password";

        protected const string SESSION_TOKEN = "session_token";
        protected const string SESSION_TOKEN_HEADER = "x-session-token";

        protected const string CURRENT_USER = "current_user";

        protected const string CLEANUP_ACTIONS = "cleanup_actions";

        public Tests()
        {
            _client = new fribs.netcore.api.clients.ss.JsonHttpClient("https://manage.logon-dev.com" /*this[BASE_URL]*/, OnException);
        }

        [AssemblyInitialize]
        public static void Config(TestContext context)
        {
            context.Properties[BASE_URL] = "https://manage.logon-dev.com";

            context.Properties[GATEWAY_ID] = "<GATEWAY_ID>";
            context.Properties[GATEWAY_SECRET] = "<GATEWAY_SECRET>";
            context.Properties[GOOGLE_SANDBOX_ID] = "<GOOGLE_SANDBOX_ID>";
            context.Properties[EMAIL_ADDRESS] = "<EMAIL_ADDRESS>";
            context.Properties[EMAIL_ADDRESS2] = "<EMAIL_ADDRESS2>";
            context.Properties[PASSWORD] = "<PASSWORD>";

            context.Properties[CLEANUP_ACTIONS] = new Stack<Action>(); // LIFO
        }

        [TestInitialize]
        public void Init()
        {
            TestInitialize();
        }

        protected virtual void TestInitialize()
        {
            Config(TestContext);

            TestContext.WriteLine(TestContext.Properties.SerializeToString());

            Authenticate();
        }

        public virtual void Authenticate(string emailAddress = null, string password = null)
        {
            emailAddress ??= this[EMAIL_ADDRESS];
            password ??= this[PASSWORD];

            if (!string.IsNullOrEmpty(_currentUser.EmailAddress) && _currentUser.EmailAddress != emailAddress)
            {
                Logout();
            }

            if (this[SESSION_TOKEN] == null)
            {
                this[SESSION_TOKEN] = _client.Post(new AuthenticateUser() 
                { 
                    email_address = emailAddress, 
                    password = password
                }).session_token;

                _client.Headers.Add(SESSION_TOKEN_HEADER, this[SESSION_TOKEN]);

                _currentUser = (emailAddress, password);
            }
        }

        protected virtual void SetBasicAuthCredentials(string emailAddress = null, string password = null)
        {
            Logout();

            _client.SetCredentials(emailAddress ?? this[EMAIL_ADDRESS], password ?? this[PASSWORD]);

            _currentUser = (emailAddress, password);
        }

        protected (string EmailAddress, string Password) _currentUser
        {
            get { return TestContext.Properties.Contains(CURRENT_USER) ? (ValueTuple<string, string>)TestContext.Properties[CURRENT_USER] : (null, null); }
            set { TestContext.Properties[CURRENT_USER] = value; }
        }

        protected virtual void Logout()
        {
            _client.SetCredentials(null, null);

            if (this[SESSION_TOKEN] != null)
            {
                _client.Delete(new Logout());

                _client.Headers.Remove(SESSION_TOKEN_HEADER);

                this[SESSION_TOKEN] = null;
            }

            _currentUser = (null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            var cleanupactions = (TestContext.Properties[CLEANUP_ACTIONS] as Stack<Action>);

            while (cleanupactions?.Count > 0)
            {
                cleanupactions.Pop().Invoke();
            }

            Logout();
        }

        public TestContext TestContext { get; set; }  // https://pdsa.com/BlogPosts/02-UnitTesting-Configuration.pdf

        public string this[string name]
        {
            get
            {
                return (TestContext.Properties.Contains(name)) ? TestContext.Properties[name]?.ToString() : null;
            }
            set
            {
                if (TestContext.Properties.Contains(name))
                {
                    TestContext.Properties[name] = value;
                }
                else
                {
                    TestContext.Properties.Add(name, value);
                }
            }
        }

        protected virtual string namestamp { get { return $"{TestContext.TestName.Split('_')[0]}_{DateTime.UtcNow.Ticks}.com"; } }

        protected virtual void Cleanup(Action action)
        {
            var emailAddress = _currentUser.EmailAddress;
            var password = _currentUser.Password;

            (TestContext.Properties[CLEANUP_ACTIONS] as Stack<Action>).Push(() => 
            {
                SetBasicAuthCredentials(emailAddress, password);  // encloses current emailAddress, password
                action?.Invoke();
            });
        }

        protected const string VALIDATION_ERROR = "validation_error";
        protected const string API_ERROR = "api_error";
        protected const string UNAUTHORIZED_ERROR = "unauthorized_error";

        protected virtual Action<WebServiceException> Is(HttpStatusCode statusCode, string errorCode = null)
        {
            return (ex) =>
            {
                OnException(ex);

                Assert.That.For(ex).IsTrue(x => x.StatusCode == (int)statusCode);
                Assert.That.For(ex).IsTrue(x => x.ResponseBody.Contains(errorCode ?? "_error"));
            };
        }

        protected virtual void OnException(Exception ex)
        {
            TestContext.WriteLine($"{ex.Message} {ex.Data.SerializeToString()}");

            //Assert.Inconclusive($"{ex.Message} - {string.Join(",", ex.Data.Values)}");
        }
    }
}

using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    [TestClass]
    public class Profile : AppTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetProfile")]
        
        [Description("Call GetProfile with a valid session, assert profile of the logged in user is returned")]
        public void Profile_ValidRequest_ReturnsProfile()
        {
            var response = _client.Get(new GetProfile());

            Assert.IsNotNull(response.email_address);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetProfile")]

        [Description("Call GetProfile without authentication, assert 401 UnAuthorized is returned")]
        public void Profile_Public_Returns401() 
        {
            Logout();

            Assert.IsNull(_client.Get(new GetProfile(), Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetProfile")]

        [Description("Call GetProfile with basic auth, assert the profile of the logged in user is returned")]
        public void Profile_BasicAuth_ReturnsUserInfo() 
        {
            SetBasicAuthCredentials();

            Profile_ValidRequest_ReturnsProfile();
        }
    }
}

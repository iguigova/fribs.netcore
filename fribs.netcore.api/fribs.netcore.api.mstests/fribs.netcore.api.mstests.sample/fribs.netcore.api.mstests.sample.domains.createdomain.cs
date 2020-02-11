using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class DomainTests
    {
        protected const string DOMAIN_ID = "domain_id";
        protected const string DOMAIN = "domain";

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateDomain")]

        [Description("Call CreateDomain with a valid session and app_id and domain, assert a domain id is returned.")]
        public void CreateDomain_ValidRequest_ReturnsDomainId()
        {
            var domain = namestamp;

            var domain_id = _client.Post(new CreateDomain() { app_id = this[APP_ID], domain = domain }).domain_id;

            this[DOMAIN_ID] = domain_id;
            this[DOMAIN] = domain;

            //Cleanup(() => _client.Delete(new RemoveDomain() { domain_id = domain_id })); // Handled by cleanup - RemoveApp - in base class

            Assert.IsNotNull(domain_id);
        }

        [Description("Call GetDomain with basic auth and valid parameters, assert domain_id and domain are returned.")]
        public void CreateDomain_BasicAuth_ReturnsDomainId()
        {
            SetBasicAuthCredentials();

            CreateDomain_ValidRequest_ReturnsDomainId();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateDomain")]

        [Description("Call CreateDomain without authentication, assert 401 UnAuthorized is returned.")]
        public void CreateDomain_Public_Returns401()
        {
            Logout();

            Assert.IsNull(_client.Post(new CreateDomain() { app_id = this[APP_ID], domain = namestamp }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateDomain")]

        [Description("Call CreateDomain without valid authorization, assert 404 NotFound is returned.")]
        public void CreateDomain_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Post(new CreateDomain() { app_id = this[APP_ID], domain = namestamp }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateDomain")]

        [Description("Call CreateDomain with a valid session but invalid domain name, assert 400 Bad Request is returned.")]
        public void CreateDomain_InvalidDomainName_Returns400()
        {
            Assert.IsNull(_client.Post(new CreateDomain() { app_id = this[APP_ID], domain = "invalid" }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }        

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateDomain")]

        [Description("Call CreateDomain with a valid session but missing domain name, assert 400 Bad Request is returned.")]
        public void CreateDomain_MissingDomainName_Returns400()
        {
            Assert.IsNull(_client.Post(new CreateDomain() { app_id = this[APP_ID] }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }
    }
}

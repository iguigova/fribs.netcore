using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class DomainTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveDomain")]

        [Description("Call RemoveDomain with a valid session and valid domain id, assert 200 is returned.")]
        public void RemoveDomain_ValidRequest_Returns200()
        {
            var response = _client.Delete(new RemoveDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] });

            Assert.IsNotNull(response);
            Assert.IsNull(_client.Get(new GetDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));

        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveDomain")]

        [Description("Call RemoveDomain with basic auth and valid doamin id, assert 200 is returned.")]
        public void RemoveDomain_BasicAuth_Returns200()
        {
            SetBasicAuthCredentials();

            RemoveDomain_ValidRequest_Returns200();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveDomain")]

        [Description("Call RemoveDomain without authentication, assert 401 is returned.")]
        public void RemoveDomain_Public_Returns401()
        {
            Logout();

            Assert.IsNull(_client.Delete(new RemoveDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveDomain")]

        [Description("Call RemoveDomain without authorization, assert 404 is returned.")]
        public void RemoveDomain_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Delete(new RemoveDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveDomain")]

        [Description("Call RemoveDomain with a domain id that is not a valid guid, assert 400 is returned.")]
        public void RemoveDomain_InvalidDomainId_Returns400()
        {
            Assert.IsNull(_client.Delete(new RemoveDomain() { app_id = this[APP_ID], domain_id = "test" }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }
    }
}

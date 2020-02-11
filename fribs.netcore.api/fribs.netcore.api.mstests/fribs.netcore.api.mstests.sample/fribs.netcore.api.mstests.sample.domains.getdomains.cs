using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class DomainTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomains")]

        [Description("Call GetDomains with a valid session and one domain, assert domain is returned.")]
        public void GetDomains_ValidRequest_ReturnsDomains()
        {
            var response =_client.Get(new GetDomains() { app_id = this[APP_ID] });

            Assert.That.For(response.results).IsTrue(s => s.Count > 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomains")]

        [Description("Call GetDomains with a valid session and no domains, assert empty list is returned.")]
        public void GetDomains_ValidRequestNoDomains_ReturnEmptyList_SkipCreateDomainOnTestInit()
        {
            var response = _client.Get(new GetDomains() { app_id = this[APP_ID] });

            Assert.That.For(response.results).IsTrue(s => s.Count == 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomains")]

        [Description("Call GetDomains with basic auth and valid parameters, assert domain is returned.")]
        public void GetDomains_BasicAuth_ReturnsDomains()
        {
            SetBasicAuthCredentials();

            GetDomains_ValidRequest_ReturnsDomains();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomains")]

        [Description("Call GetDomains without authentication, assert 401 UnAuthorized is returned.")]
        public void GetDomains_Public_Return401()
        {
            Logout();

            Assert.IsNull(_client.Get(new GetDomains() { app_id = this[APP_ID] }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomains")]

        [Description("Call GetDomains without authorization, assert 404 NotFound is returned.")]
        public void GetDomains_NoAccess_Return404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Get(new GetDomains() { app_id = this[APP_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomains")]

        [Description("Call GetDomains with invalid page number, assert 400 BadRequest is returned.")]
        public void GetDomains_InvalidPageNumber_Return400()
        {
            Assert.IsNull(_client.Get(new GetDomains() { app_id = this[APP_ID], page = -10 }, Is(HttpStatusCode.BadRequest, API_ERROR)));
        }
    }
}

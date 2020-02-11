using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class RestrictionTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions with a valid session and one restriction, assert restriction is returned.")]
        public void GetRestrictions_ValidRequest_ReturnsRestriction()
        {
            var response =_client.Get(new GetRestrictions() { app_id = this[APP_ID] });

            Assert.That.For(response.results).IsTrue(s => s.Count > 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions with a valid session and one restriction for a specific domain, assert restriction is returned.")]
        public void GetRestrictions_DomainSpecific_ReturnsRestriction()
        {            
            var response = _client.Get(new GetRestrictions() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] });

            Assert.That.For(response.results).IsTrue(s => s.Count > 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions with a valid session and no restrictions, assert empty list is returned.")]
        public void GetRestrictions_ValidRequestNoDomains_ReturnEmptyList_SkipCreateRestrictionOnTestInit()
        {
            var response = _client.Get(new GetRestrictions() { app_id = this[APP_ID] });

            Assert.That.For(response.results).IsTrue(s => s.Count == 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions with basic auth and valid parameters, assert domain is returned.")]
        public void GetRestrictions_BasicAuth_ReturnsDomains()
        {
            SetBasicAuthCredentials();

            GetRestrictions_ValidRequest_ReturnsRestriction();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions without authentication, assert 401 UnAuthorized is returned.")]
        public void GetRestrictions_Public_Return401()
        {
            Logout();

            Assert.IsNull(_client.Get(new GetRestrictions() { app_id = this[APP_ID] }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions without authorization, assert 404 NotFound is returned.")]
        public void GetRestrictions_NoAccess_Return404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Get(new GetRestrictions() { app_id = this[APP_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetRestrictions")]

        [Description("Call GetRestrictions with invalid page number, assert 400 BadRequest is returned.")]
        public void GetRestrictions_InvalidPageNumber_Return400()
        {
            Assert.IsNull(_client.Get(new GetRestrictions() { app_id = this[APP_ID], page = -10 }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }
    }
}

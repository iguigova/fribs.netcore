using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class RestrictionTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestriction")]

        [Description("Call RemoveRestriction with a valid session and valid domain id, assert 200 is returned.")]
        public void RemoveRestriction_ValidRequest_Returns200()
        {
            var response = _client.Delete(new RemoveRestriction() { app_id = this[APP_ID], restriction_id = this[RESTRICTION_ID] });

            Assert.IsNotNull(response);
            Assert.That.For(_client.Get(new GetRestrictions() { app_id = this[APP_ID] })).IsTrue(s => s.results.Count == 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestriction")]

        [Description("Call RemoveRestriction with a valid session and valid domain id, assert 200 is returned.")]
        public void RemoveRestriction_DomainSpecific_Returns200()
        {
            var response = _client.Delete(new RemoveRestriction() { app_id = this[APP_ID], restriction_id = this[DOMAIN_RESTRICTION_ID] });

            Assert.IsNotNull(response);
            Assert.That.For(_client.Get(new GetRestrictions() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] })).IsTrue(s => s.results.Count == 0);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestriction")]

        [Description("Call RemoveRestriction with basic auth and valid doamin id, assert 200 is returned.")]
        public void RemoveRestriction_BasicAuth_Returns200()
        {
            SetBasicAuthCredentials();

            RemoveRestriction_ValidRequest_Returns200();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestriction")]

        [Description("Call RemoveRestriction without authentication, assert 401 is returned.")]
        public void RemoveRestriction_Public_Returns401()
        {
            Logout();

            Assert.IsNull(_client.Delete(new RemoveRestriction() { app_id = this[APP_ID], restriction_id = this[RESTRICTION_ID] }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestriction")]

        [Description("Call RemoveRestriction without authorization, assert 404 is returned.")]
        public void RemoveRestriction_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Delete(new RemoveRestriction() { app_id = this[APP_ID], restriction_id = this[RESTRICTION_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestriction")]

        [Description("Call RemoveRestriction with a domain id that is not a valid guid, assert 400 is returned.")]
        public void RemoveRestriction_InvalidDomainId_Returns400()
        {
            Assert.IsNull(_client.Delete(new RemoveRestriction() { app_id = this[APP_ID], restriction_id = "test" }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }
    }
}

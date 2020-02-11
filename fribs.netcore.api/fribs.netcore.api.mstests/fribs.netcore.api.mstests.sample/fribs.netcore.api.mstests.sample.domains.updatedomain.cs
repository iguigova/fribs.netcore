using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class DomainTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with a valid session and no parameters, assert response is returned.")]
        public void UpdateDomain_ValidRequest_ReturnsResponse()
        {
            Assert.IsNotNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID]}));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with basic auth and no parameters, assert response is returned.")]
        public void UpdateDomain_BasicAuth_ReturnsDomain()
        {
            SetBasicAuthCredentials();

            UpdateDomain_ValidRequest_ReturnsResponse();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain without authentication, assert 401 is returned.")]
        public void UpdateDomain_Public_Returns401()
        {
            Logout();

            Assert.IsNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call GetDomain without valid authorization, assert 404 is returned.")]
        public void UpdateDomain_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with a valid session and valid parameters, update domain name, assert domain name is updated.")]
        public void UpdateDomain_ValidDomainName_UpdatesDomainName()
        {
            this[DOMAIN] = namestamp;

            Assert.IsNotNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID], domain = this[DOMAIN] }));

            GetDomain_ValidRequest_ReturnsDomain();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with a valid session and invalid domain name, returns BadRequest 400")]
        public void UpdateDomain_InvalidDomainName_Returns400()
        {
            Assert.IsNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID], domain = "invalid" }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with a valid session and valid ip_restriction_enabled, assert ip_restriction_enabled is updated.")]
        public void UpdateDomain_ValidIpRestrictionEnabled_UpdatesIpRestrictionEnabled()
        {
            this[IP_RESTRICTION_ENABLED] = true.ToString();

            Assert.IsNotNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID], ip_restriction_enabled = bool.Parse(this[IP_RESTRICTION_ENABLED]) }));

            GetDomain_ValidRequest_ReturnsDomain();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with a valid session and valid ip_restriction_mode, assert ip_restriction_mode is updated.")]
        public void UpdateDomain_ValidIpRestrictionMode_UpdatesIpRestrictionMode()
        {
            this[IP_RESTRICTION_MODE] = "whitelist";

            Assert.IsNotNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID], ip_restriction_mode = this[IP_RESTRICTION_MODE] }));

            GetDomain_ValidRequest_ReturnsDomain();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=UpdateDomain")]

        [Description("Call UpdateDomain with a valid session and valid ip_restriction_mode, assert ip_restriction_mode is updated.")]
        public void UpdateDomain_InvalidIpRestrictionMode_Returns400()
        {
            this[IP_RESTRICTION_MODE] = "yeallowlist";

            Assert.IsNull(_client.Patch(new UpdateDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID], ip_restriction_mode = this[IP_RESTRICTION_MODE] }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }
    }
}

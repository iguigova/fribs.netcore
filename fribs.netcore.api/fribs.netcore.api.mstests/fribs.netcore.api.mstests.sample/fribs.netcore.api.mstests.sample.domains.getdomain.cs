using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class DomainTests
    {
        protected const string IP_RESTRICTION_ENABLED = "ip_restriction_enabled";
        protected const string IP_RESTRICTION_MODE = "ip_restriction_mode";
        protected const string REGION_RESTRICTION_ENABLED = "region_restriction_enabled";
        protected const string REGION_RESTRICTION_MODE = "region_restriction_mode";
        protected const string TIME_RESTRICTION_ENABLED = "time_restriction_enabled";
        protected const string TIME_RESTRICTION_MODE = "time_restriction_mode";
        protected const string TIME_ZONE = "time_zone";        

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomain")]

        [Description("Call GetDomain with a valid session and valid parameters, assert domain_id and domain are returned.")]
        public void GetDomain_ValidRequest_ReturnsDomain()
        {
            var response = _client.Get(new GetDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] });

            Assert.That.For(response).IsTrue(s => s.domain_id == this[DOMAIN_ID]);
            Assert.That.For(response).IsTrue(s => s.domain == this[DOMAIN]);
            Assert.That.For(response).IsTrue(s => s.ip_restriction_enabled == bool.Parse(this[IP_RESTRICTION_ENABLED] ?? "false"));
            Assert.That.For(response).IsTrue(s => s.ip_restriction_mode == (this[IP_RESTRICTION_MODE] ?? s.ip_restriction_mode));
            Assert.That.For(response).IsTrue(s => s.region_restriction_enabled == bool.Parse(this[REGION_RESTRICTION_ENABLED] ?? "false"));
            Assert.That.For(response).IsTrue(s => s.region_restriction_mode == (this[REGION_RESTRICTION_MODE] ?? s.region_restriction_mode));
            Assert.That.For(response).IsTrue(s => s.time_restriction_enabled == bool.Parse(this[TIME_RESTRICTION_ENABLED] ?? "false"));
            Assert.That.For(response).IsTrue(s => s.time_restriction_mode == (this[TIME_RESTRICTION_MODE] ?? s.time_restriction_mode));
            Assert.That.For(response).IsTrue(s => s.time_zone == (this[TIME_ZONE] ?? s.time_zone));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomain")]

        [Description("Call GetDomain with basic auth and valid parameters, assert domain_id and domain are returned.")]
        public void GetDomain_BasicAuth_ReturnsDomain()
        {
            SetBasicAuthCredentials();

            GetDomain_ValidRequest_ReturnsDomain();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomain")]

        [Description("Call GetDomain without authentication, assert 401 is returned.")]
        public void GetDomain_Public_Returns401()
        {
            Logout();

            Assert.IsNull(_client.Get(new GetDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=GetDomain")]

        [Description("Call GetDomain without valid authorization, assert 404 is returned.")]
        public void GetDomain_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Get(new GetDomain() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }
    }
}

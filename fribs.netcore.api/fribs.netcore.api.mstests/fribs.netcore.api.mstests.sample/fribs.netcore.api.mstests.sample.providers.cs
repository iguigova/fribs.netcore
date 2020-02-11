using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    [TestClass]
    public class Providers : DomainTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=EnableProvider")]

        [Description("Call EnableProvider without authorization, assert 404 Unauthorized is returned.")]
        public void EnableProvider_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Post(new EnableProvider() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID], identity_provider_id = this[GOOGLE_SANDBOX_ID] }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }
    }
}

using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fribs.netcore.api.mstests.sample
{
    public partial class RestrictionTests
    {
        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=RemoveRestrictions")]

        [Description("Call RemoveRestrictions with a valid session and valid app id, assert 200 is returned.")]
        public void RemoveRestrictions_ValidRequest_Returns200()
        {
            var response = _client.Delete(new RemoveRestrictions() { app_id = this[APP_ID] });

            Assert.IsNotNull(response);
            Assert.That.For(_client.Get(new GetRestrictions() { app_id = this[APP_ID] })).IsTrue(s => s.results.Count == 0);
            Assert.That.For(_client.Get(new GetRestrictions() { app_id = this[APP_ID], domain_id = this[DOMAIN_ID] })).IsTrue(s => s.results.Count == 0);
        }
    }
}

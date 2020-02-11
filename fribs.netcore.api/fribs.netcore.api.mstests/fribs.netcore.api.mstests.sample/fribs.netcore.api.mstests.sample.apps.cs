using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fribs.netcore.api.mstests.sample
{
    [TestClass]
    public class AppTests : Tests
    {
        protected const string APP_ID = "app_id";

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateApp")]

        [Description("Call CreateApp with a valid session and gateway guid, assert app_id is returned.")]
        public void CreateApp_ValidRequest_ReturnsAppId()
        {
            var app_id = _client.Post(new CreateApp() { name = namestamp, gateway_id = this[GATEWAY_ID] })?.app_id;

            this[APP_ID] = app_id;

            Cleanup(() => _client.Delete(new RemoveApp() { app_id = app_id }));

            Assert.IsNotNull(app_id);
        }

        protected override void TestInitialize()
        {
            base.TestInitialize();

            if (!TestContext.TestName.Contains("_SkipCreateAppOnTestInit") 
                && (!TestContext.FullyQualifiedTestClassName.Contains("fribs.netcore.api.mstests.sample.AppTests") 
                    || !TestContext.TestName.Contains("CreateApp_")))
            {
                CreateApp_ValidRequest_ReturnsAppId();
            }
        }
    }
}

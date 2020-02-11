using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fribs.netcore.api.mstests.sample
{
    [TestClass]
    public partial class DomainTests : AppTests
    {
        protected override void TestInitialize()
        {
            base.TestInitialize();

            if (!TestContext.TestName.Contains("_SkipCreateDomainOnTestInit")
                && (!TestContext.FullyQualifiedTestClassName.Contains("fribs.netcore.api.mstests.sample.DomainTests")
                    || !TestContext.TestName.Contains("CreateDomain_")))
            {
                CreateDomain_ValidRequest_ReturnsDomainId();
            }
        }
    }
}

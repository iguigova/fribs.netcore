using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fribs.netcore.api.mstests.sample
{
    [TestClass]
    public partial class RestrictionTests : DomainTests
    {
        protected override void TestInitialize()
        {
            base.TestInitialize();

            if (!TestContext.TestName.Contains("_SkipCreateRestrictionOnTestInit")
                && (!TestContext.FullyQualifiedTestClassName.Contains("fribs.netcore.api.mstests.sample.RestrictionTests")
                    || !TestContext.TestName.Contains("CreateRestriction_")))
            {
                CreateRestriction_ValidRequest_ReturnsRestrictionId();
            }
        }
    }
}

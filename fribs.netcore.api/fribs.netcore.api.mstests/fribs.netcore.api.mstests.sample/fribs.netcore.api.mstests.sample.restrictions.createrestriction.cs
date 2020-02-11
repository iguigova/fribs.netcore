using fribs.netcore.api.mstests.sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace fribs.netcore.api.mstests.sample
{
    public partial class RestrictionTests
    {
        protected const string DOMAIN_RESTRICTION_ID = "domain_restriction_id";
        protected const string RESTRICTION_ID = "restriction_id";
        protected const string RESTRICTION_TYPE = "restriction_type";
        protected const string REGION_RESTRICTION_CODES = "region_restriction_codes";

        protected const string TIMERESTRICTION = "timerestriction";
        protected const string IPRESTRICTION = "iprestriction";
        protected const string REGIONRESTRICTION = "regionrestriction";

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateRestriction")]

        [Description("Call CreateRestriction with a valid session and parameters, assert restriction id is returned.")]
        public void CreateRestriction_ValidRequest_ReturnsRestrictionId()
        {
            var restriction_type = REGIONRESTRICTION;
            var restriction_region_codes = new List<string>() { "CA" };

            var restriction_id = _client.Post(new CreateRestriction() 
            { 
                app_id = this[APP_ID],  
                type = restriction_type, 
                region_country_codes = restriction_region_codes
            }).restriction_ids.FirstOrDefault();

            this[RESTRICTION_ID] = restriction_id;
            this[RESTRICTION_TYPE] = restriction_type;
            this[REGION_RESTRICTION_CODES] = string.Join(',', restriction_region_codes);

            //Cleanup(() => _client.Delete(new RemoveRestriction() { app_id = this[APP_ID], restriction_id = restriction_id })); // Handled by cleanup - RemoveApp - in base class

            Assert.IsNotNull(restriction_id);

            Assert.IsNotNull(this[DOMAIN_RESTRICTION_ID] = _client.Post(new CreateRestriction()  // test domain specific restrictions too
            {
                app_id = this[APP_ID],
                domain_id = this[DOMAIN_ID],
                type = restriction_type,
                region_country_codes = restriction_region_codes
            }).restriction_ids.FirstOrDefault());
        }

        [Description("Call CreateRestriction with basic auth and valid parameters, assert restriction id is returned.")]
        public void CreateRestriction_BasicAuth_ReturnsRestrictionId()
        {
            SetBasicAuthCredentials();

            CreateRestriction_ValidRequest_ReturnsRestrictionId();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateRestriction")]

        [Description("Call CreateRestriction without authentication, assert 401 UnAuthorized is returned.")]
        public void CreateRestriction_Public_Returns401()
        {
            Logout();

            Assert.IsNull(_client.Post(new CreateRestriction()
            {
                app_id = this[APP_ID],
                type = REGIONRESTRICTION,
                region_country_codes = new List<string>() { "CA" }
            }, Is(HttpStatusCode.Unauthorized, UNAUTHORIZED_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateRestriction")]

        [Description("Call CreateRestriction without valid authorization, assert 404 NotFound is returned.")]
        public void CreateRestriction_NoAccess_Returns404()
        {
            Authenticate(this[EMAIL_ADDRESS2], this[PASSWORD]);

            Assert.IsNull(_client.Post(new CreateRestriction()
            {
                app_id = this[APP_ID],
                type = REGIONRESTRICTION,
                region_country_codes = new List<string>() { "CA" }
            }, Is(HttpStatusCode.NotFound, API_ERROR)));
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateRestriction")]

        [Description("Call CreateRestriction with a valid session but invalid type, assert 400 Bad Request is returned.")]
        public void CreateRestriction_InvalidType_Returns400()
        {
            Assert.IsNull(_client.Post(new CreateRestriction() { app_id = this[APP_ID], type = "invalid" }, Is(HttpStatusCode.BadRequest, VALIDATION_ERROR)));
        }        

        [TestMethod]
        [TestCategory(TESTCATEGORY_API)]
        [TestProperty("metadata", "https://manage.logon-dev.com/json/metadata?op=CreateRestriction")]

        [Description("Call CreateRestriction with a valid session but missing type, assert 400 Bad Request is returned.")]
        public void CreateRestriction_MissingType_Returns400()
        {
            Assert.IsNull(_client.Post(new CreateRestriction() { app_id = this[APP_ID] }, Is(HttpStatusCode.BadRequest, API_ERROR)));
        }
    }
}

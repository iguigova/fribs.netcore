using System.Runtime.Serialization;
using fribs.netcore.api.response;
using ServiceStack;

namespace fribs.netcore.api.echo.model
{
	[Route("/api/authorize", "GET")]
	[Route("/api/authorize", "POST")]
	[DataContract]
    public class Authorize : IReturn<AuthorizeResponse>
    {
		// var request = "{0}?client_id={1}&state={2}&response_type=code&access_type=offline&include_granted_scopes=true&redirect_uri={3}&scope={4}&prompt={5}&login_hint={6}";

		[DataMember(IsRequired = true)]
		public string client_id { get; set; }

		[DataMember(IsRequired = true)]
		public string response_type { get; set; }

		[DataMember(IsRequired = true)]
		public string access_type { get; set; }

		[DataMember(IsRequired = true)]
		public string redirect_uri { get; set; }

		[DataMember(IsRequired = false)]
		public bool include_granted_scopes { get; set; }
		
		[DataMember(IsRequired = false)]
		public string scope { get; set; }

		[DataMember(IsRequired = false)]
		public string prompt { get; set; }

		[DataMember(IsRequired = false)]
		public string login_hint { get; set; }

		[DataMember(IsRequired = false)]
		public string state { get; set; }
	}

    public class AuthorizeResponse : IBaseResponse
    {
        public IErrorResponse error { get; set; }

		[DataMember]
		public string code { get; set; }

		[DataMember]
		public string state { get; set; }
	}
}
using fribs.netcore.api.response;
using System.Runtime.Serialization;
using ServiceStack;

namespace fribs.netcore.api.echo.model
{
	[Route("/api/token", "POST")]
	[DataContract]
	public class Token : IReturn<TokenResponse>
	{
		//var data = "grant_type=authorization_code&code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&scope={4}";

		[DataMember(IsRequired = true)]
		public string grant_type { get; set; }

		[DataMember(IsRequired = true)]
		public string code { get; set; }

		[DataMember(IsRequired = true)]
		public string client_id { get; set; }

		[DataMember(IsRequired = true)]
		public string client_secret { get; set; }

		[DataMember(IsRequired = true)]
		public string redirect_uri { get; set; }

		[DataMember(IsRequired = true)]
		public string scope { get; set; }
	}

	public class TokenResponse : IBaseResponse
	{
		public IErrorResponse error { get; set; }

		[DataMember]
		public string access_token { get; set; }

		[DataMember]
		public int expires_in { get; set; }

		[DataMember]
		public string refresh_token { get; set; }

		[DataMember]
		public string id_token { get; set; }

		[DataMember]
		public string error_code { get; set; }

		[DataMember]
		public string error_description { get; set; }
	}
}

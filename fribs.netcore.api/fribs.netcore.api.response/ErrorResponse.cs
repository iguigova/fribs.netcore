using System.Runtime.Serialization;

namespace fribs.netcore.api.response
{
	public class ErrorResponse : IErrorResponse
	{
		public ErrorResponse() { }

		public ErrorResponse(string errorCode, string errorMessage)
		{
			code = errorCode;
			message = errorMessage;
		}

		[DataMember]
		public string code { get; set; }

		[DataMember]
		public string message { get; set; }
	}
}

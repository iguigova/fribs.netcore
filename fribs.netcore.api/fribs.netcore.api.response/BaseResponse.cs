using System.Runtime.Serialization;

namespace fribs.netcore.api.response
{
	public class BaseResponse : IBaseResponse
	{
		public BaseResponse() { }

		public BaseResponse(string errorCode, string message)
		{
			error = new ErrorResponse(errorCode, message);
		}

		[DataMember]
		public IErrorResponse error { get; set; }
	}
}

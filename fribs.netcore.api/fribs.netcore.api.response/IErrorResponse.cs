namespace fribs.netcore.api.response
{
	public interface IErrorResponse
	{
		string code { get; set; }
		string message { get; set; }
	}
}

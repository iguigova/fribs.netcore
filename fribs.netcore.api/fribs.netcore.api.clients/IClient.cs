using System;

namespace fribs.netcore.api.clients
{
	public interface IClient
	{
		//WebClient Client { get; }

		string Post(string url, string data = "");

		string Put(string url, string data = "");

		string Get(string url);

		string Options(string url);

		Action<string> OnInfo { get; set; }

		Action<Exception> OnException { get; set; }
	}
}

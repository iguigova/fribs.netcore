using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace fribs.netcore.api.clients
{
	public class WebClient : IClient
	{
		protected string _url;
		protected System.Net.WebClient _client = new System.Net.WebClient();

		public System.Net.WebClient Client { get { return _client; } }

		public WebClient(string url)
		{
			_url = url;
		}

		public string Post(string url, string data = "")
		{
			return Update("POST", url, data);
		}

		public string Put(string url, string data = "")
		{
			return Update("PUT", url, data);
		}

		private string Update(string method, string url, string data = "")
		{
			return Log(url, method, () => Client.UploadString(url, method, data), data);
		}

		public string Get(string url)
		{
			return Log(url, "GET", () => Client.DownloadString(url));
		}

		public string Options(string url)
		{
			return Log(url, "OPTIONS", () => Client.UploadString(url, "OPTIONS", string.Empty));
		}

		public Action<string> OnInfo { get; set; }
		public Action<Exception> OnException { get; set; }

		private T Log<T>(string url, string method, Func<T> function, string data = "")
		{
			OnInfo?.Invoke(RemovePasswordInformation(url));
			OnInfo?.Invoke(method);
			OnInfo?.Invoke(data);

			Client.Headers[HttpRequestHeader.ContentType] = Client.Headers[HttpRequestHeader.ContentType] ?? "application/json";
			Client.Headers[HttpRequestHeader.Accept] = Client.Headers[HttpRequestHeader.Accept] ?? "application/json";

			try
			{
				var response = function();

				OnInfo?.Invoke(response.ToString());

				return response;
			}
			catch (WebException ex)
			{
				var responseText = string.Empty;

				if (ex.Response != null)
				{
					var responseStream = ex.Response.GetResponseStream();

					if (responseStream != null)
					{
						using (var reader = new StreamReader(responseStream))
						{
							responseText = reader.ReadToEnd();
						}
					}
				}

				ex.Data.Add(ex.Data.Count, RemovePasswordInformation(url));
				ex.Data.Add(ex.Data.Count, responseText);

				OnInfo?.Invoke($"Web Exception: {ex.Message} - {ex.Data[ex.Data.Count - 1]}");
				OnException?.Invoke(ex);

				throw;
			}
		}

		private static Regex _passwordPattern = new Regex(@"(password)=([^&]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private string RemovePasswordInformation(string text)
		{
			return _passwordPattern.Replace(text, "$1=*****");
		}
	}
}

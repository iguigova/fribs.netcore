using System;
using ServiceStack;

namespace fribs.netcore.exceptions
{
    public class UnauthorizedException : Exception, IHasStatusCode
    {
		public UnauthorizedException() : base("") { }

		public UnauthorizedException(string message) : base(message) { }

		public int StatusCode => 401;
    }
}

using System;
using ServiceStack;

namespace fribs.netcore.exceptions
{
    public class ForbiddenException : Exception, IHasStatusCode
    {
		public ForbiddenException() : base("") { }

		public ForbiddenException(string message) : base(message) { }

		public int StatusCode => 403;
    }
}

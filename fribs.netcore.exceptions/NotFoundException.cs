using System;
using ServiceStack;

namespace fribs.netcore.exceptions
{
    public class NotFoundException : Exception, IHasStatusCode
    {
		public NotFoundException() : base("") { }

		public NotFoundException(string message) : base(message) { }

		public int StatusCode => 404;
    }
}

using ServiceStack;
using System;

namespace fribs.netcore.exceptions
{
	public class UnspecifiedException : Exception, IHasStatusCode
    {
		public UnspecifiedException() : base("") { }

		public UnspecifiedException(string message) : base(message) { }

        public int StatusCode => 400;
    }
}

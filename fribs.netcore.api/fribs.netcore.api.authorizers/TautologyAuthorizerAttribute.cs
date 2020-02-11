using System;
using ServiceStack.Web;

namespace fribs.netcore.api.authorizers
{
	public class TautologyAuthorizerAttribute : Attribute, IAuthorizer<object>
	{
		private bool _alwaysAllow = false;

		public TautologyAuthorizerAttribute(bool alwaysAllow = false)
		{
			_alwaysAllow = alwaysAllow;
		}

		protected virtual bool IsAuthorized(IRequest req, object reqDto)
		{
			return true;
		}

		public bool HasAccess(IRequest req, object reqDto)
		{
			return _alwaysAllow || IsAuthorized(req, reqDto);
		}
	}
}

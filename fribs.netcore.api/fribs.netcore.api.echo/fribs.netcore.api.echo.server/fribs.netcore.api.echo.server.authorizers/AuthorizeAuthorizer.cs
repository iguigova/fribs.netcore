using fribs.netcore.api.authorizers;
using fribs.netcore.api.echo.model;
using ServiceStack.Web;

namespace fribs.netcore.api.echo.server
{
	public class AuthorizeAuthorizer : TautologyAuthorizerAttribute, IAuthorizer<Authorize>
	{
		public bool HasAccess(IRequest req, Authorize reqDto)
		{
			return true;
		}
	}
}

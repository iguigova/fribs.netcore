using fribs.netcore.api.authorizers;
using fribs.netcore.api.echo.model;
using ServiceStack.Web;

namespace fribs.netcore.api.echo.server
{
	public class TokenAuthorizer : TautologyAuthorizerAttribute, IAuthorizer<Token>
	{
		public bool HasAccess(IRequest req, Token reqDto)
		{
			return true;
		}
	}
}

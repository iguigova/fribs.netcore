using ServiceStack.Web;

namespace fribs.netcore.api.authorizers
{
	public interface IAuthorizer<in T>
	{
		bool HasAccess(IRequest req, T reqDto);
	}
}

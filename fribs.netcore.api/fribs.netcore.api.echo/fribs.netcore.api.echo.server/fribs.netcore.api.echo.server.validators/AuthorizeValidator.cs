using fribs.netcore.api.echo.model;
using ServiceStack.FluentValidation;

namespace fribs.netcore.api.echo.server
{
	public class AuthorizeValidator : AbstractValidator<Authorize>
	{
		public AuthorizeValidator()
		{
		}
	}
}

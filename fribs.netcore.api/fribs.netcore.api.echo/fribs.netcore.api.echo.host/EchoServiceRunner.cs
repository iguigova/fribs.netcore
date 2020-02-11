using fribs.netcore.api.authorizers;
using fribs.netcore.exceptions;
using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Web;
using System;

namespace fribs.netcore.api.echo.host
{
    public class EchoServiceRunner<T> : ServiceRunner<T>
    {
		public EchoServiceRunner(IAppHost appHost, ActionContext actionContext) : base(appHost, actionContext) { }

        public override void BeforeEachRequest(IRequest req, T request, object service)
        {
			var authorizer = AuthorizerMapper<T>.Authorizer;

            if (authorizer != null)
            {
                if (!authorizer.HasAccess(req, (dynamic)request.GetDto()))
                {
                    throw new ForbiddenException("The user is not allowed to access that resource.");
                }
            }
            else
            {
				if (typeof(T).BaseType != typeof(ServiceStack.NativeTypes.NativeTypesBase))
				{
					throw new NotImplementedException($"Service must have {typeof(T).Name} authorizer defined.");
				}
            }

            base.BeforeEachRequest(req, request, service);
        }
    }
}

using Funq;
using fribs.netcore.configuration;
using fribs.netcore.api.response;
using fribs.netcore.api.extensions;
using fribs.netcore.api.echo.server;
using fribs.netcore.exceptions;
using fribs.netcore.logging;
using Microsoft.AspNetCore.Http;
using ServiceStack;
using ServiceStack.Validation;
using ServiceStack.Web;
using ServiceStack.Host;

namespace fribs.netcore.api.echo.host
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class EchoAppHost : AppHostBase
    {
        public EchoAppHost(string appHostName) : base(appHostName, typeof(AuthorizeService).Assembly)
        {
            Log.Info($"{appHostName} started...");
        }

        public override void Configure(Container container)
        {
            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(AuthorizeValidator).Assembly);

            SetConfig(new HostConfig()
            {
                HandlerFactoryPath = "",
                ReturnsInnerException = true,
                MapExceptionToStatusCode = ExceptionMapper.StatusCodes,
                EnableFeatures = Feature.All.Remove(Feature.MsgPack | Feature.ProtoBuf | Feature.Soap | Feature.Soap11 | Feature.Soap12 | Feature.Xml | Feature.Csv | Feature.Html)
            });

            PreRequestFilters.Add((req, resp) =>
            {
                Log.Info(req.AbsoluteUri);
            });

            ServiceExceptionHandlers.Add((iReq, request, ex) =>
            {
                Log.Error(ex);

				var httpRequest = iReq.OriginalRequest as HttpRequest;
				var httpResponse = iReq.Response.OriginalResponse as HttpResponse;

				httpResponse.AddCors(httpRequest.Headers["Origin"], fribs.netcore.configuration.Configuration.Instance.CorsWhitelist);

				httpResponse.StatusCode = ((int?)ex.GetStatus()).GetValueOrDefault(400);

				return new BaseResponse(ex.GetType().Name, $"{new ExceptionParser(ex)}");
            });

            //Handle Unhandled Exceptions occurring outside of Services
            //E.g. Exceptions during Request binding or in filters:
            UncaughtExceptionHandlers.Add((iReq, response, operationName, ex) =>
            {
                Log.Error(ex);

				var httpRequest = iReq.OriginalRequest as HttpRequest;
				var httpResponse = iReq.Response.OriginalResponse as HttpResponse;

				httpResponse.AddCors(httpRequest.Headers["Origin"], fribs.netcore.configuration.Configuration.Instance.CorsWhitelist);

				response.StatusCode = ((int?)ex.GetStatus()).GetValueOrDefault(500);

				response.StatusDescription = HttpStatus.GetStatusDescription(response.StatusCode);

				response.ContentType = "application/json";

				response.WriteAsync(ServiceStack.Text.JsonSerializer.SerializeToString(new BaseResponse(ex.GetType().Name, $"{new ExceptionParser(ex)}")));
            });
        }

        public override IServiceRunner<TRequest> CreateServiceRunner<TRequest>(ActionContext actionContext)
        {
            return new EchoServiceRunner<TRequest>(this, actionContext);
        }

        public override void OnEndRequest(IRequest request = null)
        {
            //Log.Flush();

            base.OnEndRequest(request);
        }
    }
}
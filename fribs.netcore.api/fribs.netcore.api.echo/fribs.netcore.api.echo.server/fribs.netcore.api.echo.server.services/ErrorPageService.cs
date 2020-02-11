using System;
using fribs.netcore.api.authorizers;
using fribs.netcore.api.echo.model;
using ServiceStack;

namespace fribs.netcore.api.echo.server
{
    public class ErrorPageService : ServiceStack.Service
    {
        public ErrorPageService(){}

        [TautologyAuthorizer]
        public void Any(ErrorPage request)
        {
            base.Response.ContentType = "text/html";
            base.Response.WriteAsync($@"
<html>
	<head>
		<title>ERROR</title>
	</head>
	<body>
		<h3>Oops</h3>
		<p>There was a problem processing your authorization. Please try again. {DateTime.UtcNow} </p>
	</body>
</html>
");
        }
    }
}
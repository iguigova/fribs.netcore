using System.Runtime.Serialization;
using ServiceStack;

namespace fribs.netcore.api.echo.model
{
    [Route("/error")]
    [DataContract]
    public class ErrorPage : IReturn<object>
    {
        
    }
}
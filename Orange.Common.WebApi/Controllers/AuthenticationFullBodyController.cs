using Orange.Common.WebApiFramework;
using System.Web.Http;

namespace Orange.Common.WebApi
{
    [TokenAuthentication]
    [AuthenticationBody]
    [ChannelBody]
    [LanguageSetter]
    //[InputValidation]
    public class AuthenticationFullBodyController : ApiController
    {
    }
}

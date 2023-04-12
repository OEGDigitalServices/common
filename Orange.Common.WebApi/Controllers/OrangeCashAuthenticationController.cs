using Orange.Common.WebApiFramework;
using System.Web.Http;

namespace Orange.Common.WebApi
{
    [ValidateOrangeCashDialAndPin]
    [LanguageSetter]
    [TokenAuthentication]
    [ChannelBody]
    [InputValidation]

    public class OrangeCashAuthenticationController : ApiController
    {
    }
}
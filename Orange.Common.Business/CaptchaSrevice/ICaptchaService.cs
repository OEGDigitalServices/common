using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public interface ICaptchaService
    {
        bool IsValidCaptcha(string token);
    }
}
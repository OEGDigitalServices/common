using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public interface ICaptchaService
    {
        Task<bool> isValidCaptcha(string token);
    }
}
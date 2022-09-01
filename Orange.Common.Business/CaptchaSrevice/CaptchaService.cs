using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public class CaptchaService : ICaptchaService
    {
        private readonly string googleCaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";
        private readonly string SecretKey = "Secret_Key";

        public async Task<bool> isValidCaptcha(string token)
        {
            var params_ = $"?secret={SecretKey}&response={token}";
            var fullUrl = googleCaptchaUrl + params_;

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(fullUrl);

                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<CaptchaResponse>(response);

                    if (result.success == true && result.score >= .4)
                        return true;
                    return false;
                }

                return false;
            }
        }
    }

    public class CaptchaResponse
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        public double score { get; set; }
        public string action { get; set; }
    }
}

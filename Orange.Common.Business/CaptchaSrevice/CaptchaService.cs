using System;
using Orange.Common.Entities;
using Orange.Common.Utilities;

using Keys = Orange.Common.Business.Strings.Keys;

namespace Orange.Common.Business
{
    public class CaptchaService : ICaptchaService
    {
        #region Prop
        private readonly IHttpClientManager _httpClientManager;
        private readonly IUtilities _utilities;
        private readonly ILogger _logger;
        #endregion

        #region CTOR
        public CaptchaService(IHttpClientManager httpClientManager,
            IUtilities utilities,
            ILogger logger)
        {
            _httpClientManager = httpClientManager;
            _utilities = utilities;
            _logger = logger;
        }
        #endregion

        #region Methods
        public bool IsValidCaptcha(string token)
        {
            if (!IsCaptchaEnabled())
                return true;

            try
            {
                var response = _httpClientManager.Get<CaptchaResponse>(GetCaptchaUrl(token))
                    .GetAwaiter()
                    .GetResult();

                if (response.success == true && response.score >= GetCaptchaThreshold())
                        return true;

                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return false;
            }
        }

        #endregion

        #region Helpers
        private bool IsCaptchaEnabled()
        {
            var result = bool.TryParse(GetAppSetting(Keys.IsCaptchaEnabled),out bool isCaptchaEnabled);

            if (result)
                return isCaptchaEnabled;

            // Disabled By Default
            return result;
        }
        private string GetCaptchaUrl(string token)
        {
            var CaptchaUrl = GetAppSetting(Keys.CaptchaUrl);
            var SecretKey = GetAppSetting(Keys.SecretKey);
            return string.Concat(
                CaptchaUrl,
                $"?secret={SecretKey}&response={token}");
        }
        private double GetCaptchaThreshold()
        {
            try
            {
                var CaptchaThreshold = Convert.ToDouble(GetAppSetting(Keys.CaptchaThreshold));

                return CaptchaThreshold; 
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e);
                return 0.4;
            }
        }
        private string GetAppSetting(string key)
        {
            var value = _utilities.GetAppSetting(key);
            return value;
        }
        #endregion
    }
}

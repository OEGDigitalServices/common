using System;
using Orange.Common.Entities;
using Orange.Common.Utilities;

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
            // TODO: after validate, remove this check
            //var isCaptchaEnabled = bool.Parse(_utilities.GetAppSetting(Strings.Keys.IsCaptchaEnabled));
            //if (!isCaptchaEnabled)
            //    return true;
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
        private string GetCaptchaUrl(string token)
        {
            var CaptchaUrl = _utilities.GetAppSetting(Strings.AppSettings.CaptchaUrl);
            var SecretKey = _utilities.GetAppSetting(Strings.Keys.SecretKey);
            return string.Concat(
                CaptchaUrl,
                $"?secret={SecretKey}&response={token}");
        }
        private double GetCaptchaThreshold()
        {
            try
            {
                var CaptchaThreshold = Convert.ToDouble(
                        _utilities.GetAppSetting(Strings.Keys.CaptchaThreshold));

                return CaptchaThreshold; 
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e);
                return 0.4;
            }
        }
        #endregion
    }
}

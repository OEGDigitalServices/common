namespace Orange.Common.Utilities
{
    public class KafkaConfigurations
    {
        #region Props

        private readonly IUtilities _utilities;

        #endregion

        #region Ctor

        public KafkaConfigurations(IUtilities utilities)
        {
            _utilities = utilities;
        }

        #endregion

        #region Methods

        public string BootstrapServers =>
            GetAppSettings(Strings.AppSettingKeys.DefaultBootstrapServers);

        public int LingerMs =>
            GetIntVal(GetAppSettings(Strings.AppSettingKeys.LingerMs), 5.ToString());

        public int MessageTimeoutMs =>
            GetIntVal(GetAppSettings(Strings.AppSettingKeys.LingerMs), 10000.ToString());

        #endregion

        #region Helpers

        private string GetAppSettings(string AppSettingsKey)
        {
            return _utilities.GetAppSetting(AppSettingsKey);
        }

        private int GetIntVal(string value, string defaultVal)
        {
            var result = int.TryParse(value, out int intValue);
            if (result)
                return intValue;
            if(int.TryParse(defaultVal, out int intDefautlValue))
            {
                return intDefautlValue;
            }
            return default;
        }

        #endregion
    }
}

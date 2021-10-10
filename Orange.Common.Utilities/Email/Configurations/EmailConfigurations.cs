namespace Orange.Common.Utilities
{
    public class EmailConfigurations
    {
        private readonly IUtilities _utilities;

        public string SMTPAddress { get; }
        public int Port { get; }
        public string EmailFromAddress { get; }
        public string EmailFromPassword { get; }

        public EmailConfigurations(IUtilities utilities)
        {
            _utilities = utilities;
            SMTPAddress = _utilities.GetAppSetting(Strings.AppSettingKeys.SMTPAddress);
            Port = int.Parse(_utilities.GetAppSetting(Strings.AppSettingKeys.Port));
            EmailFromAddress = _utilities.GetAppSetting(Strings.AppSettingKeys.EmailFromAddress);
            EmailFromPassword = _utilities.GetAppSetting(Strings.AppSettingKeys.EmailFromPassword);
        }
    }
}

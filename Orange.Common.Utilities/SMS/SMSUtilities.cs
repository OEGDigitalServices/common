using Orange.Common.Entities;
using System;

namespace Orange.Common.Utilities
{
    /// <summary>
    /// TODO: Code needs more enhancments
    /// </summary>
    public class SMSUtilities : ISMSUtilities
    {
        #region Props

        private readonly IUtilities _utilities;
        private readonly ILogger _logger;
        private readonly ISmsMessageManager _smsMessageManager;

        #endregion

        #region Ctors

        public SMSUtilities(IUtilities utilities, ILogger logger, ISmsMessageManager smsMessageManager)
        {
            _utilities = utilities;
            _logger = logger;
            _smsMessageManager = smsMessageManager;
        }

        #endregion

        public bool SendSMSMessage(string MobileNumber, string msgBody, string language, string alias)
        {
            bool flag = false;
            try
            {
                string international = Strings.Numbers.DoubleZero;
                string temp = MobileNumber;
                if (MobileNumber.StartsWith(international))
                    MobileNumber = temp;
                else if (MobileNumber.StartsWith(Strings.Numbers.Zero))
                    MobileNumber = Strings.Numbers.Two + MobileNumber;
                else if (!MobileNumber.StartsWith(Strings.Numbers.Two.ToString()))
                    MobileNumber = Strings.Numbers.TwoZero + MobileNumber;

                int msgOption;
                string tmp = msgBody;
                if (language.ToLower().Trim().Equals(Strings.Cultures.Arabic.ToLower()))
                {
                    if (tmp.Length > 70)
                    {
                        int MesssageLength = 67;
                        int NumberOfMessages = msgBody.Length / MesssageLength + 1;
                        msgOption = 192;

                        string Message;
                        for (int i = 1; i <= NumberOfMessages; i++)
                        {
                            string Header = "05000312";
                            Header += string.Format("{0:x2}", (byte)NumberOfMessages); // Number of Part
                            Header += string.Format("{0:x2}", (byte)i); // Current Part
                            if (i < NumberOfMessages)
                                Message = msgBody.Substring((i - 1) * MesssageLength, MesssageLength);
                            else
                                Message = msgBody.Substring((i - 1) * MesssageLength);

                            tmp = string.Empty;
                            for (int _iLoop = 0; _iLoop < Message.Length; _iLoop++)
                                tmp += string.Format(Strings.SMSFormats.BodyFormat, (int)Message[_iLoop]);

                            Message = Header + tmp;

                            MessageDetails msgDetails = new MessageDetails();
                            string msgUsername = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSUsername);
                            string msgPassword = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSPassword);

                            _smsMessageManager.Initialize(msgUsername, msgPassword);

                            msgDetails.MessageDate = DateTime.Now;
                            msgDetails.MessageTarget = MobileNumber;
                            msgDetails.MessageOption = Convert.ToInt32(msgOption);
                            msgDetails.MessageBody = Message;
                            if (alias != string.Empty)
                                msgDetails.Alias = alias;
                            else
                                msgDetails.Alias = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSTitle);

                            int Messageid = _smsMessageManager.SendMessage(msgDetails);
                            if (Messageid > 0)
                                flag = true;
                        }
                    }
                    else
                    {
                        msgOption = 64;
                        tmp = string.Empty;
                        for (int _iLoop = 0; _iLoop < msgBody.Length; _iLoop++)
                            tmp += string.Format(Strings.SMSFormats.BodyFormat, (int)msgBody[_iLoop]);

                        MessageDetails msgDetails = new MessageDetails();
                        string msgUsername = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSUsername);
                        string msgPassword = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSPassword);
                        _smsMessageManager.Initialize(msgUsername, msgPassword);
                        msgDetails.MessageDate = DateTime.Now;
                        msgDetails.MessageTarget = MobileNumber;
                        msgDetails.MessageOption = Convert.ToInt32(msgOption);
                        msgDetails.MessageBody = tmp;
                        if (alias != string.Empty)
                            msgDetails.Alias = alias;
                        else
                            msgDetails.Alias = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSTitle);

                        int Messageid = _smsMessageManager.SendMessage(msgDetails);
                        if (Messageid > 0)
                            flag = true;
                    }
                }
                else
                {
                    if (tmp.Length > 160)
                    {
                        int MesssageLength = 153;
                        int NumberOfMessages = msgBody.Length / MesssageLength + 1;
                        msgOption = 128;
                        string Message;
                        for (int i = 1; i <= NumberOfMessages; i++)
                        {
                            string Header = "05000312";
                            Header += string.Format("{0:x2}", (byte)NumberOfMessages); // Number of Part
                            Header += string.Format("{0:x2}", (byte)i); // Current Part
                            if (i < NumberOfMessages)
                                Message = msgBody.Substring((i - 1) * MesssageLength, MesssageLength);
                            else
                                Message = msgBody.Substring((i - 1) * MesssageLength);

                            tmp = string.Empty;
                            for (int j = 0; j < Message.Length; j++)
                                tmp += string.Format("{0:X2}", (char)Message[j]);

                            Message = Header + tmp;
                            MessageDetails msgDetails = new MessageDetails();
                            string msgUsername = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSUsername);
                            string msgPassword = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSPassword);
                            _smsMessageManager.Initialize(msgUsername, msgPassword);
                            msgDetails.MessageDate = DateTime.Now;
                            msgDetails.MessageTarget = MobileNumber;
                            msgDetails.MessageOption = Convert.ToInt32(msgOption);
                            msgDetails.MessageBody = Message;
                            if (alias != string.Empty)
                                msgDetails.Alias = alias;
                            else
                                msgDetails.Alias = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSTitle);

                            int Messageid = _smsMessageManager.SendMessage(msgDetails);
                            if (Messageid > 0)
                                flag = true;
                        }
                    }
                    else
                    {
                        msgOption = 0;
                        MessageDetails msgDetails = new MessageDetails();
                        string msgUsername = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSUsername);
                        string msgPassword = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSPassword);

                        _smsMessageManager.Initialize(msgUsername, msgPassword);
                        msgDetails.MessageDate = DateTime.Now;
                        msgDetails.MessageTarget = MobileNumber;
                        msgDetails.MessageOption = Convert.ToInt32(msgOption);
                        msgDetails.MessageBody = tmp;
                        if (alias != string.Empty)
                            msgDetails.Alias = alias;
                        else
                            msgDetails.Alias = _utilities.GetAppSetting(Strings.AppSettingKeys.SMSTitle);

                        int Messageid = _smsMessageManager.SendMessage(msgDetails);
                        if (Messageid > 0)
                            flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex, false);
                flag = false;
            }

            return flag;
        }
    }
}

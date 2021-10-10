using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Orange.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;

namespace Orange.Common.Utilities
{
    public class Utilities : IUtilities
    {
        private readonly ILogger _logger;
        public Utilities(ILogger logger)
        {
            _logger = logger;
        }
        public string AdminEmail
        {
            get { return GetAppSetting(Strings.Mails.AdminEmail); }
        }
        public string BaseSiteUrl
        {
            get { return GetAppSetting(Strings.SharePoint.BaseSiteUrl); }
        }
        public string MyAccountTargetWeb
        {
            get
            {
                return GetAppSetting(Strings.SharePoint.MyAccountTargetWeb);
            }
        }
        public string LinePurchasePlansListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.LinePurchasePlansListUrl); }
        }
        public string HolidayLinesListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.HolidayLinesListUrl); }
        }
        public string Home4GPlansListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.Home4GPlansListUrl); }
        }
        public string LinePurchasePlansFamiliesListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.LinePurchasePlansFamiliesListUrl); }
        }
        public string OffersAndPromosWeb
        {
            get { return GetAppSetting(Strings.SharePoint.OffersAndPromosWeb); }
        }
        public string MyAccountEnglishWebUrl
        {
            get { return GetAppSetting(Strings.SharePoint.MyAccountEnglishWebUrl); }
        }
        public string TariffPlansListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.TariffPlansListUrl); }
        }
        public Channel GetChannel(string channelName)
        {
            Channel channel = Channel.Portal;
            if (channelName.ToLower() == "MobinilMobileApp".ToLower() || channelName.ToLower() == "MobinilAndMe".ToLower())
                channel = Channel.MobinilAndMe;
            else if (channelName.ToLower() == "SelfCareApp".ToLower() || channelName.ToLower() == "DataSelfCareApp".ToLower())
                channel = Channel.InternetSelfie;
            else if (channelName.ToLower() == "MobinilUControl".ToLower() || channelName.ToLower() == "UControl".ToLower())
                channel = Channel.UControl;
            else if (channelName.ToLower() == "MobinilShop".ToLower())
                channel = Channel.MobinilShop;
            else if (channelName.ToLower() == "OrangeMoney".ToLower())
                channel = Channel.OrangeMoney;
            else if (channelName.ToLower() == "cpapp")
                channel = Channel.CPApp;
            else
                Enum.TryParse(channelName, out channel);
            return channel;
        }
        public string EncodeHTML(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                return Microsoft.Security.Application.Encoder.HtmlEncode(Input);
            }
            return string.Empty;
        }
        public string PublicSiteURL
        {
            get
            {
                return GetAppSetting(Strings.SharePoint.PublicSiteURL);
            }
        }
        public string RemoveZeroFromDial(string dial)
        {
            return dial.StartsWith("0") ? dial.Remove(0, 1).Trim().ToString() : dial;
        }
        public string GetAppSetting(string strKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[strKey];
        }
        public string GetUserAgent()
        {
            try
            {
                return HttpContext.Current.Request.UserAgent;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }
        public string GetInternalServerIP()
        {
            try
            {
                return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }
        public string GetUserIPAddress()
        {
            try
            {
                //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a 
                //client connecting to a web server through an HTTP proxy or load balancer
                string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (ip.Contains(","))
                    ip = ip.Split(',')[0];

                return ip;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }
        public string GetSuperRechargeTransactionID(string dial)
        {
            return string.Format("SB_{0}{1}", dial, DateTime.Now.ToString(Strings.DateFormats.yyyyMMddhhmmss));
        }
        public object GetValueFromCache(string CacheKey)
        {

            if (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Cache == null)
                return null;

            if (System.Web.HttpContext.Current.Cache[CacheKey] != null)
            {
                return System.Web.HttpContext.Current.Cache[CacheKey];
            }
            return null;
        }
        public List<ExpandoObject> GetSpreadsheetData(string workSheet, string filePath)
        {
            List<ExpandoObject> data = new List<ExpandoObject>();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
            {
                // Get the worksheet we are working with
                IEnumerable<Sheet> sheets = spreadsheetDocument.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == workSheet);

                WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDocument.WorkbookPart.GetPartById(sheets.First().Id);
                Worksheet worksheet = worksheetPart.Worksheet;
                SharedStringTablePart sstPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                SharedStringTable ssTable = sstPart.SharedStringTable;
                // Get the CellFormats for cells without defined data types
                WorkbookStylesPart workbookStylesPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<WorkbookStylesPart>().First();
                CellFormats cellFormats = (CellFormats)workbookStylesPart.Stylesheet.CellFormats;

                ExtractRowsData(data, worksheet, ssTable, cellFormats);
            }

            return data;
        }
        private void ExtractRowsData(List<ExpandoObject> data, Worksheet worksheet, SharedStringTable ssTable, CellFormats cellFormats)
        {
            var columnHeaders = worksheet.Descendants<Row>().First().Descendants<Cell>().Select(c => Convert.ToString(ProcessCellValue(c, ssTable, cellFormats))).ToArray();
            var columnHeadersCellReference = worksheet.Descendants<Row>().First().Descendants<Cell>().Select(c => c.CellReference.InnerText.Replace("1", string.Empty)).ToArray();
            var spreadsheetData = from row in worksheet.Descendants<Row>()
                                  where row.RowIndex > 1
                                  select row;

            foreach (var dataRow in spreadsheetData)
            {
                dynamic row = new ExpandoObject();
                Cell[] rowCells = dataRow.Descendants<Cell>().ToArray();
                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    // Find and add the correct cell to the row object
                    Cell cell = dataRow.Descendants<Cell>().Where(c => c.CellReference == columnHeadersCellReference[i] + dataRow.RowIndex).FirstOrDefault();
                    if (cell != null)
                        ((IDictionary<String, Object>)row).Add(new KeyValuePair<String, Object>(columnHeaders[i], ProcessCellValue(cell, ssTable, cellFormats)));
                }
                data.Add(row);
            }
        }
        static Func<Cell, SharedStringTable, CellFormats, Object> ProcessCellValue = (c, ssTable, cellFormats) =>
        {
            // If there is no data type, this must be a string that has been formatted as a number
            if (c.DataType == null)
            {
                if (c.StyleIndex.HasValue)
                {
                    CellFormat cf = cellFormats.Descendants<CellFormat>().ElementAt<CellFormat>(Convert.ToInt32(c.StyleIndex.Value));
                    if (c.CellValue != null && !string.IsNullOrEmpty(c.CellValue.Text))
                    {
                        if (cf.NumberFormatId >= 0 && cf.NumberFormatId <= 13) // This is a number
                            return Convert.ToDecimal(c.CellValue.Text);
                        else if (cf.NumberFormatId >= 14 && cf.NumberFormatId <= 22) // This is a date
                            return DateTime.FromOADate(Convert.ToDouble(c.CellValue.Text));
                        else
                            return c.CellValue.Text;
                    }
                }
            }
            if (c.CellValue != null && !string.IsNullOrEmpty(c.CellValue.Text))
            {
                switch (c.DataType.Value)
                {
                    case CellValues.SharedString:
                        return ssTable.ChildElements[Convert.ToInt32(c.CellValue.Text)].InnerText;
                    case CellValues.Boolean:
                        return c.CellValue.Text == "1" ? true : false;
                    case CellValues.Date:
                        return DateTime.FromOADate(Convert.ToDouble(c.CellValue.Text));
                    case CellValues.Number:
                        return Convert.ToDecimal(c.CellValue.Text);
                    default:
                        if (c.CellValue != null)
                            return c.CellValue.Text;
                        return string.Empty;
                }
            }
            return string.Empty;
        };
        public bool IsValidDial(string dial)
        {
            Regex regexDial = new Regex(@"^01([0-2,5])\d{8}$", RegexOptions.Compiled);
            return regexDial.IsMatch(dial);
        }
        public string GetCurrentLanguage()
        {
            return Thread.CurrentThread.CurrentCulture.Parent.Name;
        }
        public void RemoveCache(string cacheKey)
        {
            System.Web.HttpContext.Current.Cache.Remove(cacheKey);
        }
        public string TreatsListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.TreatsListUrl); }
        }
        public string MyOrangeOfferAndPromotionsListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.MyOrangeOfferAndPromotionsListUrl); }
        }
        public string PortalOfferAndPromotionsListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.PortalOfferAndPromotionsListUrl); }
        }
        public string MyOrangeStartupPromoListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.MyOrangeStartupPromoListUrl); }
        }
        public string CAFPromoCodesMappingListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.CAFPromoCodesMappingListUrl); }
        }
        public string FullFillOfferItemsListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.FulFillOfferItemsListUrl); }
        }
        public string LinePurchaseAssetsListUrl
        {
            get { return GetAppSetting(Strings.SharePoint.LinePurchaseAssetsListUrl); }
        }
        public CultureInfo GetLanguageCulture(string language)
        {
            var culture = new CultureInfo(Strings.Cultures.EnUs);
            if (!string.IsNullOrWhiteSpace(language.ToLower()))
                culture = new CultureInfo(language.ToLower());
            return culture;
        }
        public string OrangeCashFawryCategories
        {
            get { return GetAppSetting(Strings.SharePoint.OrangeCashFawryCategories); }
        }
        public string OrangeCashAddMoneyContentListURL
        {
            get { return GetAppSetting(Strings.SharePoint.OrangeCashAddMoneyContentListURL); }
        }
        public bool IsValidPin(string pin)
        {
            try
            {
                Regex regexDial = new Regex(@"^\d{6}$", RegexOptions.Compiled);
                return regexDial.IsMatch(pin);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return false;
            }
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }
        public string GetFormattedFees(string lang, decimal? price)
        {
            if (!price.HasValue)
            {
                if (string.Compare(lang, Strings.Cultures.En, true) == 0)
                    return Strings.General.Unavailable;
                return Strings.General.UnavailableAr;
            }

            if (string.Compare(lang, Strings.Cultures.En, true) == 0)
                return Strings.Keys.EGP + price.Value.ToString(Strings.Numbers.DecimalFormat2);
            return price.Value.ToString(Strings.Numbers.DecimalFormat2) + Strings.Keys.EGPAr;
        }
        public string GetAbsoluteUrlOrSelf(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    return null;

                return new Uri(new Uri(GetAppSetting("PublicSiteURL")), url).AbsoluteUri;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex, false);
                return url;
            }
        }
        public T Clone<T, U>(U bucket) where T : new()
        {
            var properties = typeof(U).GetProperties();
            var obj = new T();
            foreach (var pi in properties)
                if (pi.CanWrite)
                    pi.SetValue(obj, pi.GetValue(bucket, null), null);
            return obj;
        }

        public string MobileAdministrationWebUrl
        {
            get { return GetAppSetting(Strings.SharePoint.MobileAdministrationWebUrl); }
        }
        public int DisabledTimeToReserveTicketInMinutes
        {
            get
            {
                int.TryParse(GetAppSetting(Strings.Keys.DisabledTimeToReserveTicketInMinutes), out int disabledTimeToReserveTicketInMinutes);
                return disabledTimeToReserveTicketInMinutes;
            }
        }
        public byte[] Base64DecodeinBayte(string base64EncodedData)
        {
            return System.Convert.FromBase64String(base64EncodedData);
        }
        public bool ValidateLanguageInput(string language)
        {
            if (string.IsNullOrEmpty(language))
                return false;
            if (language != Strings.Cultures.Ar && language != Strings.Cultures.En)
                return false;
            return true;
        }
        public string GetEnumDisplayName<T>(T action) where T : Enum
        {
            string name = action.ToString();
            var attribute = action.GetType().GetMember(action.ToString()).First().GetCustomAttribute<DisplayAttribute>();
            if (attribute != null)
            {
                name = attribute.Name;
            }
            return name;
        }
        public CultureInfo GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentUICulture;
        }
        public CultureInfo GetCultureInfo(string language)
        {
            var _culture = new CultureInfo(Strings.Cultures.EnUs);
            if (!string.IsNullOrWhiteSpace(language))
                _culture = new CultureInfo(language);
            return _culture;
        }
        public DateTime FormatDate(string date, string dateFormat)
        {
            DateTime.TryParseExact(date, dateFormat, GetCultureInfo(Strings.Cultures.EnUs), DateTimeStyles.None, out DateTime formattedDate);
            return formattedDate;
        }
        public List<T> GetAllCachedRecordsFromDb<T>(string cacheKey, List<T> records)
        {
            List<T> allCachedRecords = HttpRuntime.Cache.Get(cacheKey) as List<T>;
            if (allCachedRecords == null)
            {
                allCachedRecords = records;
                HttpRuntime.Cache.Insert(cacheKey, allCachedRecords,
                    null,
                    Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration);
            }
            return allCachedRecords;
        }
        public T Deserialize<T>(string json)
        {
            object obj = null;
            try
            {
                obj = new JavaScriptSerializer().Deserialize<T>(json);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp);
                return (T)obj;
            }
            return (T)obj;
        }
    }
}

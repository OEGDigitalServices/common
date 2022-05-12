using Orange.Common.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;

namespace Orange.Common.Utilities
{
    public interface IUtilities
    {
        string AdminEmail { get; }
        string MyAccountTargetWeb { get; }
        string PublicSiteURL { get; }
        string BaseSiteUrl { get; }
        string MyAccountEnglishWebUrl { get; }
        string OffersAndPromosWeb { get; }
        string LinePurchasePlansListUrl { get; }
        string HolidayLinesListUrl { get; }
        string Home4GPlansListUrl { get; }
        string LinePurchasePlansFamiliesListUrl { get; }
        string TariffPlansListUrl { get; }
        Channel GetChannel(string channelName);
        string EncodeHTML(string input);
        string GetAppSetting(string strKey);
        string GetUserAgent();
        string GetInternalServerIP();
        string GetUserIPAddress();
        List<ExpandoObject> GetSpreadsheetData(string workSheet, string filePath);
        string RemoveZeroFromDial(string dial);
        string GetCurrentLanguage();
        string GetUICurrentLanguage();
        void RemoveCache(string cacheKey);
        string TreatsListUrl { get; }
        string MyOrangeOfferAndPromotionsListUrl { get; }
        string PortalOfferAndPromotionsListUrl { get; }
        string MyOrangeStartupPromoListUrl { get; }
        string CAFPromoCodesMappingListUrl { get; }
        string FullFillOfferItemsListUrl { get; }
        string OrangeCashFawryCategories { get; }
        string LinePurchaseAssetsListUrl { get; }
        CultureInfo GetLanguageCulture(string language);
        bool IsValidPin(string pin);
        string GetSuperRechargeTransactionID(string dial);
        string MobileAdministrationWebUrl { get; }
        bool IsValidEmail(string email);
        string GetFormattedFees(string lang, decimal? price);
        object GetValueFromCache(string CacheKey);
        //object GetValueFromCache(CachingKeys CacheKey);
        string GetAbsoluteUrlOrSelf(string url);
        T Clone<T, U>(U bucket) where T : new();
        int DisabledTimeToReserveTicketInMinutes { get; }
        string OrangeCashAddMoneyContentListURL { get; }
        byte[] Base64DecodeinBayte(string base64EncodedData);
        bool IsValidDial(string dial);
        bool ValidateLanguageInput(string language);
        string GetEnumDisplayName<T>(T action) where T : Enum;
        CultureInfo GetCurrentCulture();
        CultureInfo GetCultureInfo(string language);
        DateTime FormatDate(string date, string dateFormat);
        List<T> GetAllCachedRecordsFromDb<T>(string cacheKey, List<T> records);
        List<T> GetAllCachedRecordsFromDb<T>(string cacheKey, Func<List<T>> fetchingMethod, double? daysToExpire = null);
        T Deserialize<T>(string json);
        void AddValueToCache(string CacheKey, object obj, int? Minutes = null);

        double ReturnCostInPiasters(double cost);
        string AddZeroToDial(string dial);
        string AddTwoToDial(string dial);
        string GenerateRandomNumber();
        System.Net.CredentialCache GetCredentialCache(string URL);
        string GetSoapXml<T>(T obj);
        T XMLToObject<T>(string xml) where T : class;
    }
}

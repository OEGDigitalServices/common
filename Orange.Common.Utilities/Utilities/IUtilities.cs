using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using Orange.Common.Entities;
using System.IO;

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
        void RemoveCache(string cacheKey);
        string TreatsListUrl { get; }
        string MyOrangeOfferAndPromotionsListUrl { get; }
        string PortalOfferAndPromotionsListUrl { get; }
        string MyOrangeStartupPromoListUrl { get; }
        string CAFPromoCodesMappingListUrl { get; }
        string FullFillOfferItemsListUrl { get; }
        string OrangeCashFawryCategories { get; }
        string LinePurchaseAssetsListUrl { get;  }
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
        bool ValidateLanguageInput(string language);
    }
}

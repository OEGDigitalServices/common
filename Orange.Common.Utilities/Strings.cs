using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace Orange.Common.Utilities
{
    public class Strings
    {
        public struct General
        {
            public const string Equal = "=";
            public const string And = "&";
            public const string ISO88591Encoding = "iso-8859-1";
            public const string CorporateCustomerType = "Corporate";
            public const string UnauthorizedRequest = "Unauthorised request";
            public const string True = "True";
            public const string Space = " ";
            public const string NowTextFile = "now.txt";
            public const string Unavailable = "Unavailable";
            public const string UnavailableAr = "غير متاح";
        }
        public struct MediaTypeHeaderValues
        {
            public const string Json = "application/json";
            public const string Form = "application/x-www-form-urlencoded";
        }

        public struct Keys
        {
            public const string Null = "null";
            public const string Zero = "0";
            public const string One = "1";
            public const string Two = "2";
            public const string Dot = ".";
            public const string Colon = ":";
            public const string Amount = "Amount";
            public const string UserName = "UserName";
            public const string EAIStatus = "EAIStatus";
            public const string EAIErrorCode = "EAIErrorCode";
            public const string CreatedDate = "CreatedDate";
            public const string Dial = "Dial";
            public const string Corporate = "corporate";
            public const string IsEasyLogin = "IsEasyLogin";
            public const string ChannelName = "ChannelName";
            public const string False = "false";
            public const string Lang = "Lang";
            public const string UserId = "userId";
            public const string RecipientDial = "RecipientDial";
            public const string IsForAnotherDial = "IsForAnotherDial";
            public const string dropdownindex = "dropdownindex";
            public const string NopCustomer = "Nop.customer";
            public const string Id = "ID";
            public const string True = "True";
            public const string EGP = "EGP ";
            public const string EGPAr = " جنيها";
            public const string MB = "MB";
            public const string GB = "GB";
            public const string Hour = "Hour";
            public const string DragonBytes = "DragonBytes";
            public const string s = "s";
            public const string Format01 = "{0} {1}";
            public const string Unlimited = "Unlimited";
            public const string UnlimitedAr = "غير محدودة";

            public const string DisabledTimeToReserveTicketInMinutes = "DisabledTimeToReserveTicketInMinutes";
            public const string DataGatheringSameIPIntervalInSeconds = "DataGatheringSameIPIntervalInSeconds";
            public const string DataGatheringCodeValidtyInSeconds = "DataGatheringCodeValidtyInSeconds";
        }

        public struct DateFormats
        {
            public const string hhmmsstt = "hh:mm:ss tt";
            public const string ddMMyyyy = "dd-MM-yyyy";
            public const string ddMMyyyySlash = "dd/MM/yyyy";
            public const string hhmmss = "hh:mm:ss";
            public const string ddMMyyyyhhmmssm = "ddMMyyyyhhmmssm";
            public const string ddMMyyyyhhmmtt = "ddMMyyyyhhmmtt";
            public const string yyyyMMddhhmmss = "yyyyMMddhhmmss";
            public const string dMMMMyyyy = "d MMMM yyyy";
            public const string yyyyMMddTHHmmss = "yyyy-MM-ddTHH:mm:ss";

            

            public const string YearMonthDayHourMinSec = "yyyyMMddhhmmss";
            public const string MMddyyyySlash = "MM/dd/yyyy";

        }

        public struct Cultures
        {
            public const string En = "en";
            public const string Ar = "ar";
            public const string Arabic = "Arabic";
            public const string English = "English";
            public const string EnUs = "en-us";
            public const string ArEg = "ar-eg";
        }

        public struct Regex
        {
            public const string CreditCard = @"\d{16,16}";
            public const string PaymentExpiryMonth = @"^((0[1-9])|(1[0-2]))$";
            public const string PaymentExpiryYear = @"[1-9][0-9]";
            public const string PaymentSecuirtyCode = @"^\d+$";
            public const string Numbers = @"^\d+$";
            public const string AZWithDash = @"[a-zA-Z -]*";
            public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            public const string ScratchCard = @"^\d{1,16}$";
            public const string OrangeDial = @"^01([0-2,5])\d{8}$";
            public const string Mobile = "^[0-9]{11}$";
            public const string Mobinil = "^012[0-9]{8}$";
            public const string NationalId = "^[2-3]{1}[0-9]{13}$";
        }
        public struct SharePoint
        {
            public const string BaseSiteUrl = "BaseSiteUrl";
            public const string MyAccountTargetWeb = "MyAccountTargetWeb";
            public const string MyAccountEnglishWebUrl = "MyAccountEnglishWebUrl";
            public const string EntertainmentEnglishWebUrl = "EntertainmentEnglishWebUrl";
            public const string BeINPackagesListURL = "BeINPackagesListURL";
            public const string BeINChannelsListURL = "BeINChannelsListURL";
            public const string OffersAndPromosWeb = "OffersAndPromosWeb";
            public const string PublicSiteURL = "PublicSiteURL";
            public const string TariffPlansListUrl = "TariffPlansListUrl";
            public const string TreatsListUrl = "TreatsListUrl";
            public const string MyOrangeOfferAndPromotionsListUrl = "MyOrangeOffersAndPromotionsListUrl";
            public const string PortalOfferAndPromotionsListUrl = "OffersAndPromosListUrl";
            public const string LinePurchasePlansListUrl = "LinePurchasePlansListUrl";
            public const string Home4GPlansListUrl = "Home4GPlansListUrl";
            public const string LinePurchasePlansFamiliesListUrl = "LinePurchasePlansFamiliesListUrl";
            public const string HolidayLinesListUrl = "HolidayLinesListUrl";
            public const string SocialMediaSharingImagesURL = "SocialMediaSharingImagesURL";

            public const string EnglishWebUrl = "EnglishWebUrl";
            public const string CAFCitiesListUrl = "CAFCitiesListUrl";
            public const string CAFServicesListUrl = "CAFServicesListUrl";
            public const string CAFStoreBranchesListUrl = "CAFStoreBranchesListUrl";
            public const string MyOrangeStartupPromoListUrl = "MyOrangeStartupPromoListUrl";
            public const string CAFPromoCodesMappingListUrl = "CAFPromoCodesMappingListUrl";
            public const string FulFillOfferItemsListUrl = "FulFillOfferItemsListUrl";
            public const string OrangeCashFawryCategories = "OrangeCashFawryGetCategories";
            public const string LinePurchaseAssetsListUrl = "LinePurchaseAssetsListUrl";

            public const string MessageTemplatesListUrl = "MessageTemplatesListUrl";
            public const string AvailableVoucherListUrl = "AvailableVoucherListUrl";
            public const string MobileAdministrationWebUrl = "MobileAdministrationWebUrl";
            public const string OrangeCashAddMoneyContentListURL = "OrangeCashAddMoneyContentListURL";
            
            public const string ServicesEnglishWebUrl = "ServicesEnglishWebUrl";
            public const string DataGatheringCitiesListUrl = "DataGatheringCitiesListUrl";
            public const string DataGatheringDistrictsListUrl = "DataGatheringDistrictsListUrl";
            public const string DataGatheringFilesListUrl = "DataGatheringFilesListUrl";
            public const string DataGatheringFilesListName = "DataGatheringFilesListName";

			public const string JoinOrangeRequestReportListUrl = "JoinOrangeRequestReportListUrl";

		}
        public struct Mails
        {
            public const string AdminEmail = "AdminEmail";
            public const string RenewExtensionCCEmailAddress = "RenewTPExtensionCCEmailAddress";
            public const string SiteUrl = "[%SiteUrl%]";
            public const string BodyMailReplace = "[%Body%]";
            public const string DSLUserName = "[%DSLUserName%]";
            public const string PaymentAmount = "[%PaymentAmount%]";
            public const string PaidAmount = "[%PaidAmount%]";
            public const string TransactionNumber = "[%TransactionNumber%]";
            public const string Date = "[%Date%]";
            public const string DateTime = "[%DateTime%]";
            public const string DateTimeFormat = "dd-MM-yyyy hh:mm:ss tt";
            public const string BillPaymentFailedTransactions = "BillPayment-Failed-Transactions-To (";
            public const string MailFrom = "MailFrom";
            public const string CCMail = "CCMail";
            public const string MailTo = "MailTo";
            public const string Time = "[%Time%]";
            public const string MainMailContainer = "MainMailContainer";
            public const string ECareResource = "ECareResource";
            public const string AddExtensionCCEmailAddress = "AddExtensionCCEmailAddress";
            public const string MobileNumber = "[%MobileNumber%]";
            public const string CommonGlobalResource = "Common";
            public const string ScratchCardNumber = "[%VoucherNumber%]";
            public const string LinePurchaseBCCMail = "attackteammnp-eg@Mobinil.com";

        }
        public struct Plans
        {
            public const string AdminEmail = "AdminEmail";
            public const string PlanID = "PlanID";
            public const string Title = "Title";
            public const string TitleAr = "TitleAr";
            public const string Price = "Price";
            public const string AllDayQuota = "AllDayQuota";
            public const string SpecialTimeQuota = "SpecialTimeQuota";
            
            public const string InclusiveGBs = "InclusiveGBs";
            public const string RouterPrice = "RouterPrice";
            public const string RouterInstallment = "RouterInstallment";
            public const string InstallmentPerMonth = "InstallmentPerMonth";
            public const string NumberOfMonths = "NumberOfMonths";
        }
        public struct Numbers
        {
            public const string Zero = "0";
            public const string UpperCaseHexaDecimalChar = "X2";
            public const string DecimalFormat = "0.#";
            public const string MoneyFormat = "0.00";
            public const string DecimalFormat2 = "0.##";
        }
        public struct CachingIntervals
        {
            public const double ThirtyMinutes = 0.5;
            public const int Hour = 1;
            public const int TwoHours = 1;
            public const int ThreeHours = 3;
            public const int FourHours = 4;
            public const int FiveHours = 5;
            public const int SixHours = 6;
            public const int SevenHours = 7;
            public const int EightHours = 8;
            public const int NineHours = 9;
            public const int TenHours = 10;
            public const int ElevenHours = 11;
            public const int TwelveHours = 12;
            public const int Day = 24;
            public const int TwoDays = 48;
            public const int TenDays = 240;
            public const int Month = 720;
        }
        public struct CachingKeys
        {
            public const string GetPrepaidInfo = "GetPrepaidInfo";
            public const string SharePointLinePurchasePlans = "_all_Line__Pur_chase_P_LA_NS_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointHolidayLines = "_ALL_HOLI__DAY_LI_N_S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointLinePurchasePlansFamilies = "_all_Line__Pur_chase_P_LA_NS__F_MIl_ies_CACH_EK_EY_{0}{1}{2}";
            public const string CreditCardTypes = "CreditCardTypes";
            public const string MerchantInfo = "MerchantInfo";
            public const string GetDialRegisteredInfo = "GetDialRegisteredInfo_";
            public const string SharePointBeINPackages = "_Be__IN_Pac_ckages_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointBeINChannels = "_Be_In_Chann_els_CACH_EK_EY_{0}{1}{2}";
            public const string GetFamilyInfo = "GetFamilyInfo";
            public const string FamilyManageQuota = "FamilyManageQuota";
            public const string FamilyTransferQuota = "FamilyTransferQuota";
            public const string SharePointTariffs = "_all__P_LA_NS_CACH_EK_EY_{0}{1}{2}{3}";
            public const string SharePointTreatsDescription = "_Get__TR__E _ATS_DES_CR_IP__TI _ON_CACH_EK_EY_{0}{1}{2}";
            public const string MyOrangeSPOffersAndPromotions = "__Offers___And__Promotions__Cache______Key____{0}{1}{2}";
            public const string OrangeHomePageBanner = "__Orange___Banner__Cache______Key____{0}{1}{2}";
            public const string IWRConsumptionBanner = "__IWR___Consumption__Banner__Cache______Key____{0}{1}{2}";
            public const string IWRFooterBanner = "__IWR___Footer__Banner__Cache______Key____{0}{1}{2}";
            public const string TariffPlans = "__Tariff___Plans__Cache______Key____{0}{1}{2}";
            public const string PortalSPOffersAndPromotions = "OFFERS_CACHE_KEY{0}{1}_{2}_{3}";
            public const string SharePointSocialMediaImages = "_So__ci__al_Med__ia__Ima__gs_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointCAFCities = "_ca___f_C__i__tie_S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointCAFServices = "_ca___f_S_e_r__v__ice_S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointCAFStoreBranches = "_ca___f_S_t_or__e___S_Br_anch__e__S_CACH_EK_EY_{0}{1}{2}";
            public const string MyOrangeStartupPromo = "__Sta__rt__up__Pr_o_mo___Cache______Key____{0}{1}{2}";
            public const string MyOrangeCAFPromoCodesMapping = "__PR__OM_O__CO__D_E_MA_P_P_I____N__G___Cache______Key____{0}{1}";
            public const string FullFillOfferItems = "__Fulfill____Offers_____Cache___Key____{0}{1}{2}";
            public const string FindAllInYourStores = "__Find___All__InYour__Stores__Cache___Key____{0}{1}{2}";
            public const string HomePageFeatures = "__Home___Page__Features____Cache___Key____{0}{1}{2}";
            public const string ServicesForYou = "__Services___For__You__Cache______Key____{0}{1}{2}";
            public const string EssentialServices = "__Essential___Services__Cache______Key____{0}{1}{2}"; 
            public const string GetConnected = "__Get___Connected__Cache______Key____{0}{1}{2}"; 
            public const string OrangeApps = "__Orange___Apps__Cache______Key____{0}{1}{2}";
            public const string LandingPagePartners = "__Landing___Page___Partners___Cache______Key____{0}{1}{2}";
            public const string FirstClassPartners = "__First___Class___Partners___Cache______Key____{0}{1}{2}";
            public const string SpecialGenericContent = "__Special___Generic___Content___Cache______Key____{0}{1}{2}";
            public const string SpecialCategories = "__Special___Categories___Cache______Key____{0}{1}{2}"; 
            public const string TiersCategories = "__Tiers___Categories___Cache______Key____{0}{1}{2}"; 
            public const string OrangeCashFawryCategories = "__ORA__AN__GE___C_A_S_H__FAW__RY__CAT__EG_O_RI___ES__Cache______Key____{0}{1}";
            public const string LinePurchaseAssets = "__Line__Purchase_____Assets_____Cache___Key____{0}{1}{2}";
            public const string ServicesCards = "__Services___Services__Cache______Key____{0}{1}{2}";
            public const string PagesTitles = "__Pages___tiTlEs__GenErIc_CaChe_____Key____{0}{1}{2}";
            public const string ClientCache = "_Client_Cache_";
            public const string LoadActivatedBuckets = "_L___o_a___d__A_c_t___iv__ate__d___Bu__cke_ts{0}{1}";
            public const string RasseenyContents = "__Rasseeny____Contents_____Cache___Key____{0}{1}{2}";
            public const string RasseenyServiceClasses = "__Rasseeny____Service_____Classes_____Cache___Key____{0}{1}";
            public const string SharePointStoreLocatorCities = "_st_Ore_loca_TO_r_c_i__tie_S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointStoreLocatorAreas = "_st_Ore_loca_TO_Ar_ea__s__EA_S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointStoreLocatorServices = "_st_Ore_loca_TO_r__S_e_r__v__ice_S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointStoreLocatorBranches = "_st_Ore_loca_TO_r_Br_aNch__e__S_CACH_EK_EY_{0}{1}{2}";
            public const string SharePointStoreLocatorBanners = "_st_Ore_loca_TO_r_Ba_NN__e__R___s_CACH_EK_EY_{0}{1}{2}";
            public const string EntertainmentAndServices = "__Entertainment___And__Services_CaChe_____Key____{0}{1}{2}"; 
            public const string HomePageBannerSidebar = "__Home___Page__Banner__Sidebar_CaChe_____Key____{0}{1}{2}";
            public const string MessagesTemplatesKey = "__M_E_S_S_A_G_E_S__TEM_P__L__AT__E_C_A_C_H_E__K_E_Y__{0}{1}{2}";
            public const string AvailableVoucherCacheKey = "__AV_A__I_LA__B_LE__V_OU__C_H__ER__{0}{1}{2}";
            public const string CheckDialIsOrange = "CheckDialIsOrange__";
            public const string NewUpsellingBanner = "__N_E_W__U_P_S_E_L_L_I_N_G__B_A_N_N_E_R__C_A_C_H_E_K_E_Y__{0}{1}{2}";
            public const string DataGatheringCities = "__Data____Gathering__Cities___Cache___Key____{0}{1}{2}{3}";
            public const string DataGatheringDistricts = "__Data____Gathering__Districts___Cache___Key____{0}{1}{2}{3}";
            public const string DataGatheringAttachments = "__Data____Gathering__Attachments___Cache___Key____{0}{1}{2}{3}";
            public const string OrangeCashWalletConfiguration = "__Oran__ge_Cash___AddMon______eyContent_CaChe_____Key____{0}{1}{2}";

			public const string ShopHomepageMenu = "__Shop___Home___Page__Menu____Cache___Key____{0}{1}{2}";

			public const string JoinOrangeRequestReport = "__Join____Orange__Request___Report___Cache___Key____{0}{1}{2}";

		}
		public struct Entertainment
        {
            public const string ImageUrl = "ImageURL";
            public const string Id = "ID";
            public const string Channels = "Channels";
            public const string PackageId = "PackageId";
            public const string Price = "Price";
            public const string Title = "Title";
            public const string TitleAr = "TitleAr";
            public const string DescriptionEn = "DescriptionEn";
            public const string DescriptionAr = "DescriptionAr";
            public const string TAndCEn = "TAndCEn";
            public const string TAndCAr = "TAndCAr";
            public const string SubscriptionType = "SubscriptionType";
            public const string SubscriptionTypeAr = "SubscriptionTypeAr";
            public const string Size = "Size";
            public const string FileRef = "FileRef";
            public const string WebViewURL = "WebViewURL";

        }
        public struct CAF
        {
            public const string CityEnglishName = "Title";
            public const string CityArabicName = "ArabicTitle";
            public const string ServiceEnglishName = "Title";
            public const string ServiceArabicName = "ArabicTitle";
            public const string StoreEnglishName = "Title";
            public const string StoreArabicName = "ArabicTitle";
            public const string StoreEnglishAddress = "Address";
            public const string StoreArabicAddress = "ArabicAddress";
            public const string StoreEnglishHour = "WorkingHours";
            public const string StoreArabicHour = "ArabicWorkingHours";
            public const string Latitude = "Latitude";
            public const string Longitude = "Longitude";
            public const string StoreCity = "StoreCity";
            public const string StoreType = "StoreType";
            public const string TypeServices = "TypeServices";
            public const string StoreServices = "StoreServices";
        }

        public struct LoggingMessages
        {
            public const string Format = "{0} :: {1} :: {2}";
            public const string FormatWithEntity = "{0} :: {1} => Entity: {2}";
            public const string FormatWithEntityAndID = "{0} :: {1} => Entity: {2} :: ID: {3}";
        }
        public struct StoreLocator
        {
            public const string ID = "ID";
            public const string CityEnglishName = "StoreAreaEn";
            public const string CityArabicName = "StoreAreaAr";
            public const string AreaEnglishName = "StoreAreaEn";
            public const string AreaArabicName = "StoreAreaAr";
            public const string ServiceEnglishName = "ServiceNameEn";
            public const string ServiceArabicName = "ServiceNameAr";
            public const string StoreEnglishName = "StoreNameEn";
            public const string StoreArabicName = "StoreNameAr";
            public const string StoreEnglishAddress = "StoreAddressEn";
            public const string StoreArabicAddress = "StoreAddressAr";
            public const string StoreEnglishHour = "StoreHoursEn";
            public const string StoreArabicHour = "StoreHours";
            public const string Latitude = "Latitude";
            public const string Longitude = "Longitude";
            public const string StoreCity = "StoreCity";
            public const string StoreArea = "StoreArea";
            public const string AreaCity = "AreaCity";
            public const string StoreType = "StoreType";
            public const string StoreServices = "StoreServices";
            public const string Telephone = "Telephone";
            public const string Fax = "Fax";
        }

        public struct Separators
        {
            public const string SemiColon = ";";
            public const string SemiColonHash = ";#";
        }
        public struct ErrorDescriptions
        {
            public const string NullResponse = "Null Response from Secure API";
            public const string DialIsNull = "Dial is null";
            public const string DialIsNonOrange = "Dial is non orange";
            public const string Success = "Success";
            public const string DialIsCorporate = "Dial Is Corporate";
            public const string GetDialRegisteredInfoError = "GetDialRegisteredInfoError => {0}";
            public const string DialIsInvalid = "Dial is invalid {0}";
            public const string LanguageIsNull = "Language Is Null";
            public const string ServiceException = "Exception {0}";
            public const string LanguageIsInvalid = "Language is invalid";
            public const string PinIsNullOrEmpty = "Pin is null or empty";
            public const string PinIsNotVaild = "Pin is not vaild";
            public const string AccountIsLocked = "Account is locked";
            public const string WalletBalanceUnspecifiedError = "Wallet balance inquiry error description => {0}";
            public const string TechnicalError = "TechnicalError";
            public const string CustomerNotEligible = "Customer Not Eligible";
            public const string SuccessHasNoCredit = "Success has no credit";
            public const string ErrorReceivedFromIN = "Error received from IN";
            
            public const string AccountNumberAndPasswordIsNull = "Account number and password is null";
            public const string AccountNumberIsNullOrEmpty = "Account number is null or empty";
            public const string SMSNotSent = "SMS not sent successfully";
            public const string SMSCodeIsInValid = "SMS code is invalid";
            public const string InvalidCode = "SMS code is invalid";
            public const string CodeIsNullOrEmpty = "Code is null or empty";
            public const string UserInfoIsNull = "User Info is null or empty";
            public const string BirthDateIsNull = "BirthDate is null or empty";
            public const string IdentityTypeIsNull = "Identity Type is null or empty";
            public const string CityIsNull = "City is null or empty";
            public const string DistrictIsNull = "District is null or empty";
            public const string FullNameIsNull = "Full Name is null or empty";
            public const string StreetIsNull = "Street is null or empty";
            public const string IdentificationNumberIsNull = "IdentificationNumber is null or empty";
            public const string MotherNameIsNull = "Mother Name is null or empty";
            public const string IdentificationNumberIsInvalid = "IdentificationNumber is Invalid {0}";
            public const string AttachmentIsNull = "Attachment is null";
            public const string AttachmentNotSaved = "Attachment not saved";

            

        }
        public struct SharePointQuery
        {
            public const string OrderBy = "<OrderBy><FieldRef Name='{0}' Ascending='{1}'/></OrderBy>";
        }
        public struct URLHost
        {
            public const string Services = "services.orange.eg";
            public const string Orange = "www.orange.eg";
        }
        public struct Methods
        {
            public const string RedeemCafPromoCode = "RedeemCafPromoCode";
            public const string AddComplaint = "AddComplaint";
            public const string SuperRechargeCheckEligibility = "SuperRechargeCheckEligibility";
            public const string SuperRechargeProvisionPromo = "SuperRechargeProvisionPromo";
        }
        public struct ConvenienceFeeType
        {
            public const string Percentage = "percentage";
            public const string Fixed = "fixed";
        }
        public struct DateLanguage
        {
            public const string Arabic = "ar-AE";
        }
        public struct DialPackages
        {
            public const string P108 = "108";
            public const string P109 = "109";
            public const string P97 = "97";
            public const string P152 = "152";
            public const string P87 = "87";
        }
        public struct SMS
        {
            public const string DataGatheringAlias = "Orange";
            public const string SMSTitle = "SMSTitle";
            public const string Arabic = "Arabic";
            public const string FormateBody = "{0:X04}";
            public const string SMSUsername = "SMSUsername";
            public const string InfotainmentSMSUserName = "InfotainmentSMSUserName";
            public const string InfotainmentSMSPassword = "InfotainmentSMSPassword";
            public const string SMSpassword = "SMSpassword";

        }
    }
}

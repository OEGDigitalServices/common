using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace Orange.Common.Utilities
{
    public interface ISharePointUtilities
    {
        string BaseSiteUrl { get; }
        string EnglishWebUrl { get; }
        string EntertainmentEnglishWebUrl { get; }
        string ServicesEnglishWebUrl { get; }
        string DataGatheringCitiesListUrl { get; }
        string DataGatheringDistrictsListUrl { get; }
        string DataGatheringFilesListUrl { get; }
        string DataGatheringFilesListName { get; }
        string BeINPackagesListURL { get; }
        string BeINChannelsListURL { get; }
        string SocialMediaSharingImagesURL { get; }
        string CAFCitiesListUrl { get; }
        string CAFServicesListUrl { get; }
        string CAFStoreBranchesListUrl { get; }
        SPUser GetCurrentUser();
        bool IsCurrentUserInGroup(string groupName);
        bool IsSiteAdmin();
        object GetFieldValue(SPListItem listItem, string fieldName);
        object GetFieldLookupValue(SPListItem listItem, string fieldName);
        object GetFieldLookupId(SPListItem listItem, string fieldName);
        string UploadFileToMossList(string TargetSite, string TargetWeb, string ListURL, string FolderURL, byte[] fileContents, string FileName);
        object GetFieldValueImage(SPListItem listItem, string fieldName, out string imageAlt);
        object GetFieldValueImage(SPListItem listItem, string fieldName, out string imageAlt, out string HyperLink);
        object GetFieldValueImage(SPListItem listItem, string fieldName);
        List<T> CheckIfObjectIsCached<T>(string cacheKey);
        T CheckIfObjectIsCachedWithSingleReturn<T>(string cacheKey);
        void CacheList<T>(List<T> items, string cacheKey, double hours);
        void CacheObject<T>(T item, string cacheKey, double hours);
        void CacheList<T>(T item, string cacheKey, double hours);
        T ConnectExternalToSharePoint<T>(Func<SPWeb, string, T> item, string siteUrl, string webUrl, string language);
        T ConnectExternalToSharePoint<T>(Func<SPWeb, string, List<string>, T> item, string siteUrl, string webUrl, List<string> paramsList);
        T ConnectExternalToSharePoint<T>(Func<SPWeb, string, bool, T> item, string siteUrl, string webUrl, string language, bool showToUser);
        T ConnectExternalToSharePoint<T>(Func<SPWeb, string, string, T> item, string siteUrl, string webUrl, string language, string offerId);
        T ConnectExternalToSharePoint<T>(Func<SPWeb, string, int, T> item, string siteUrl, string webUrl, string language, int parentOfferId);
        T ConnectExternalToSharePoint<T>(Func<SPWeb, T> item, string siteUrl, string webUrl);
        bool GetItemsList(SPWeb context, out SPListItemCollection items, String listURL);
		string JoinOrangeRequestReportListUrl { get; }
	}
}

using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing.Fields;
using Microsoft.SharePoint.Security;
using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Orange.Common.Utilities
{
    public class SharePointUtilities : ISharePointUtilities
    {
        private readonly ILogger _logger;
        private readonly ICachingUtilities _cachingUtilities;
        private readonly IUtilities _utilities;
        public SharePointUtilities(ILogger logger, IUtilities utilities, ICachingUtilities cachingUtilities)
        {
            _logger = logger;
            _utilities = utilities;
            _cachingUtilities = cachingUtilities;
        }

        public string BaseSiteUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.BaseSiteUrl);
            }
        }

        public string EnglishWebUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.EnglishWebUrl);
            }
        }

        public string EntertainmentEnglishWebUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.EntertainmentEnglishWebUrl);
            }
        }
        public string BeINPackagesListURL
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.BeINPackagesListURL);
            }
        }
        public string BeINChannelsListURL
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.BeINChannelsListURL);
            }
        }
        public string SocialMediaSharingImagesURL
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.SocialMediaSharingImagesURL);
            }
        }

        public string CAFCitiesListUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.CAFCitiesListUrl);
            }
        }

        public string CAFServicesListUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.CAFServicesListUrl);
            }
        }

        public string CAFStoreBranchesListUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.CAFStoreBranchesListUrl);
            }
        }

        public string ServicesEnglishWebUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.ServicesEnglishWebUrl);
            }
        }

        public string DataGatheringCitiesListUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.DataGatheringCitiesListUrl);
            }
        }

        public string DataGatheringDistrictsListUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.DataGatheringDistrictsListUrl);
            }
        }
        public string DataGatheringFilesListUrl
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.DataGatheringFilesListUrl);
            }
        }
        public string DataGatheringFilesListName
        {
            get
            {
                return _utilities.GetAppSetting(Strings.SharePoint.DataGatheringFilesListName);
            }
        }

		public string JoinOrangeRequestReportListUrl
		{
			get
			{
				return _utilities.GetAppSetting(Strings.SharePoint.JoinOrangeRequestReportListUrl);
			}
		}



		/// <summary>
		/// Gets the current user.
		/// </summary>
		/// <returns></returns>
		[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public SPUser GetCurrentUser()
        {
            try
            {
                if (SPContext.Current.Web != null)
                {
                    if (SPContext.Current.Web.CurrentUser != null)
                    {
                        return SPContext.Current.Web.CurrentUser;
                    }
                }
                return null;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }
        /// <summary>
        /// Determines whether [is current user in group] [the specified group name].
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public bool IsCurrentUserInGroup(string groupName)
        {
            bool result = false;
            if (SPContext.Current.Web != null)
            {
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    SPUser user = SPContext.Current.Web.CurrentUser;
                    for (int i = 0; i < user.Groups.Count; i++)
                    {
                        if (user.Groups[i].Name.ToLower() == groupName.ToLower())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Determines whether [is site admin].
        /// </summary>
        /// <returns></returns>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public bool IsSiteAdmin()
        {
            try
            {
                if (SPContext.Current.Web != null)
                {
                    if (SPContext.Current.Web.CurrentUser != null)
                    {
                        if (SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return false;
            }
        }
        /// <summary>
        /// Gets the moss list.
        /// </summary>
        /// <param name="taregtSite">The taregt site.</param>
        /// <param name="taregtWeb">The taregt web.</param>
        /// <param name="listUrl">The list URL.</param>
        /// <returns></returns>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public SPList GetMossList(string taregtSite, string taregtWeb, string listUrl)
        {
            try
            {

                SPList listItems = null;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(taregtSite))
                    {
                        using (SPWeb web = site.OpenWeb(taregtWeb))
                        {
                            listItems = web.GetList(listUrl);
                        }
                    }
                }
             );
                return listItems;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message + "&& Taregt Web=>" + taregtWeb + "&&List URL =>" + listUrl, exp, false);
                return null;
            }
        }
        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <param name="listItem">The list item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public object GetFieldValue(SPListItem listItem, string fieldName)
        {
            string text = string.Empty;
            if (fieldName == string.Empty)
            {
                return text;
            }
            try
            {
                object myObj = listItem[fieldName];
                if (listItem[fieldName].GetType() == typeof(ImageFieldValue))
                    return myObj != null ? ((ImageFieldValue)myObj).ImageUrl : string.Empty;
                return myObj != null ? myObj : string.Empty;
            }
            catch
            {
                return string.Empty;
            }

        }
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public object GetFieldLookupValue(SPListItem listItem, string fieldName)
        {
            string text = string.Empty;
            if (fieldName == string.Empty)
            {
                return text;
            }
            try
            {
                var field = listItem.Fields.GetField(fieldName);
                if (field.Type != SPFieldType.Lookup)
                {
                    return GetFieldValue(listItem, fieldName);
                }
                SPFieldLookupValue lookupField = new SPFieldLookupValue(listItem[fieldName].ToString());
                return lookupField.LookupValue != null ? lookupField.LookupValue : string.Empty;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public object GetFieldLookupId(SPListItem listItem, string fieldName)
        {
            string text = string.Empty;
            if (fieldName == string.Empty)
            {
                return text;
            }
            try
            {
                var field = listItem.Fields.GetField(fieldName);
                if (field.Type != SPFieldType.Lookup)
                {
                    return GetFieldValue(listItem, fieldName);
                }
                SPFieldLookupValue lookupField = new SPFieldLookupValue(listItem[fieldName].ToString());
                return lookupField.LookupId;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public string UploadFileToMossList(string TargetSite, string TargetWeb, string ListURL, string FolderURL, byte[] fileContents, string FileName)
        {
            SPFile file = null;
            SPFolder folder = null;
            string FileURL = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(ListURL))
                {
                    using (SPWeb Web = site.OpenWeb(TargetWeb))
                    {
                        site.AllowUnsafeUpdates = true;
                        Web.AllowUnsafeUpdates = true;
                        folder = Web.GetFolder(FolderURL);
                        file = folder.Files.Add(FileName, fileContents, true);
                        file.Update();
                        site.AllowUnsafeUpdates = false;
                        Web.AllowUnsafeUpdates = false;
                        FileURL = file.Url;
                    }
                }
            });
            return FileURL;
        }
        /// <summary>
        /// Gets the field value image.
        /// </summary>
        /// <param name="listItem">The list item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public object GetFieldValueImage(SPListItem listItem, string fieldName, out string imageAlt)
        {
            string text = string.Empty;
            imageAlt = string.Empty;
            if (fieldName == string.Empty)
            {
                return text;
            }
            try
            {
                object myObj = listItem[fieldName];
                if (listItem[fieldName].GetType() == typeof(Microsoft.SharePoint.Publishing.Fields.ImageFieldValue))
                {
                    imageAlt = ((myObj != null) ? ((ImageFieldValue)myObj).AlternateText : string.Empty);
                    return ((myObj != null) ? ((ImageFieldValue)myObj).ImageUrl : string.Empty);
                }
                else
                {
                    return ((myObj != null) ? myObj : string.Empty);
                }
            }
            catch
            {
                return string.Empty;
            }

        }
        public object GetFieldValueImage(SPListItem listItem, string fieldName, out string imageAlt, out string HyperLink)
        {
            string text = string.Empty;
            imageAlt = string.Empty;
            HyperLink = string.Empty;
            if (fieldName == string.Empty)
            {
                return text;
            }
            try
            {
                object myObj = listItem[fieldName];
                if (listItem[fieldName].GetType() == typeof(Microsoft.SharePoint.Publishing.Fields.ImageFieldValue))
                {
                    imageAlt = ((myObj != null) ? ((ImageFieldValue)myObj).AlternateText : string.Empty);
                    HyperLink = ((myObj != null) ? ((ImageFieldValue)myObj).Hyperlink : string.Empty);
                    return ((myObj != null) ? ((ImageFieldValue)myObj).ImageUrl : string.Empty);
                }
                else
                {
                    return ((myObj != null) ? myObj : string.Empty);
                }
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// Gets the field value image.
        /// </summary>
        /// <param name="listItem">The list item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public object GetFieldValueImage(SPListItem listItem, string fieldName)
        {
            string text = string.Empty;
            if (fieldName == string.Empty)
            {
                return text;
            }
            try
            {
                object myObj = listItem[fieldName];
                if (listItem[fieldName].GetType() == typeof(Microsoft.SharePoint.Publishing.Fields.ImageFieldValue))
                {
                    return ((myObj != null) ? ((ImageFieldValue)myObj).ImageUrl : string.Empty);
                }
                else
                {
                    return ((myObj != null) ? myObj : string.Empty);
                }
            }
            catch
            {
                return string.Empty;
            }

        }
        public bool GetItemsList(SPWeb context, out SPListItemCollection items, String listURL)
        {
            items = null;
            var list = context.GetList(listURL);
            if (list == null)
                return false;
            items = list.GetItems();
            return true;
        }
        public T ConnectExternalToSharePoint<T>(Func<SPWeb, string, T> item, string siteUrl, string webUrl, string language)
        {
            SPWeb web = null;
            T returnVal = default(T);
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite site = new SPSite(siteUrl);
                    using (web = site.OpenWeb(webUrl))
                    {
                        returnVal = item(web, language);
                    }
                });
                return returnVal;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
            finally
            {
                web?.Dispose();
            }
        }
        public T ConnectExternalToSharePoint<T>(Func<SPWeb, string, List<string>, T> item, string siteUrl, string webUrl, List<string> paramsList)
        {
            SPWeb web = null;
            T returnVal = default(T);
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite site = new SPSite(siteUrl);
                    using (web = site.OpenWeb(webUrl))
                    {
                        returnVal = item(web, webUrl, paramsList);
                    }
                });
                return returnVal;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
            finally
            {
                web?.Dispose();
            }
        }

        public T ConnectExternalToSharePoint<T>(Func<SPWeb, string, bool, T> item, string siteUrl, string webUrl, string language, bool showToUser)
        {
            SPWeb web = null;
            T returnVal = default(T);
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite site = new SPSite(siteUrl);
                    using (web = site.OpenWeb(webUrl))
                    {
                        returnVal = item(web, webUrl, showToUser);
                    }
                });
                return returnVal;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
            finally
            {
                web?.Dispose();
            }
        }
        public T ConnectExternalToSharePoint<T>(Func<SPWeb, string, string, T> item, string siteUrl, string webUrl, string language, string offerId)
        {
            SPWeb web = null;
            T returnVal = default(T);
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite site = new SPSite(siteUrl);
                    using (web = site.OpenWeb(webUrl))
                    {
                        returnVal = item(web, language, offerId);
                    }
                });
                return returnVal;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
            finally
            {
                web?.Dispose();
            }
        }
        public T ConnectExternalToSharePoint<T>(Func<SPWeb, string, int, T> item, string siteUrl, string webUrl, string language, int parentOfferId)
        {
            SPWeb web = null;
            T returnVal = default(T);
            try
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    SPSite site = new SPSite(siteUrl);
                    using (web = site.OpenWeb(webUrl))
                    {
                        returnVal = item(web, language, parentOfferId);
                    }
                });
                return returnVal;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
            finally
            {
                web?.Dispose();
            }
        }
        public T ConnectExternalToSharePoint<T>(Func<SPWeb, T> item, string siteUrl, string webUrl)
        {
            SPWeb web = null;
            T returnVal = default(T);
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite site = new SPSite(siteUrl);
                    using (web = site.OpenWeb(webUrl))
                    {
                        returnVal = item(web);
                    }
                });
                return returnVal;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
            finally
            {
                web?.Dispose();
            }
        }

        public List<T> CheckIfObjectIsCached<T>(string cacheKey)
        {
            object temp = _cachingUtilities.GetValueFromCache(cacheKey);
            return (List<T>)temp;
        }
        public T CheckIfObjectIsCachedWithSingleReturn<T>(string cacheKey)
        {
            object temp = _cachingUtilities.GetValueFromCache(cacheKey);
            return (T)temp;
        }
        public void CacheList<T>(List<T> items, string cacheKey, double hours)
        {

            if (items.Count > 0)
            {
                _cachingUtilities.AddValueToCache(cacheKey, items, hours);
            }
        }
        public void CacheList<T>(T item, string cacheKey, double hours)
        {
            _cachingUtilities.AddValueToCache(cacheKey, item, hours);
        }
        public void CacheObject<T>(T item, string cacheKey, double hours)
        {
            if (item == null)
                return;
            _cachingUtilities.AddValueToCache(cacheKey, item, hours);
        }
    }
}

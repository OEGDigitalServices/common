﻿using Orange.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Web.Http.Controllers;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Orange.Common.Utilities
{
    public class ServicesUtilities : IServicesUtilties
    {

        #region Props

        private readonly ILogger _logger;
        private readonly IUtilities _utilities;

        #endregion

        #region Ctors

        public ServicesUtilities(ILogger logger, IUtilities utilities)
        {
            _logger = logger;
            _utilities = utilities;
        }

        #endregion

        #region Methods

        public HeaderData GetHeaderData(HttpRequest request)
        {
            var headerData = new HeaderData();
            if (request.Headers[Strings.Keys.Language] != null)
                headerData.Language = request.Headers[Strings.Keys.Language];
            if (request.Headers[Strings.Keys.Token] != null)
                headerData.Token = request.Headers[Strings.Keys.Token];

            return headerData;
        }

        public HeaderData GetHeaderData(HttpActionContext actionContext)
        {
            var headerData = new HeaderData();
            var language = actionContext.Request.Headers.FirstOrDefault(a => a.Key == Strings.Keys.Language || a.Key == Strings.Keys.Language.ToLower());
            var token = actionContext.Request.Headers.FirstOrDefault(a => a.Key == Strings.Keys.Token || a.Key == Strings.Keys.Token.ToLower());
            if (!language.Equals(new KeyValuePair<string, IEnumerable<string>>()))
                headerData.Language = language.Value.FirstOrDefault();
            if (!token.Equals(new KeyValuePair<string, IEnumerable<string>>()))
                headerData.Token = token.Value.FirstOrDefault();
            return headerData;
        }

        public void FillDataFromFormValues(BodyInput input, HttpRequest request)
        {
            var formValues = GetFormData(request);
            input.Dial = formValues.Dial;
            input.Password = formValues.Password;
        }
        private BodyInput GetFormData(HttpRequest request)
        {
            var input = new BodyInput();
            if (request.Form[Strings.Keys.Dial] != null)
                input.Dial = request.Form[Strings.Keys.Dial];
            if (request.Form[Strings.Keys.Password] != null)
                input.Password = request.Form[Strings.Keys.Password];
            return input;
        }

        public void FillTokenAndLanguageFromHeadersValues(BodyInput input, HttpRequest request)
        {
            try
            {
                var headerValues = GetHeaderData(request);
                input.Language = headerValues.Language;
                input.Token = headerValues.Token;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
            }
        }

        public string SendRequest(string url, string request)
        {
            try
            {
                string response = string.Empty;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = Strings.Services.XmlContentType;
                httpWebRequest.Method = Strings.Services.PostVerb;
                InitiateSSLTrust();

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(request);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
                return response;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }

        public string SendRequest(string url, string request, string requestVerb = Strings.Services.PostVerb, string headers = null)
        {
            try
            {
                string response = string.Empty;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = Strings.Services.JsonContentType;
                httpWebRequest.Method = requestVerb;
                if (!string.IsNullOrEmpty(headers))
                {
                    httpWebRequest.Headers["Authorization"] = "Basic QWRtaW5pc3RyYXRvcjptYW5hZ2U=";
                    //httpWebRequest.Headers["Accept"] = "*/*";
                }
                InitiateSSLTrust();

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(request);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
                return response;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }

        #region Helpers

        /// <summary>
        /// Change SSL checks so that all checks pass
        /// </summary>
        void InitiateSSLTrust()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(
                        delegate
                        { return true; }
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex, false);
            }
        }

        #endregion

        public string SerializeXmlRequest<T>(T input, bool isSpecialEncodingTagsRequired = false)
        {
            string request = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(input.GetType());
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, input);
                    request = sww.ToString(); // Your XML
                    request = request.Replace(Strings.CommandXmlTags.XmlVersionWithEncodingTag, Strings.CommandXmlTags.XmlVersionTag)
                        .Replace(Strings.CommandXmlTags.AutomaticGeneratedCommandTag, Strings.CommandXmlTags.CommandTag);
                    if (isSpecialEncodingTagsRequired)
                        request = request.Replace(Strings.CommandXmlTags.XmlVersionTag, Strings.CommandXmlTags.EncodingTag);
                }
            }
            return request;
        }

        public XElement ExtractDataFromXmlDocument(XDocument doc, string value)
        {
            return (from x in doc.Descendants(value) select x).FirstOrDefault();
        }

        public bool IsItTestEnviroment()
        {
            bool.TryParse(_utilities.GetAppSetting(Strings.AppSettingKeys.IsItTestEnviroment), out bool isItTestEnviroment);
            return isItTestEnviroment;
        }

        public bool IsMongoEnabled()
        {
            bool.TryParse(_utilities.GetAppSetting(Strings.AppSettingKeys.IsMongoEnabled), out bool isMongoEnabled);
            return isMongoEnabled;
        }

        #endregion
    }
}
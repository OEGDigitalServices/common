using System;
using Orange.Common.Entities;
using System.Web;
using System.Web.Http.Controllers;
using System.Xml.Linq;

namespace Orange.Common.Utilities
{
    public interface IServicesUtilties
    {
        HeaderData GetHeaderData(HttpRequest request);
        HeaderData GetHeaderData(HttpActionContext actionContext);
        void FillDataFromFormValues(BodyInput input, HttpRequest request);
        void FillTokenAndLanguageFromHeadersValues(BodyInput input, HttpRequest request);
        string SendRequest(string url, string request);
        string SendRequest(string url, string request, string requestVerb = Strings.Services.PostVerb, string headers = null);
        string SerializeXmlRequest<T>(T input, bool isSpecialEncodingTagsRequired = false);
        XElement ExtractDataFromXmlDocument(XDocument doc, string value);
        bool IsItTestEnviroment();
        bool IsItNextTestEnviroment();
        bool IsMongoEnabled();
        System.Net.CredentialCache GetCredentialCache(string URL);
        string GetEAISource(Channel channel);
        Object XMLToObject(string xml, Type objectType);
        string GetSoapXml<T>(T obj);
        DialType GetDialType(string rpCode);
        bool IsStagingEnviroment();        
        ServiceCallOutput SendGatewayRequest(string url, string request);
        ServiceCallOutput SendGatewayRequest(string url, string request, string requestVerb = Strings.Services.PostVerb, string headers = null);
        string GenerateXMLRequest<T>(T xmlClass, string parentNode = "");
    }
}

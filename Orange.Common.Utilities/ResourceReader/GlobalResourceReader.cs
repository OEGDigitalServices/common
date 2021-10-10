using System.Web;

namespace Orange.TriplePlay.Common.Utilities
{
    public class GlobalResourceReader : IGlobalResourceReader
    {
        public string GetValueByKey(string resourceFileName, string resourceKey)
        {
            var value = HttpContext.GetGlobalResourceObject(resourceFileName, resourceKey) as string;
            return value;
        }

        public string GetValueByKey(string resourceFileName, string resourceKey, string cultureName)
        {
            var value = HttpContext.GetGlobalResourceObject(resourceFileName, resourceKey, new System.Globalization.CultureInfo(cultureName)) as string;
            return value;
        }
    }
}

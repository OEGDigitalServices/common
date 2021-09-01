using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public interface ISecurityUtilities
    {
        string HashData(string plainText);
         bool VerifyHashedData(string hashedText, string plainText);
        
        string FormatCreditCardNumber(string cardNumber, string key, string IV);
        string FormatCreditCardNumber(string cardNumber);
        string CreateSHA256Signature(bool useRequest, SortedList<String, String> _requestFields, SortedList<String, String> _responseFields, string secureHashKey);
        string EncryptStringToBytesAES(string plainText, string secureKey, string secureIV);
        string DecryptStringUsingAES(string plainText, string secureKey, string secureIV);
        string DecryptStringAES(string encryptedStr, string key, string Iv);
        string EncryptStringAES(string str, string key, string Iv);
        byte[] HashDataByMD5(string plainText);
    }
}

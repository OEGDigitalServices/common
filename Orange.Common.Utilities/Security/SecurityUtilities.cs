using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    /// <summary>
    /// SecurityUtilities
    /// </summary>
    public class SecurityUtilities : ISecurityUtilities
    {
        private readonly ILogger _logger;
        public SecurityUtilities(ILogger logger)
        {
            _logger = logger;
        }

        public string HashData(string plainText)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;
                byte[] valueToHash = new byte[encoding.GetByteCount(plainText)];
                encoding.GetBytes(plainText, 0, plainText.Length, valueToHash, 0);
                using (HashAlgorithm hash = HashAlgorithm.Create("SHA256"))
                {
                    byte[] hashValue = hash.ComputeHash(valueToHash);
                    StringBuilder hashedText = new StringBuilder((hashValue.Length) * 2);
                    foreach (byte hexdigit in hashValue)
                    {
                        hashedText.AppendFormat(CultureInfo.InvariantCulture.NumberFormat, "{0:X2}", hexdigit);
                    }
                    return hashedText.ToString();
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }


        public bool VerifyHashedData(string hashedText, string plainText)
        {
            try
            {
                string computedHash = HashData(plainText);
                if (string.Equals(computedHash, hashedText, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
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
        /// Formats the credit card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string FormatCreditCardNumber(string cardNumber, string key, string IV)
        {
            string formattedNumber = string.Empty;
            if (!string.IsNullOrEmpty(cardNumber))
                formattedNumber = cardNumber.Substring(0, 6) + "XXXXXXX" + cardNumber.Substring(cardNumber.Length - 4);
            return EncryptStringToBytesAES(formattedNumber, key, IV);
        }

        /// <summary>
        /// Formats the credit card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        public string FormatCreditCardNumber(string cardNumber)
        {
            string formattedNumber = string.Empty;
            if (!string.IsNullOrEmpty(cardNumber))
                formattedNumber = "XXXXXXXXXXXX" + cardNumber.Substring(cardNumber.Length - 4);
            return formattedNumber;
        }

        /// <summary>
        /// Creates the SH a256 signature. 
        /// </summary>
        /// <param name="useRequest">if set to <c>true</c> [use request].</param>
        /// <returns></returns>
        public string CreateSHA256Signature(bool useRequest, SortedList<String, String> _requestFields, SortedList<String, String> _responseFields, string secureHashKey)
        {

            string _secureSecret = secureHashKey;

            // Hex Decode the Secure Secret for use in using the HMACSHA256 hasher
            // hex decoding eliminates this source of error as it is independent of the character encoding
            // hex decoding is precise in converting to a byte array and is the preferred form for representing binary values as hex strings. 
            byte[] convertedHash = new byte[_secureSecret.Length / 2];
            for (int i = 0; i < _secureSecret.Length / 2; i++)
            {
                convertedHash[i] = (byte)Int32.Parse(_secureSecret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            // Build string from collection in preperation to be hashed
            StringBuilder sb = new StringBuilder();
            SortedList<String, String> list = (useRequest ? _requestFields : _responseFields);
            foreach (KeyValuePair<string, string> kvp in list)
            {
                if (kvp.Key.StartsWith("vpc_") || kvp.Key.StartsWith("user_"))
                    sb.Append(kvp.Key + "=" + kvp.Value + "&");
            }
            // remove trailing & from string
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            // Create secureHash on string
            string hexHash = "";
            using (HMACSHA256 hasher = new HMACSHA256(convertedHash))
            {
                //byte[] hashValue = hasher.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));

                byte[] utf8bytes = Encoding.UTF8.GetBytes(sb.ToString());
                byte[] iso8859bytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("iso-8859-1"), utf8bytes);
                byte[] hashValue = hasher.ComputeHash(iso8859bytes);



                foreach (byte b in hashValue)
                {
                    hexHash += b.ToString("X2");
                }
            }
            return hexHash;
        }
        public string EncryptStringToBytesAES(string plainText, string secureKey, string secureIV)
        {
            try
            {
                byte[] Key = Convert.FromBase64String(secureKey);
                byte[] IV = Convert.FromBase64String(secureIV);
                byte[] encrypted;
                // Create an AesManaged object 
                // with the specified key and IV. 
                using (AesManaged aesAlg = new AesManaged())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {

                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }


                // Return the encrypted bytes from the memory stream. 
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }

        }
        public string DecryptStringUsingAES(string encryptedText, string secureKey, string secureIV)
        {
            string plaintext = null;

            try
            {
                byte[] Key = Convert.FromBase64String(secureKey);
                byte[] IV = Convert.FromBase64String(secureIV);
                byte[] cipherText = Convert.FromBase64String(encryptedText);
                // Create a RijndaelManaged object
                // with the specified key and IV.
                using (RijndaelManaged aesAlg = new RijndaelManaged())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    aesAlg.Mode = CipherMode.CBC;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
            }

            return plaintext;
        }
        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="encryptedStr">The encrypted STR.</param>
        /// <returns></returns>
        public string DecryptStringAES(string encryptedStr, string key, string Iv)
        {
            string decryptedString = string.Empty;

            byte[] bytes = DecodeHex16(encryptedStr);

            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.Key = Encoding.ASCII.GetBytes(key);
                myRijndael.IV = Encoding.ASCII.GetBytes(Iv);
                decryptedString = DecryptStringFromBytes_AES(bytes, myRijndael.Key, myRijndael.IV);
            }

            return decryptedString;
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public string EncryptStringAES(string str, string key, string Iv)
        {
            string encryptedString = string.Empty;

            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.Key = Encoding.ASCII.GetBytes(key);
                myRijndael.IV = Encoding.ASCII.GetBytes(Iv);
                byte[] encrypted = EncryptStringToBytes_AES(str, myRijndael.Key, myRijndael.IV);
                encryptedString = EncodeHex16(encrypted);
            }

            return encryptedString;
        }
        private Byte[] DecodeHex16(string srcString)
        {
            if (null == srcString)
            {
                throw new ArgumentNullException("srcString");
            }

            int arrayLength = srcString.Length / 2;

            Byte[] outputBytes = new Byte[arrayLength];

            for (int index = 0; index < arrayLength; index++)
            {
                outputBytes[index] = Byte.Parse(srcString.Substring(index * 2, 2), NumberStyles.AllowHexSpecifier);
            }

            return outputBytes;
        }
        private string EncodeHex16(Byte[] srcBytes)
        {
            if (null == srcBytes)
            {
                throw new ArgumentNullException("byteArray");
            }
            string outputString = "";

            foreach (Byte b in srcBytes)
            {
                outputString += b.ToString("X2");
            }

            return outputString;
        }
        private byte[] EncryptStringToBytes_AES(string plainText, byte[] Key, byte[] IV)
        {

            // Declare the streams used
            // to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt = null;
            CryptoStream csEncrypt = null;
            StreamWriter swEncrypt = null;

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                swEncrypt = new StreamWriter(csEncrypt);

                //Write all data to the stream.
                swEncrypt.Write(plainText);

            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
            }
            finally
            {
                // Clean things up.

                // Close the streams.
                if (swEncrypt != null)
                    swEncrypt.Close();
                if (csEncrypt != null)
                    csEncrypt.Close();
                if (msEncrypt != null)
                    msEncrypt.Close();

                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();

        }

        /// <summary>
        /// Decrypts the string from bytes_ AES.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="Key">The key.</param>
        /// <param name="IV">The IV.</param>
        /// <returns></returns>
        private string DecryptStringFromBytes_AES(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // TDeclare the streams used
            // to decrypt to an in memory
            // array of bytes.
            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                msDecrypt = new MemoryStream(cipherText);
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);

                // Read the decrypted bytes from the decrypting stream
                // and place them in a string.
                plaintext = srDecrypt.ReadToEnd();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
            }
            finally
            {
                // Clean things up.

                // Close the streams.
                if (srDecrypt != null)
                    srDecrypt.Close();
                if (csDecrypt != null)
                    csDecrypt.Close();
                if (msDecrypt != null)
                    msDecrypt.Close();

                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;

        }
        public byte[] HashDataByMD5(string plainText)
        {
            using (System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(plainText);
                bs = x.ComputeHash(bs);

                return bs;
            }
        }
    }
}

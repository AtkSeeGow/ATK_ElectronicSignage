using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicSignage.Web.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string Signature(string date, string appKey)
        {
            var codePage = "utf-8";
            var encoding = Encoding.GetEncoding(codePage);
            var bytes = Encoding.GetEncoding(codePage).GetBytes(date);

            var hmac = new HMACSHA1(encoding.GetBytes(appKey));

            using (var cryptoStream = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
                cryptoStream.Write(bytes, 0, bytes.Length);

            var result = Convert.ToBase64String(hmac.Hash);
            return result;
        }
    }
}

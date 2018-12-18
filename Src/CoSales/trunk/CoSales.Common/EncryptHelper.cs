using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Common
{
    /// <summary>
    /// 加解密类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// MD5进行加密
        /// 不可逆过程
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string plaintext)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string ciphertext = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(plaintext))).Replace("-", "");
            return ciphertext;
        }
    }
}

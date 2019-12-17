using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Common
{
    /// <summary>
    /// ID 生成器
    /// </summary>
    public class IdGenerator
    {
        /// <summary>
        /// 生成唯一ID
        /// </summary>
        /// <returns></returns>
        public static long GeneratorInt64()
        {
            return GetInt64HashCode(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Return unique Int64 value for input string
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        static long GetInt64HashCode(string strText)
        {
            long hashCode = 0;
            if (!string.IsNullOrEmpty(strText))
            {
                //Unicode Encode Covering all characterset
                byte[] byteContents = Encoding.Unicode.GetBytes(strText);
                System.Security.Cryptography.SHA256 hash =
                new System.Security.Cryptography.SHA256CryptoServiceProvider();
                byte[] hashText = hash.ComputeHash(byteContents);
                //32Byte hashText separate
                //hashCodeStart = 0~7  8Byte
                //hashCodeMedium = 8~23  8Byte
                //hashCodeEnd = 24~31  8Byte
                //and Fold
                long hashCodeStart = BitConverter.ToInt64(hashText, 0);
                long hashCodeMedium = BitConverter.ToInt64(hashText, 8);
                long hashCodeEnd = BitConverter.ToInt64(hashText, 24);
                hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
            }
            return hashCode;
        }
    }
}

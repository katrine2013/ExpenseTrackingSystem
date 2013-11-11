using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ExpenseTrackingSystem.Web.Helper
{
    public static class UserHelper
    {
        /// <summary>
        ///  Make SHA of the string
        /// </summary>
        /// <param name="unhashedValue">incomming string</param>
        /// <returns>string after making SHA512</returns>
        public static string hashSHA512(string unhashedValue)
        {
            SHA512 shaM = new SHA512Managed();
            if (String.IsNullOrEmpty(unhashedValue))
            {
                unhashedValue = " ";
            }
            byte[] hash =
             shaM.ComputeHash(Encoding.ASCII.GetBytes(unhashedValue));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }
    }
}
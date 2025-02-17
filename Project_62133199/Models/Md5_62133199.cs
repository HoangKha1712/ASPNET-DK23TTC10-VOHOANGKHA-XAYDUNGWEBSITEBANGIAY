using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project_62133199.Models
{
    public class Md5_62133199
    {
        public static String MaHoaMD5(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] mahoamd5;
            UTF8Encoding encode = new UTF8Encoding();
            mahoamd5 = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder data = new StringBuilder();
            for (int i = 0; i < mahoamd5.Length; i++)
            {
                data.Append(mahoamd5[i].ToString("x2"));
            }
            return data.ToString();
        }    
    }
}
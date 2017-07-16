using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SampleRx.Web.Utils
{
    public class Helpers
    {
        // This is for testing purposes. In real world, we should be using hash & salt
        public static byte[] getHash(string value)
        {
            var data = Encoding.UTF8.GetBytes(value);
            using (SHA512 shaM = new SHA512Managed())
            {
                return shaM.ComputeHash(data);
            }
        }

        
        public static void SaveSessionValue(object value, string key)
        {
            HttpContext.Current.Session[key] = value;
        }
        public static object GetSessionValue(string key)
        {
            return HttpContext.Current.Session[key];
        }
    }
}
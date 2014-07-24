using System;
using System.Text;

namespace Xander.UI.Web.Utils
{
    public static class Hex
    {
         public static string ToHexLower(this byte[] bytes)
         {
             if (bytes == null) throw new ArgumentNullException("bytes");
             var sb = new StringBuilder(bytes.Length * 2);
             foreach (byte b in bytes)
                 sb.AppendFormat("{0:x2}", b);
             return sb.ToString();
         }

        public static string ToHexUpper(this byte[] bytes)
         {
            if (bytes == null) throw new ArgumentNullException("bytes");
            var sb = new StringBuilder(bytes.Length * 2);
             foreach (byte b in bytes)
                 sb.AppendFormat("{0:X2}", b);
             return sb.ToString();
         }
    }
}
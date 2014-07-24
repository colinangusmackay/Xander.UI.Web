using System.Security.Cryptography;
using System.Text;

namespace Xander.UI.Web.Utils
{
    public static class Md5Hash
    {
        public static string ToMd5(this string clearText)
        {
            var clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] hash = MD5.Create().ComputeHash(clearBytes);
            return hash.ToHexLower();
        }
    }
}
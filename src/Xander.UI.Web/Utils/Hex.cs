#region MIT License
/*
The MIT License (MIT)

Copyright (c) 2014 Colin Angus Mackay

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.


********************************************************************************
For more information visit https://github.com/colinangusmackay/Xander.UI.Web/
********************************************************************************
*/
#endregion
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
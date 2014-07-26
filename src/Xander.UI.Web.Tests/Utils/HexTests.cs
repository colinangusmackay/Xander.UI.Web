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
// ReSharper disable InvokeAsExtensionMethod
using NUnit.Framework;
using Shouldly;
using Xander.UI.Web.Utils;

namespace Xander.UI.Web.Tests.Utils
{
    [TestFixture]
    public class HexTests
    {
        [Test]
        public void ToHexLower_EmptyArray_ReturnsEmptyString()
        {
            var result = Hex.ToHexLower(new byte[0]);

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void ToHexLower_SingleElement_ReturnsTwoCharString()
        {
            var result = Hex.ToHexLower(new byte[] {0x3E});
            result.ShouldBe("3e");
        }

        [Test]
        public void ToHexLower_5Elements_Returns10CharString()
        {
            var result = Hex.ToHexLower(new byte[] {0x12, 0x34, 0x56, 0x78, 0x9A});
            result.ShouldBe("123456789a");
        }

        [Test]
        public void ToHexUpper_EmptyArray_ReturnsEmptyString()
        {
            var result = Hex.ToHexUpper(new byte[0]);

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void ToHexUpper_SingleElement_ReturnsTwoCharString()
        {
            var result = Hex.ToHexUpper(new byte[] { 0xAF });
            result.ShouldBe("AF");
        }

        [Test]
        public void ToHexUpper_4Elements_Returns8CharString()
        {
            var result = Hex.ToHexUpper(new byte[] { 0x0B, 0xAD, 0xF0, 0x0D });
            result.ShouldBe("0BADF00D");
        }
    }
}
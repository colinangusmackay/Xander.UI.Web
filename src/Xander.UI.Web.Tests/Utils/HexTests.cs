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
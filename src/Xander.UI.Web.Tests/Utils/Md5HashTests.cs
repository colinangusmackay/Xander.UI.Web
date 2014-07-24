// ReSharper disable InvokeAsExtensionMethod
using NUnit.Framework;
using Shouldly;
using Xander.UI.Web.Utils;

namespace Xander.UI.Web.Tests.Utils
{
    [TestFixture]
    public class Md5HashTests
    {
        [Test]
        public void ToMd5_KnownConvertion_ConvertsToKnownValue()
        {
            var expected = "df39135b5108b526c44200843ed50eb5";
            var result = Md5Hash.ToMd5("hello@xander-framework.org");
            result.ShouldBe(expected);
        }
    }
}
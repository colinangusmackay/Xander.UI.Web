using System;
using NUnit.Framework;
using Shouldly;

namespace Xander.UI.Web.Tests
{
    [TestFixture]
    public class GravatarTests
    {
        [Test]
        public void For_NullAddress_ThrowsException()
        {
            Should.Throw<ArgumentNullException>(() => Gravatar.For(null))
                .ParamName.ShouldBe("emailAddress");
        }

        [Test]
        public void ForUrl_EmailAddress_BasicUrl()
        {
            string url = Gravatar.For("hello@xander-framework.org").Url;
            url.ShouldBe("https://secure.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5");
        }

        [Test]
        public void ForSecureUrl_EmailAddress_SecureUrl()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnSecureProtocol.Url;
            url.ShouldBe("https://secure.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5");
        }

        [Test]
        public void ForInsecureUrl_EmailAddress_InsecureUrl()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5");
        }

        [Test]
        public void ForInsecureSize80Url_EmailAddress_Insecure80Url()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.WithSize(80).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5");
        }

        [Test]
        public void ForInsecureSize64Url_EmailAddress_Insecure64Url()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.WithSize(64).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?s=64");
        }

        [Test]
        public void WithSize_OverMaxSize_ThrowsException()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Gravatar.For("hello@example.com").WithSize(2049))
                .Message.ShouldContain("Gravatar images can be between 1 and 2048 pixels.");
        }

        [Test]
        public void WithSize_UnderMinSize_ThrowsException()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Gravatar.For("hello@example.com").WithSize(0))
                .Message.ShouldContain("Gravatar images can be between 1 and 2048 pixels.");
        }

        [Test]
        public void ForInsecureDefaultImageUrl_EmailAddress_InsecureDefaultUrl()
        {
            string url = Gravatar.For("i-dont-exist@xander-framework.org").OnInsecureProtocol.DefaultingTo("http://example.com/my-image.jpg").Url;
            url.ShouldBe("http://www.gravatar.com/avatar/17ed90ef2174cc9f2b7e60e8c3e8d3d1?d=http%3a%2f%2fexample.com%2fmy-image.jpg");
        }

        [Test]
        public void DefaultingTo_UrlWithQueryComponent_ThrowsException()
        {
            Should.Throw<ArgumentException>( () => Gravatar.For("i-dont-exist@xander-framework.org").DefaultingTo("http://example.com/my-image.jpg?query-compoents=are-not-allowed"))
                .Message.ShouldContain("URLs for default avatars cannot contain query components");
            
        }

        [Test]
        public void DefaultingTo_UrlWithQueryComponentAsAsciiCode_ThrowsException()
        {
            Should.Throw<ArgumentException>(() => Gravatar.For("i-dont-exist@xander-framework.org").DefaultingTo("http://example.com/my-image.jpg%3fquery-compoents=are-not-allowed"))
                .Message.ShouldContain("URLs for default avatars cannot contain query components.");

        }

        [Test]
        public void DefaultingToEnum_SetToUrl_ThrowsArgumentException()
        {
            Should.Throw<ArgumentException>( () => Gravatar.For("hello@example.com").DefaultingTo(GravatarDefaultImage.Url))
                .Message.ShouldContain("Use the string version of DefaultingTo to set the URL.");
        }

        [Test]
        public void DefaultingTo_NotFound404_SetsDquerycomponentTo404()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.NotFound404).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=404");
        }

        [Test]
        public void DefaultingTo_MysteryMan_SetsDquerycomponentToMM()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.MysteryMan).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=mm");
        }

        [Test]
        public void DefaultingTo_Identicon_SetsDquerycomponentToIdenticon()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.Identicon).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=identicon");
        }

        [Test]
        public void DefaultingTo_MonsterId_SetsDquerycomponentToMonsterId()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.MonsterId).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=monsterid");
        }

        [Test]
        public void DefaultingTo_Wavatar_SetsDquerycomponentToWavatar()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.Wavatar).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=wavatar");
        }

        [Test]
        public void DefaultingTo_Retro_SetsDquerycomponentToRetro()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.Retro).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=retro");
        }

        [Test]
        public void DefaultingTo_Blank_SetsDquerycomponentToBlank()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.DefaultingTo(GravatarDefaultImage.Blank).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=blank");
        }

        [Test]
        public void AlwaysDefaultingTo_MonsterId_SetsDquerycomponentToMonsterIdAndFToY()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.Always.DefaultingTo(GravatarDefaultImage.MonsterId).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?d=monsterid&f=y");
        }

        [Test]
        public void RatedAs_G_DoesntSetRating()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.RatedAs(GravatarRating.G).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5");
        }

        [Test]
        public void RatedAs_PG_AddsParentalGuidanceRatingQueryComponent()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.RatedAs(GravatarRating.PG).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?r=pg");
        }

        [Test]
        public void RatedAs_R_AddsRestrictedRatingQueryComponent()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.RatedAs(GravatarRating.R).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?r=r");
        }

        [Test]
        public void RatedAs_X_AddseXtremeRatingQueryComponent()
        {
            string url = Gravatar.For("hello@xander-framework.org").OnInsecureProtocol.RatedAs(GravatarRating.X).Url;
            url.ShouldBe("http://www.gravatar.com/avatar/df39135b5108b526c44200843ed50eb5?r=x");
        }
    }
}
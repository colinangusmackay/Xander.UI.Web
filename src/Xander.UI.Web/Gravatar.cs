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
using System.Web;
using Xander.UI.Web.Utils;

namespace Xander.UI.Web
{
    public interface IForceContinuation
    {
        Gravatar DefaultingTo(string alternativeUrl);
        Gravatar DefaultingTo(GravatarDefaultImage defaultImage);
    }
    public class Gravatar : IForceContinuation
    {
        // A basic url is about half of this, but with options can be longer.
        private const int _estimatedBufferRequired = 128;
        private const int _defaultSize = 80;
        private const int _minSize = 1;
        private const int _maxSize = 2048;
        private const char _queryDelim = '?';
        private const char _querySubdelim = '&';
        private const string _blankImage = "blank";
        private const string _identiconImage = "identicon";
        private const string _monsterIdImage = "monsterid";
        private const string _404Image = "404";
        private const string _retroImage = "retro";
        private const string _wavatarImage = "wavatar";
        private const string _mysteryManImage = "mm";
        private const string _pgRating = "pg";
        private const string _rRating = "r";
        private const string _xRating = "x";

        private readonly string _emailAddress;
        private bool _isSecure = true;
        private int _size = 80;
        private string _defaultAvatar;
        private GravatarDefaultImage _defaultImage = GravatarDefaultImage.NotSet;
        private bool _forceDefaultImage = false;
        private GravatarRating _rating = GravatarRating.G;
        private Gravatar(string emailAddress)
        {
            _emailAddress = emailAddress.Trim().ToLowerInvariant();
        }

        public static Gravatar For(string emailAddress)
        {
            if (emailAddress == null) throw new ArgumentNullException("emailAddress");
            
            return new Gravatar(emailAddress);
        }

        public Gravatar OnSecureProtocol
        {
            get 
            { 
                _isSecure = true;
                return this;
            }
        }

        public Gravatar OnInsecureProtocol
        {
            get
            {
                _isSecure = false;
                return this;
            }
        }

        public Gravatar WithSize(int size)
        {
          if ((size < _minSize) || (size > _maxSize))
              throw new ArgumentOutOfRangeException("size", size, "Gravatar images can be between 1 and 2048 pixels.");
            _size = size;
            return this;
        }

        public Gravatar DefaultingTo(string alternativeUrl)
        {
            if (alternativeUrl.Contains("?") || alternativeUrl.ToLowerInvariant().Contains("%3f"))
                throw new ArgumentException("URLs for default avatars cannot contain query components.", "alternativeUrl");
            _defaultAvatar = alternativeUrl;
            _defaultImage = GravatarDefaultImage.Url;
            return this;
        }

        public Gravatar DefaultingTo(GravatarDefaultImage defaultImage)
        {
            if (defaultImage == GravatarDefaultImage.Url)
                throw new ArgumentException("Use the string version of DefaultingTo to set the URL.", "defaultImage");

            _defaultImage = defaultImage;
            return this;
        }

        public IForceContinuation Always
        {
            get
            {
                _forceDefaultImage = true;
                return this;
            }
        }

        public IForceContinuation WhenNotAvailable
        {
            get 
            { 
                _forceDefaultImage = false;
                return this;
            }
        }

        public Gravatar RatedAs(GravatarRating rating)
        {
            _rating = rating;
            return this;
        }

        public string Url
        {
            get
            {
                var sb = new StringBuilder(_estimatedBufferRequired);
                sb.Append(_isSecure ? "https://secure" : "http://www");
                sb.Append(".gravatar.com/avatar/");
                var hash = _emailAddress.ToMd5();
                sb.Append(hash);
                bool inQueryComponent = false;

                if (_size != _defaultSize)
                {
                    AppendSize(sb);
                    inQueryComponent = true;
                }

                if (_defaultImage != GravatarDefaultImage.NotSet)
                {
                    AppendDefaultImageType(sb, inQueryComponent);
                    inQueryComponent = true;
                }

                if (_forceDefaultImage)
                {
                    AppendForceDefaultImageSwitch(sb, inQueryComponent);
                    inQueryComponent = true;
                }

                if (_rating != GravatarRating.G)
                {
                    AppendRating(sb, inQueryComponent);
                }

                var result = sb.ToString();
                return result;
            }
        }

        private void AppendRating(StringBuilder sb, bool inQueryComponent)
        {
            sb.Append(inQueryComponent ? _querySubdelim : _queryDelim);
            sb.Append("r=");
            switch (_rating)
            {
                case GravatarRating.PG:
                    sb.Append(_pgRating);
                    break;
                case GravatarRating.R:
                    sb.Append(_rRating);
                    break;
                case GravatarRating.X:
                    sb.Append(_xRating);
                    break;
            }
        }

        private static void AppendForceDefaultImageSwitch(StringBuilder sb, bool inQueryComponent)
        {
            sb.Append(inQueryComponent ? _querySubdelim : _queryDelim);
            sb.Append("f=y");
        }

        private void AppendSize(StringBuilder sb)
        {
            sb.Append(_queryDelim);
            sb.Append("s=");
            sb.Append(_size);
        }

        private void AppendDefaultImageType(StringBuilder sb, bool inQueryComponent)
        {
            sb.Append(inQueryComponent ? _querySubdelim : _queryDelim);
            sb.Append("d=");
            switch (_defaultImage)
            {
                case GravatarDefaultImage.Url:
                    sb.Append(HttpUtility.UrlEncode(_defaultAvatar));
                    break;
                case GravatarDefaultImage.Blank:
                    sb.Append(_blankImage);
                    break;
                case GravatarDefaultImage.Identicon:
                    sb.Append(_identiconImage);
                    break;
                case GravatarDefaultImage.MonsterId:
                    sb.Append(_monsterIdImage);
                    break;
                case GravatarDefaultImage.MysteryMan:
                    sb.Append(_mysteryManImage);
                    break;
                case GravatarDefaultImage.NotFound404:
                    sb.Append(_404Image);
                    break;
                case GravatarDefaultImage.Retro:
                    sb.Append(_retroImage);
                    break;
                case GravatarDefaultImage.Wavatar:
                    sb.Append(_wavatarImage);
                    break;
            }
        }
    }
}
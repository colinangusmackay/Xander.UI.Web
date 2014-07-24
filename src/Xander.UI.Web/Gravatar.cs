using System;
using System.Text;
using System.Web;
using Xander.UI.Web.Utils;

namespace Xander.UI.Web
{
    public class Gravatar
    {
        // A basic url is about half of this, but with options can be longer.
        private const int _estimatedBufferRequired = 128;
        private const int _defaultSize = 80;
        private const int _minSize = 1;
        private const int _maxSize = 2048;
        private const char _queryDelim = '?';
        private const char _querySubdelim = '&';

        private readonly string _emailAddress;
        private bool _isSecure = true;
        private int _size = 80;
        private string _defaultAvatar;
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
                    sb.Append(_queryDelim);
                    sb.Append("s=");
                    sb.Append(_size);
                    inQueryComponent = true;
                }

                if (!string.IsNullOrWhiteSpace(_defaultAvatar))
                {
                    sb.Append(inQueryComponent ? _querySubdelim : _queryDelim);
                    sb.Append("d=");
                    sb.Append(HttpUtility.UrlEncode(_defaultAvatar));
                }

                var result = sb.ToString();
                return result;
            }
        }
    }
}
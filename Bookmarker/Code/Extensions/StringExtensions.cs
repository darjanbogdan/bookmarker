﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookmarker.Code.Extensions
{
    public static class StringExtensions
    {
        #region Methods

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion
    }
}

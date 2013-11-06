﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Achiles.Codex.Web.Misc
{
    public static class Extensions
    {
        public static string CleanId(this string id)
        {
            var separatorIndex = id.IndexOf('/');
            if (separatorIndex < 0)
                return id;
            else return id.Substring(separatorIndex);
        }

        public static string Fit(this string str, int maxLen)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= maxLen)
                return str;

            return string.Concat(str.Substring(0, maxLen), "..");
        }
    }
}
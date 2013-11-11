﻿using System.Collections.Generic;
using System.Security.Principal;
using System.Text.RegularExpressions;
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

        public static string GetQueryHighlightedinContext(this string content, string query, int contextWords = 5)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrEmpty(content))
                return null;
            var sanitizedContent = Regex.Replace(content, @"<[^>]+>|&nbsp;", "").Trim();
            var rxRipContext = new Regex(string.Format(@"(?<ctx>(\w*\W){{0,{1}}}(?<q>{0})(\w*\W){{0,{1}}})", query, contextWords),RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

            var matches = rxRipContext.Match(sanitizedContent);
            if (!matches.Success) return null;
            var context = matches.Groups["ctx"];
            return Regex.Replace(context.Value, string.Format("(?<q>{0})", query), "<strong>${q}</strong>", RegexOptions.IgnoreCase);
            //return context.Value.Replace(query, string.Format("<strong>{0}</strong>",query));
        }

        public static bool IsContributor(this IPrincipal user)
        {
            return user.IsInRole("Contributor");
        }
        public static bool IsAdmin(this IPrincipal user)
        {
            return user.IsInRole("Admin");
            
        }
    }
}
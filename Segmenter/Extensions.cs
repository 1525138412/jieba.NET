﻿using System;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace JiebaNet.Segmenter
{
    public static class Extensions
    {
        public static readonly Regex RegexDigits = new Regex(@"\d+", RegexOptions.Compiled);

        public static int ToInt32(this char ch)
        {
            return ch;
        }

        public static char ToChar(this int i)
        {
            return (char) i;
        }

        public static string Sub(this string s, int startIndex, int endIndex)
        {
            return s.Substring(startIndex, endIndex - startIndex);
        }

        public static bool IsInt32(this string s)
        {
            return RegexDigits.IsMatch(s);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System
{
    public static class StringExtentions
    {
        /// <summary>
        /// Checks wheter current string is <c>null</c> or empty.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>
        ///   <c>true</c> if string is <c>null</c> or empty.
        /// </returns>
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string FormatWith(this string str, object arg)
        {
            return string.Format(str, arg);
        }

        public static string FormatWith(this string str, object arg0, object arg1)
        {
            return string.Format(str, arg0, arg1);
        }

        public static string FormatWith(this string str, object arg0, object arg1, string arg2)
        {
            return string.Format(str, arg0, arg1, arg2);
        }

        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static string Or(this string str, string defaultStr)
        {
            return str.IsEmpty() ? defaultStr : str;
        }
    }
}

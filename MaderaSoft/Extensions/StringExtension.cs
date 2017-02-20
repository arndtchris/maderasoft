using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public static class StringExtension
    {
        /// <summary>
        /// Permet de mettre en majuscule la première lettre d'une chaîne de caractères
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string str)
        {
            if (String.IsNullOrEmpty(str))
                throw new ArgumentException("La chaîne de carctères est null");
            return str.First().ToString().ToUpper() + str.Substring(1);
        }
    }
}
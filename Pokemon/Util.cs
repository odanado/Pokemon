using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Pokemon
{
    class Util
    {
        public static string toId(string str)
        {
            str = str.Replace("é","e");
            str = str.Replace("♂","M");
            str = str.Replace("♀","F");

            str = str.ToLower();
            str = Regex.Replace(str,"[^a-z0-9]+","");

            return str;
        }
    }
}

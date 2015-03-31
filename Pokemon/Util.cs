using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Codeplex.Data;

namespace Pokemon
{
    class Util
    {
        public static string toKey(string str)
        {
            str = str.Replace("é","e");
            str = str.Replace("♂","M");
            str = str.Replace("♀","F");

            str = str.ToLower();
            str = Regex.Replace(str,"[^a-z0-9]+","");

            return str;
        }
        public static dynamic loadJson(string filename)
        {
            System.Reflection.Assembly myAssembly =
                System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.StreamReader sr =
                new System.IO.StreamReader(
                    myAssembly.GetManifestResourceStream("Pokemon." + filename),
                    System.Text.Encoding.GetEncoding("UTF-8"));

            var json = DynamicJson.Parse(sr.ReadToEnd());

            sr.Close();

            return json;
        }
    }
}

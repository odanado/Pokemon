using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Nature
    {
        public string key { set; get; }

        public Dictionary<string, double> multiplier { set; get; }

        public Nature(string name = "hardy")
        {
            key = Util.toKey(name);

            var natures = Util.loadJson("nature.json");

            if (!natures.IsDefined(key))
            {
                throw new KeyNotFoundException(key + " は性格に存在しません");
            }

            string plus = (string) natures[key]["plus"];
            string minus = (string) natures[key]["minus"];

            multiplier = new Dictionary<string, double>();

            multiplier["atk"] = 1.0;
            multiplier["def"] = 1.0;
            multiplier["spa"] = 1.0;
            multiplier["spd"] = 1.0;
            multiplier["spe"] = 1.0;

            multiplier[plus] = 1.1;
            multiplier[minus] = 0.9;


            
        }
    }
}

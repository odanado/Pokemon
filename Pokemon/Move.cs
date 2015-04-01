using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Move
    {
        public string key { get; private set; }

        public string type { get; private set; }
        public int accuracy { get; private set; }
        public int basePower { get; set; }
        public string category { get; private set; }
        public int pp { get; private set; }

        public bool isSpreadHit { get; private set; }
        public bool isCriticalHit { get; set; }

        public Move(string name = "none")
        {
            key = Util.toKey(name);
            var movedex = Util.loadJson("moves.json").BattleMovedex;

            if (name != "none" && !movedex.IsDefined(key))
            {
                throw new KeyNotFoundException(key + " は技に存在しません");
            }
            type = movedex[key].type;
            accuracy = (int)movedex[key].accuracy;
            basePower = (int)movedex[key].basePower;
            category = movedex[key].category;
            pp = (int)movedex[key].pp;

            isSpreadHit = false;
            isCriticalHit = false;

        }

    }
}

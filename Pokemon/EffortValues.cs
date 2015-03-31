using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class EffortValues
    {
        public uint hp { set; get; }
        public uint atk { set; get; }
        public uint def { set; get; }
        public uint spa { set; get; }
        public uint spd { set; get; }
        public uint spe { set; get; }

        public EffortValues()
            : this(0, 0, 0, 0, 0, 0)
        {
        }

        public EffortValues(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.spa = spa;
            this.spd = spd;
            this.spe = spe;
        }

    }
}

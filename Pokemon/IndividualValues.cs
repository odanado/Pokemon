using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class IndividualValues
    {
        public uint hp { set; get; }
        public uint atk { set; get; }
        public uint def { set; get; }
        public uint spa { set; get; }
        public uint spd { set; get; }
        public uint spe { set; get; }

        public IndividualValues()
        {
            this.hp = 31;
            this.atk = 31;
            this.def = 31;
            this.spa = 31;
            this.spd = 31;
            this.spe = 31;
        }

        public IndividualValues(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class BaseStats
    {
        public uint hp { set; get; }
        public uint atk { set; get; }
        public uint def { set; get; }
        public uint spa { set; get; }
        public uint spd { set; get; }
        public uint spe { set; get; }
        public IndividualValues ivs { set; get; }
        public EffortValues evs { set; get; }
        public uint level { set; get; }


        public BaseStats()
        {
            this.ivs = new IndividualValues();
            this.evs = new EffortValues();
            this.level = 50;
            calcStats();
        }

        public BaseStats(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.spa = spa;
            this.spd = spd;
            this.spe = spe;
        }

        private void calcStats()
        {

        }

    }
}

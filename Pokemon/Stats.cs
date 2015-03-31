using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Stats
    {
        public uint hp { set; get; }
        public uint atk { set; get; }
        public uint def { set; get; }
        public uint spa { set; get; }
        public uint spd { set; get; }
        public uint spe { set; get; }

        public Stats(BaseStats bs, EffortValues evs, IndividualValues ivs, uint level, Nature nature)
        {
            // {(種族値×2＋個体値＋努力値/4)×Lv/100}＋10＋Lv
            hp = ((bs.hp * 2 + ivs.hp + evs.hp / 4) * level / 100 + 10 + level);

            // [{(種族値×2＋個体値＋努力値/4)×Lv/100}＋5]×性格補正(1.1、1.0、0.9)
            atk = (uint)(((bs.atk * 2 + ivs.atk + evs.atk / 4) * level / 100 + 5) * nature.multiplier["atk"]);
            def = (uint)(((bs.def * 2 + ivs.def + evs.def / 4) * level / 100 + 5) * nature.multiplier["def"]);
            spa = (uint)(((bs.spa * 2 + ivs.spa + evs.spa / 4) * level / 100 + 5) * nature.multiplier["spa"]);
            spd = (uint)(((bs.spd * 2 + ivs.spd + evs.spd / 4) * level / 100 + 5) * nature.multiplier["spd"]);
            spe = (uint)(((bs.spe * 2 + ivs.spe + evs.spe / 4) * level / 100 + 5) * nature.multiplier["spe"]);
        }

        public override string ToString() 
        {
            return String.Format("Stats : {0} {1} {2} {3} {4} {5}", hp, atk, def, spa, spd, spe);
        }

    }
}

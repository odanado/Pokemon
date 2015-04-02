using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLibrary
{
    public class State
    {
        public bool isReflect { set; get; }
        public bool isLightScreen { set; get; }
        public bool isDoubleDamage { set; get; }
        public bool isSunnyDay { set; get; }
        public bool isRainDance { set; get; }
        public bool isSandStorm { set; get; }
        public bool isDauble { set; get; }

        public State()
        {
            isReflect = false;
            isLightScreen = false;
            isDoubleDamage = false;
            isSunnyDay = false;
            isRainDance = false;
            isSandStorm = false;
            isDauble = false;
        }
    }
}

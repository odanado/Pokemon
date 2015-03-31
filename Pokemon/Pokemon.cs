using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Pokemon
    {
        public Dictionary<string, int> baseStats { get; set; }
        public Dictionary<string, int> effortValues { get; set; }
        public Dictionary<string, int> individualValues { get; set; }
        public Dictionary<string, int> boosts { get; set; }

        public Abilities abilities { get; set; }
        public List<string> types { get; set; }

        public Nature nature { get; set; }
        public Item item { get; set; }

        public uint level { get; set; }

        public double heightm { get; set; }
        public double weightkg { get; set; }

        public string key { get; set; }

        public Dictionary<string, int> stats_ { get; set; }

        public Pokemon(string name)
        {
            var pokedex = new Pokedex(name);

            key = pokedex.key;

            baseStats = pokedex.baseStats;
            effortValues = new Dictionary<string, int>()
            {
                {"hp",0},{"atk",0},{"def",0},{"spa",0},{"spd",0},{"spe",0}
            };
            individualValues = new Dictionary<string, int>()
            {
                {"hp",31},{"atk",31},{"def",31},{"spa",31},{"spd",31},{"spe",31}
            };
            boosts = new Dictionary<string, int>()
            {
                {"atk",0},{"def",0},{"spa",0},{"spd",0},{"spe",0},{"accuracy",0},{"evasion",0}
            };

            abilities = pokedex.abilities;
            types = pokedex.types;

            nature = new Nature();
            item = new Item();

            level = 50;

            heightm = pokedex.heightm;
            weightkg = pokedex.weightkg;

            stats_ = new Dictionary<string, int>();

        }

        private void calcStats()
        {
            stats_["hp"] = (int)((baseStats["hp"] * 2 + individualValues["hp"] + effortValues["hp"] / 4) * level / 100 + 10 + level);

            foreach (var statName in new List<string>() { "atk", "def", "spa", "spd", "spe" })
            {
                stats_[statName] = (int)(((baseStats[statName] * 2 + individualValues[statName] + effortValues[statName] / 4) * level / 100 + 5) * nature.multiplier[statName]);
            }
        }

        public Dictionary<string,int> stats
        {
            get
            {
                calcStats();
                return stats_;
            }
        }

    }
}

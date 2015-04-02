using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Pokemon
    {
        public Dictionary<string, int> baseStats { get; set; }
        private ObservableDictionary<string, int> effortValues_;
        private ObservableDictionary<string, int> individualValues_;
        private ObservableDictionary<string, int> boosts_;

        public Abilities abilities { get; set; }
        public List<string> types { get; set; }

        public Nature nature { get; set; }
        public Item item { get; set; }

        public int level { get; set; }

        public double heightm { get; set; }
        public double weightkg { get; set; }

        public string key { get; set; }

        public Dictionary<string, int> stats { get; set; }

        public bool isBurn { get; set; }

        public Pokemon(string name)
        {
            var pokedex = new Pokedex(name);

            key = pokedex.key;

            baseStats = pokedex.baseStats;
            effortValues_ = new ObservableDictionary<string, int>(new ObservableDictionary<string,int>.Func(calcStats))
            {
                {"hp",0},{"atk",0},{"def",0},{"spa",0},{"spd",0},{"spe",0}
            };
            individualValues_ = new ObservableDictionary<string, int>(new ObservableDictionary<string, int>.Func(calcStats))
            {
                {"hp",31},{"atk",31},{"def",31},{"spa",31},{"spd",31},{"spe",31}
            };
            boosts_ = new ObservableDictionary<string, int>(new ObservableDictionary<string, int>.Func(calcStats))
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

            stats = new Dictionary<string, int>();

            isBurn = false;
            calcStats();
        }

        private void calcStats()
        {
            stats["hp"] = (int)((baseStats["hp"] * 2 + individualValues_["hp"] + effortValues_["hp"] / 4) * level / 100 + 10 + level);

            var boostTable = new List<double>(){1, 1.5, 2, 2.5, 3, 3.5, 4};
            foreach (var statName in new List<string>() { "atk", "def", "spa", "spd", "spe" })
            {
                stats[statName] = (int)(((baseStats[statName] * 2 + individualValues_[statName] + effortValues_[statName] / 4) * level / 100 + 5) * nature.multiplier[statName]);
                if(boosts_[statName] >= 0) {
                    stats[statName] = (int)(stats[statName] * boostTable[boosts_[statName]]);
                }
                else {
                    stats[statName] = (int)(stats[statName] / boostTable[boosts_[statName]]);
                }
            }
        }


        public ObservableDictionary<string, int> effortValues
        {
            set { effortValues_ = value; calcStats(); }
            get { calcStats(); return effortValues_; }
        }

        public ObservableDictionary<string, int> individualValues
        {
            set { individualValues_ = value; calcStats(); }
            get { calcStats(); return individualValues_; }
        }

        public ObservableDictionary<string, int> boosts
        {
            set { boosts_ = value; calcStats(); }
            get { calcStats(); return boosts_; }
        }

    }
}

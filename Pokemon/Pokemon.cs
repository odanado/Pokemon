using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLibrary
{
    public class Pokemon
    {
        public Dictionary<string, int> baseStats { get; set; }
        private ObservableDictionary<string, int> effortValues_;
        private ObservableDictionary<string, int> individualValues_;
        private ObservableDictionary<string, int> boosts_;

        public Abilities abilities { get; set; }
        public List<string> types { get; set; }

        private Nature nature_;
        public Item item { get; set; }

        public int level { get; set; }

        public double heightm { get; set; }
        public double weightkg { get; set; }

        public string key { get; set; }

        public Dictionary<string, int> stats { get; set; }

        public bool isBurn { get; set; }
        public bool isParalysis { get; set; }

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

            nature_ = new Nature();
            item = new Item();

            level = 50;

            heightm = pokedex.heightm;
            weightkg = pokedex.weightkg;

            stats = new Dictionary<string, int>();

            isBurn = false;
            isParalysis = false;
            calcStats();
        }

        private void calcStats()
        {
            stats["hp"] = (int)((baseStats["hp"] * 2 + individualValues_["hp"] + effortValues_["hp"] / 4) * level / 100 + 10 + level);

            
            foreach (var statName in new List<string>() { "atk", "def", "spa", "spd", "spe" })
            {
                stats[statName] = (int)(((baseStats[statName] * 2 + individualValues_[statName] + effortValues_[statName] / 4) * level / 100 + 5) * nature_.multiplier[statName]);
                
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

        public Nature nature
        {
            set { nature_ = value; calcStats(); }
            get { return nature_; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Pokemon
    {
        public BaseStats baseStats { get; set; }
        public EffortValues effortValues { get; set; }
        public IndividualValues individualValues { get; set; }
        public Abilities abilities { get; set; }
        public List<string> types { get; set; }

        public Nature nature { get; set; }
        public Item item { get; set; }

        public uint level { get; set; }

        public double heightm { get; set; }
        public double weightkg { get; set; }

        public string key { get; set; }

        public Stats stats_ { get; set; }

        public Pokemon(string name)
        {
            var pokedex = new Pokedex(name);

            key = pokedex.key;

            baseStats = pokedex.baseStats;
            effortValues = new EffortValues();
            individualValues = new IndividualValues();
            abilities = pokedex.abilities;
            types = pokedex.types;

            nature = new Nature();
            item = new Item();

            level = 50;

            heightm = pokedex.heightm;
            weightkg = pokedex.weightkg;

            stats_ = new Stats(baseStats, effortValues, individualValues, level, nature);

        }

        public Stats stats
        {
            get
            {
                stats_ = new Stats(baseStats, effortValues, individualValues, level, nature);
                return stats_;
            }
        }

    }
}

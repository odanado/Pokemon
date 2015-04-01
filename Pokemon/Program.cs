using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Program
    {
        static void Main(string[] args)
        {


            var nature = new Nature("jolly");
            var pokemon = new Pokemon("Garchomp");
            pokemon.effortValues["atk"] = 252;
            pokemon.effortValues["spe"] = 252;
            pokemon.boosts["atk"] = 2;

            foreach (var stat in pokemon.stats)
            {
                Console.WriteLine(stat);
            }

            var move = new Move("Thunderbolt");

            Console.WriteLine(move.basePower);

        }
    }
}

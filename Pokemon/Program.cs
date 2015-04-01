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
            var attacker = new Pokemon("Garchomp");
            var defender = new Pokemon("Garchomp");

            attacker.effortValues["atk"] = 252;
            
            var damageCalc = new DamageCalculator(); ;

            var move = new Move("outrage");
            var result = damageCalc.exec(attacker, defender, move);

            result.ForEach(n => Console.WriteLine(n));
        }
    }
}

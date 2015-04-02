using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLibrary
{
    public class DamageCalculator
    {

        private dynamic typeChart;
        public DamageCalculator()
        {
            typeChart = Util.loadJson("typechart.json").BattleTypeChart;
        }
        public List<int> exec(Pokemon attacker, Pokemon defender, Move move, State state)
        {
            string attackStatName = move.category == "Physical" ? "atk" : "spa";
            string defenseStatName = move.category == "Physical" ? "def" : "spd";
            int attackStat = modifyStat(attacker, attackStatName);
            int defenseStat = modifyStat(defender, defenseStatName);

            if (state.isSandStorm && move.category == "Special")
            {
                defenseStat = (int)(defenseStat * 1.5);
            }

            /*
             * ダメージ = ( ( ( ( ( ( 攻撃側のレベル * 2 ) / 5 + 2 ) * 威力 * 攻撃力 ) / 防御力 ) / 50 + 2 ) * マルチ対象※ * 天候※ * 急所 * 乱数幅(16分率) * タイプ一致※ * タイプ相性 * 火傷 * ダメージ補正※ )
             * 威力 = ( いりょく(技) * 威力補正 )
             * 攻撃力 = ( こうげき * 攻撃力補正 )
             * 防御力 = ( ぼうぎょ * 防御力補正 )
             */

            int baseDamage;
            baseDamage = (((((attacker.level * 2) / 5 + 2) * move.basePower * attackStat) / defenseStat) / 50 + 2);

            if (move.isSpreadHit)
            {
                baseDamage = modify(baseDamage, 0.75);
            }

            if (state.isSunnyDay)
            {
                if (move.type == "Fire")
                {
                    baseDamage = modify(baseDamage, 1.5);
                }
                if (move.type == "Water")
                {
                    baseDamage = modify(baseDamage, 0.5);
                }
            }

            if (state.isRainDance)
            {
                if (move.type == "Water")
                {
                    baseDamage = modify(baseDamage, 1.5);
                }
                if (move.type == "Fire")
                {
                    baseDamage = modify(baseDamage, 0.5);
                }

            }

            if (move.isCriticalHit)
            {
                baseDamage = modify(baseDamage, 1.5);
            }

            List<int> result = new List<int>(16);

            for (int i = 0; i < 16; i++)
            {
                result.Add(baseDamage * (i + 85) / 100);
            }

            // STAB
            if (attacker.types.IndexOf(move.type) != -1)
            {
                result = result.Select(n => modify(n, 1.5)).ToList();
            }

            var totalTypeMod = 1;
            foreach (var type in defender.types)
            {
                var damageTaken = (int)typeChart[type]["damageTaken"][move.type];
                if (damageTaken == 1) totalTypeMod++;
                if (damageTaken == 2) totalTypeMod--;
                if (damageTaken == 3)
                {
                    totalTypeMod = 0;
                    break;
                }
            }
            if (totalTypeMod > 0)
            {
                for (int i = 1; i < totalTypeMod; i++)
                {
                    result = result.Select(n => n * 2).ToList();
                }
            }
            if (totalTypeMod < 0)
            {
                totalTypeMod = -totalTypeMod;
                for (int i = 1; i < totalTypeMod; i++)
                {
                    result = result.Select(n => n / 2).ToList();
                }
            }

            if (move.category == "Physical" && attacker.isBurn)
            {
                result = result.Select(n => n / 2).ToList();
            }

            var mod = calcMod(attacker, defender, move, state);
            result = result.Select(n => modify(n, mod, 0x1000)).ToList();

            return result;
        }
        private int modify(int value, double numerator, double denominator = 1.0)
        {
            int modifier = (int)Math.Floor(numerator * 4096 / denominator);
            return (value * modifier + 2048 - 1) / 4096;
        }

        private int modifyStat(Pokemon pokemon, string statName)
        {
            int stat = pokemon.stats[statName];
            var boostTable = new List<double>() { 1, 1.5, 2, 2.5, 3, 3.5, 4 };
            if (pokemon.boosts[statName] >= 0)
            {
                stat = (int)(stat * boostTable[pokemon.boosts[statName]]);
            }
            else
            {
                stat = (int)(stat / boostTable[pokemon.boosts[statName]]);
            }

            return stat;
        }

        private int calcMod(Pokemon attcker, Pokemon defender, Move move, State state)
        {
            int mod = 0x1000;

            if (state.isReflect && move.category == "Physical")
            {
                int tmp = state.isDauble ? 0xA8F : 0x800;
                mod = joinMod(mod, tmp);
            }

            if (state.isLightScreen && move.category == "Special")
            {
                int tmp = state.isDauble ? 0xA8F : 0x800;
                mod = joinMod(mod, tmp);
            }

            /*
             * TODO 
             * 特性
             * 持ち物
             * 技の効果
             * 場の効果
             * を実装する
             * 
             */

            return mod;
        }

        private int joinMod(int mod1, int mod2)
        {
            return ((mod1 * mod2 + 0x800) >> 12);
        }
    }
}

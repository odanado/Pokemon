using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class DamageCalculator
    {

        private dynamic typeChart;
        public DamageCalculator()
        {
            typeChart = Util.loadJson("typechart.json").BattleTypeChart;
        }
        public List<int> exec(Pokemon attacker, Pokemon defender, Move move)
        {
            string attackStat = move.category == "Physical" ? "atk" : "spa";
            string defenseStat = move.category == "Physical" ? "def" : "spd";
            /*
             * ダメージ = ( ( ( ( ( ( 攻撃側のレベル * 2 ) / 5 + 2 ) * 威力 * 攻撃力 ) / 防御力 ) / 50 + 2 ) * マルチ対象※ * 天候※ * 急所 * 乱数幅(16分率) * タイプ一致※ * タイプ相性 * 火傷 * ダメージ補正※ )
             * 威力 = ( いりょく(技) * 威力補正 )
             * 攻撃力 = ( こうげき * 攻撃力補正 )
             * 防御力 = ( ぼうぎょ * 防御力補正 )
             */

            int baseDamage;
            baseDamage = (((((attacker.level * 2) / 5 + 2) * move.basePower * attacker.stats[attackStat]) / defender.stats[defenseStat]) / 50 + 2);

            if (move.isSpreadHit)
            {
                baseDamage = modify(baseDamage, 0.75);
            }

            /* 
             * TODO 天候補正
             * 引数に場の状態を追加して天候補正を実装するのがいいのかな 
             */

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

            if (attacker.isBurn)
            {
                result = result.Select(n => n / 2).ToList();
            }
            
            return result;
        }
        private int modify(int value, double numerator, double denominator = 1.0)
        {
            int modifier = (int)Math.Floor(numerator * 4096 / denominator);
            return (value * modifier + 2048 - 1) / 4096;
        }

    }
}

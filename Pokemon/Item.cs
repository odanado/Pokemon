using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLibrary
{
    public class Item
    {
        private string key;
        public Item(string name = "none")
        {
            key = Util.toKey(name);
            var items = Util.loadJson("items.json").BattleItems;

            if (name != "none" && !items.IsDefined(key))
            {
                throw new KeyNotFoundException(key + " は道具に存在しません");
            }
        }
    }
}

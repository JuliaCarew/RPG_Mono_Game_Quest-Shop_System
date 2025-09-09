using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.ShopSystem
{
    // list of in-game items with price amounts as int UI and names as text under them
    internal class ShopInventory : Component, IUpdatable
    {
        //public ShopInventory(Actor Shop)
        //{
            // have override to a new owner  
        //}

        // have AddItem() method to add current items to shop's iventory

        // have RemoveItem() method, removes item from inv

        // AssignSprites() get icons for gold and all items to show

        void IUpdatable.Update()
        {
            // whenever player purchases an item, update
        }
    }
}

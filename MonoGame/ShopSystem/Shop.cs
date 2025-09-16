using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.ShopSystem
{
    public class Shop : Actor
    {
        // assign all components to the shop (UI, inventory, spawner)

        // SetPosition() - take from map system, maps are hardcoded with symbols

        // make singleton, so can spawn only one instance in game 
        public Shop()
        {
            // add components

            // reference LoadTexture from Item class
            // place in grid array (3 since there's 3 items)
            Name = "Shop";

        }
        public override void Death()
        {
            return;
        }
    }
}

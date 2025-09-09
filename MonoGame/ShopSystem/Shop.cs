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
        public Shop()
        {
            // add components
        }
        public override void Death()
        {
            return;
        }
    }
}

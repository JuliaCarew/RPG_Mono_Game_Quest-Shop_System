using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.ShopSystem
{
    public class Shop : Actor
    {   
        public Shop()
        {
            Name = "Shop";
        }
        public override void Death()
        {
            return;
        }
    }
}

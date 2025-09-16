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
        public List<Item> shopInventory = new List<Item>();

        // add/reference InventorySystem class

        //public ShopInventory(Actor Shop)
        //{
        // have override to a new owner  
        //}

        void AddItem() //method to add current items to shop's iventory
        {

        }

        void RemoveItem() // method, removes item from inv
        {

        }

        void AssignSprites() //get icons for gold and all items to show
        {
            // use load 
        }

        void IUpdatable.Update()
        {
            // whenever player purchases an item, update
        }
    }
}

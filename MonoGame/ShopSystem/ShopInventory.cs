using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.ShopSystem
{
    // list of in-game items with price amounts as int UI and names as text under them
    public class ShopInventory : Component
    {
        public List<Item> shopInventory { get; private set; }  = new List<Item>();

        public void InitializeShop(Scene scene)
        {
            // spawn 3 items and add them to the shop inventory
            shopInventory.Add(scene.AddEntity(new HealingPotion()) as Item);
            shopInventory.Add(scene.AddEntity(new ScrollOfFireball()) as Item);
            shopInventory.Add(scene.AddEntity(new ScrollOfLightning()) as Item);
        }

        public void BuyItem(int index, Player player)
        {
            if (index < 0 || index >= shopInventory.Count) return;

            var item = shopInventory[index];
            // add to player inventory
            player.Inventory.Add(item);

            // remove from shop
            shopInventory.RemoveAt(index);
            item.DetachFromScene();
        }
    }
}

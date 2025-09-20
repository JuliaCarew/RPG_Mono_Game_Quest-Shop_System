using Nez;
using Nez.UI;
using System;
using System.Linq;

namespace MonoGame.ShopSystem
{
    public class Shop_UI : UICanvas
    {
        ShopInventory shopInventory;
        Player player;

        public Shop_UI(ShopInventory shopInventory, Player player)
        {
            this.shopInventory = shopInventory;
            this.player = player;

            BuildUI();
        }

        void BuildUI()
        {
            var skin = Skin.CreateDefaultSkin();
            var style = skin.Get<TextButtonStyle>();

            var table = new Table();
            table.SetFillParent(true);

            // display each item as a button
            for (int i = 0; i < shopInventory.shopInventory.Count; i++)
            {
                int index = i;
                var item = shopInventory.shopInventory[i];

                var btn = new TextButton(item.Name, skin);
                btn.OnClicked += b =>
                {
                    // when clicked, buy the item
                    shopInventory.BuyItem(index, player);
                    Stage.GetRoot().ClearChildren(); // rebuild UI after buying
                    BuildUI();
                };

                table.Add(btn).Pad(10);
                table.Row();
            }

            Stage.AddElement(table);
        }
    }
}

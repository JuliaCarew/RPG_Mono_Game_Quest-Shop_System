using Nez;
using Nez.UI;
using System;

namespace MonoGame.ShopSystem
{
    public class Shop_UI : Scene
    {
        private ShopInventory _shopInventory;
        private Player _player;

        public Shop_UI(ShopInventory shopInventory, Player player)
        {
            _shopInventory = shopInventory;
            _player = player;
        }

        public override void Initialize()
        {
            base.Initialize();
            // Create a full-screen Stage and add to Scene
            var uiEntity = CreateEntity("ui");
            var canvas = uiEntity.AddComponent(new UICanvas());

            var stage = canvas.Stage;

            var skin = Skin.CreateDefaultSkin();

            var table = new Table();
            table.SetFillParent(true);
            stage.AddElement(table);

            // build shop buttons
            BuildUI(table, stage, skin);
        }

        private void BuildUI(Table table, Stage stage, Skin skin)
        {
            table.Clear();

            table.Add(new Label("Shop", skin)).Pad(10);
            table.Row();

            int itemCount = Math.Min(3, _shopInventory.shopInventory.Count);
            for (int i = 0; i < itemCount; i++)
            {
                int index = i;
                var item = _shopInventory.shopInventory[i];

                var btn = new TextButton(item.Name, skin);
                btn.OnClicked += b =>
                {
                    _shopInventory.BuyItem(index, _player);
                    BuildUI(table, stage, skin); // rebuild UI
                };

                table.Add(btn).Pad(10);
                table.Row();
            }
        }
    }
}

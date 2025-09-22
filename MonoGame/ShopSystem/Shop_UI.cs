using Nez;
using Nez.UI;
using System;
using Microsoft.Xna.Framework;

namespace MonoGame.ShopSystem
{
    public class Shop_UI : UICanvas
    {
        private ShopInventory _shopInventory;
        private Player _player;
        private Table _table;
        private Skin _skin;

        public Shop_UI(ShopInventory shopInventory, Player player)
        {
            _shopInventory = shopInventory;
            _player = player;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            _skin = Skin.CreateDefaultSkin();
            _table = new Table();
            _table.SetFillParent(true);

            _table.SetTouchable(Touchable.Enabled);

            Stage.AddElement(_table);
            BuildUI();

            Debug.Log("Shop UI added to entity");
        }
        

        public override void Update()
        {
            base.Update();

            // debug
            if (Input.LeftMouseButtonPressed)
            {
                Debug.Log($"Mouse clicked");
            }
        }

        private void BuildUI()
        {
            _table.Clear();

            // background
            var background = new PrimitiveDrawable(Color.Black * 0.6f);
            _table.SetBackground(background);

            // shop title
            _table.Add(new Label("Shop", _skin)).Pad(10);
            _table.Row();

            // track current currency
            _table.Add(new Label($"Your Gold: {_player.Currency}", _skin)).Pad(10);
            _table.Row();

            // add buttons for items
            for (int i = 0; i < Math.Min(3, _shopInventory.shopInventory.Count); i++)
            {
                int index = i;
                var item = _shopInventory.shopInventory[i];
                var buttonText = $"{item.Name} - {item.Price}g";
                var btn = new TextButton(buttonText, _skin);

                btn.SetTouchable(Touchable.Enabled);
                Debug.Log($"Creating button: {buttonText}");

                btn.OnClicked += b =>
                {
                    Debug.Log("Shop UI: button pressed");
                    // check if player has enough currency
                    if (_player.SpendCurrency(item.Price))
                    {
                        _shopInventory.BuyItem(index, _player);
                        BuildUI(); // rebuild UI 
                    }
                    else
                    {
                        Debug.Log("Not enough gold!");
                    }
                };

                _table.Add(btn).Pad(10);
                _table.Row();
            }
        }
    }
}

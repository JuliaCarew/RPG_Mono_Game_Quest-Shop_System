using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ShopSystem;
using Nez;
using Nez.Sprites;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    public class Player : Actor
    {
        public List<Item> Inventory = new List<Item>();

        public InventorySystem inventorySystem;

        public Shop_UI ShopUIRef;

        public bool isAiming;
        public Player()
        {
            healthSystem.health = 9;
            //Position = startPosition;
            Name = "Player";
        }

        public override void OnAddedToScene()
        {

            AddComponent(new PlayerMovement(this));
            AddComponent(new InventorySystem(this));
            AddComponent(new PlayerUI(this));
            inventorySystem = GetComponent<InventorySystem>();
            LoadTexture("Player");
        }

        public void ShopInteract()
        {
            if(ShopUIRef == null) return;

            Debug.Log("Shop opened");
            var shopInventory = new ShopInventory();
            Core.Scene = new Shop_UI(shopInventory, this);
            GameEvents.OnShopReached();
        }

        public void AddItem(Item item)
        {
            if (Inventory.Count == 5)
            {
                Debug.Log("No room for item");
            }
            else
            {
                Debug.Log(Inventory.Count);
                //item.Owner = this;
                Inventory.Add(item);
                GameEvents.OnItemCollected(item);
            }
        }
        public override void Death()
        {
            if (healthSystem.health == 0)
            {
                Core.Scene = new GameOver();
            }
        }
    }

    public class PlayerMovement : Movement
    {
        Player player;
        public PlayerMovement(Player actor)
        {
            entity = actor;

            tilePosition = entity.startPosition;

            player = actor;
        }
        public override void Update()
        {
            if (!player.isAiming)
            {
                base.Update();
            }
            
        }

        public override void InteractOrMove(Microsoft.Xna.Framework.Vector2 targetPosition)
        {
            Debug.Log("Map level");
            Debug.Log(map.Level);
            Debug.Log(entity.Position);
            // Get a return from the target position
            int tile = map.checkTile(targetPosition);

            Point targetPoint = new Point((int)(targetPosition.X * 16), (int)(targetPosition.Y * 16));

            foreach (Point actorPosition in entity.ActorsPosition)
            {
                if (targetPoint == actorPosition)
                {
                    Actor targetActor = entity.turnBasedSystem.GetActor(targetPosition * 16);

                    if (targetActor != null && targetActor != entity)  // Make sure we're not attacking the player
                    {
                        Debug.Log("Actor found attack");
                        entity.basicAttack(targetActor);  // Attack the target actor
                        return;  // Exit after attacking
                    }
                }
            }

            // Process tile-based movement and other interactions
            switch (tile)
            {
                case 0: // Wall
                    break;

                case 1: // Ground
                    tilePosition = targetPosition;
                    break;

                case 2: // Exit
                    if (Map.instance.enemies.Count == 0)
                        Map.instance.ReloadMap();
                    break;

                case 3: // Player (already here)
                    Debug.Log("Player is already here.");
                    tilePosition = targetPosition;
                    break;

                case 4: // Enemy
                    Debug.Log("Enemy found!");
                    tilePosition = targetPosition;
                    break;

                case 5: // Ghost
                    Debug.Log("Ghost found!");
                    tilePosition = targetPosition;
                    break;

                case 6: // Spider
                    Debug.Log("Spider found!");
                    tilePosition = targetPosition;
                    break;

                case 7: // Health potion
                    player.AddItem((Map.instance.GetItem("Healing Potion")));
                    Debug.Log("Healing potion!");
                    tilePosition = targetPosition;
                    break;

                case 8: // Fireball scroll
                    player.AddItem((Map.instance.GetItem("Scroll of Fireball")));
                    Debug.Log("Fireball scroll!");
                    tilePosition = targetPosition;
                    break;

                case 9: // Lightning scroll
                    player.AddItem((Map.instance.GetItem("Scroll of Lightining")));
                    Debug.Log("Lightning scroll!");
                    tilePosition = targetPosition;
                    break;

                case 10: // shop
                    player.ShopInteract();
                    Debug.Log("Shop");
                    tilePosition = targetPosition;
                    break;
            }

            entity.Move(tilePosition * 16);
        }
    }
    public class PlayerUI : UICanvas
    {
        private Player entity;
        public Table table;

        public PlayerUI(Player actor)
        {
            entity = actor;

            Skin skin = Skin.CreateDefaultSkin();
            table = new Table();
            Stage.AddElement(table);
            table.ToFront();
        }

        public override void OnAddedToEntity()
        {
            table.Add(entity.Name);
            table.Row();
            table.Add("Health: " + entity.healthSystem.health);
            table.Row();
        }

        public override void Update()
        {
            table.Clear();
            table.Add("[Inventory]");
            table.Row();

            if (entity.Inventory.Count == 0)
            {
                table.Add("(Empty)");
                table.Row();
            }
            else
            {
                int keyNum = 1;
                foreach (Item item in entity.Inventory)
                {
                    table.Add($"{item.Name} = Key :{keyNum}");
                    table.Row();
                    keyNum++;
                }
            }

            table.SetPosition(50, 40);
        }
    }
}

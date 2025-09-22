using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez;
using Nez.Systems;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame
{
    public abstract class Item: Entity
    {
        public Actor.AttackType attackType;

        public Player Owner;

        private Texture2D Texture;

        public Vector2 tilePosition;

        public int Price;

        public override void OnAddedToScene()
        {
            Owner = (Player)Scene.FindEntity("Player");
        }
        public void LoadTexture(string textureName)
        {
            //Position = tilePosition;
            Texture = Owner.Scene.Content.Load<Texture2D>(textureName);
            SpriteRenderer tileRenderer = new SpriteRenderer(Texture);
            tileRenderer.SetOrigin(-tilePosition * 16);
            tileRenderer.SetLayerDepth(1);
            //Debug.Log(startPosition);
            //Debug.Log(startPosition / 16);
            AddComponent(tileRenderer);
        }

        public virtual void Use() { }
    }

    public class HealingPotion : Item
    {
        int HealAmount = 3;

        private Texture2D Health;

        public HealingPotion()
        {
            Name = "Healing Potion";
            Price = 5;
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();
            LoadTexture("Heal");
        }


        public override void Use()
        {
            Debug.Log("Heal");
            Owner.healthSystem.Heal(HealAmount);
            Owner.EndTurn();
        }
    }
    public class ScrollOfFireball : Item, IUpdatable
    {
        int DamageAmount = 2;

        private Texture2D FireBall;

        public ScrollOfFireball()
        {
            Name = "Scroll of Fireball";
            attackType = Actor.AttackType.Magic;
            Price = 10;
        }
        public override void OnAddedToScene()
        {
            base.OnAddedToScene();
            LoadTexture("Fireball");
        }

        public override void Use()
        {
            if (!Owner.isAiming)
            {
                Owner.isAiming = true;
            }
            else
            {
                Owner.isAiming = false;
            }
        }
        public override void Update()
        {
            base.Update();
            if (Owner.isAiming)
            {
                ShooterController();
            }
        }
        public virtual void ShooterController()
        {
            Vector2 direction = Vector2.Zero;

            // Get the direction based on input 
            if (Input.IsKeyPressed(Keys.W))
            {
                Debug.Log("Shoot Up");

                direction.Y -= 1; // Up
            }
            if (Input.IsKeyPressed(Keys.S))
            {
                Debug.Log("Shoot Down");

                direction.Y += 1; // Down
            }
            if (Input.IsKeyPressed(Keys.A))
            {
                Debug.Log("Shoot Left");

                direction.X -= 1; // Left
            }
            if (Input.IsKeyPressed(Keys.D))
            {
                Debug.Log("Shoot Right");

                direction.X -= 1; // Right
            }

            if (direction != Vector2.Zero)
            {
                Debug.Log("Direction wasnt equal to vector zero");
                FireballAttack(direction);
            }
        }
        private void FireballAttack(Vector2 direction)
        {
            // Start at the Player position
            Vector2 currentPosition = Owner.Position;

            // For loop for range
            for (int i = 0; i < 5; i++) 
            {
                // Each time adding the direction value of 1 and times it to pixel size
                currentPosition += direction * 16;

                // Get a return from the current position
                int tile = Map.instance.checkTile(currentPosition / 16);

                // Check for an enemy at the current position
                Enemy enemy = Map.instance.GetEnemy(currentPosition);
                if (enemy != null)
                {
                    enemy.TakeDamage(DamageAmount,attackType);
                    // Break out of loop 
                    break;
                }

                // Check if wall is hit
                if (tile == 0)
                {
                    break;
                }

            }

            // When we break do these
            Owner.isAiming = false;
            Owner.EndTurn();
            Owner.Inventory.Remove(this);
        }


    }

    public class ScrollOfLightning: Item
    {
        int DamageAmount = 1;

        private Texture2D Lightning;
        public ScrollOfLightning()
        {
            Name = "Scroll of Lightining";
            attackType = Actor.AttackType.Magic;
            Price = 8;
        }
        public override void OnAddedToScene()
        {
            base.OnAddedToScene();
            LoadTexture("Lightning");
        }

        public override void Use()
        {
            Debug.Log("Lightning");
            for (int i = Map.instance.enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = Map.instance.enemies[i];
                if (enemy != null)
                {
                    Debug.Log($"{enemy.Name} was hit by lightning");
                    enemy.TakeDamage(DamageAmount,attackType);
                }
            }
            Owner.EndTurn();
        }
    }

}

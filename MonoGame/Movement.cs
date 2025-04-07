using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez.Sprites;
using Nez;
using Nez.AI.Pathfinding;

namespace MonoGame
{
    public class Movement : Component, IUpdatable
    {
        public Actor entity;
        public Vector2 tilePosition; // This needs to be updated to map grind
        public Map map;

        // Making my grind how for looking. this should be from my tile map but this is a quick fix 
        AstarGridGraph grid = new AstarGridGraph(500, 500);

        // This is basically the start method
        public override void OnAddedToEntity()
        {
            map = (Map)Entity.Scene.FindEntity("Map");
            //tilePosition = entity.startPosition / 16;
            entity.Position = tilePosition * 16;


        }

        // Component, IUpdatable lets me use the update method
        public virtual void Update()
        {
            if (entity.isTurn && !entity.WaitAnimation)
            {
                Controller();
            }

        }

        public virtual void Controller()
        {
            grid = entity.Grid();

            Vector2 move = tilePosition;


            if (Input.IsKeyPressed(Keys.W))
            {
                Debug.Log("Moving Up");
                move.Y -= 1;
                Debug.Log(move);
            }
            if (Input.IsKeyPressed(Keys.S))
            {
                Debug.Log("Moving Down");
                move.Y += 1;
                Debug.Log(move);
            }
            if (Input.IsKeyPressed(Keys.A))
            {
                Debug.Log("Moving Left");
                move.X -= 1;
                Debug.Log(move);
            }
            if (Input.IsKeyPressed(Keys.D))
            {
                Debug.Log("Moving Right");
                move.X += 1;
                Debug.Log(move);
            }

            

            if (move != tilePosition)
            InteractOrMove(move);
        }

        public virtual void InteractOrMove(Vector2 targetPosition)
        { 
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez;
using Nez.AI.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using System;


namespace MonoGame
{
    public class Enemy : Actor
    {
        private int stunCount;

        public bool isStunted
        {
            get
            {
                if (stunCount > 0)
                {
                    spriteRenderer.Color = Color.Gray;
                    return true;
                }
                else
                {
                    spriteRenderer.Color = Color.White;
                    return false;
                }
                
            }
        }
        public Enemy()
        {
            healthSystem.health = 3;
            //Position = startPosition;
            Name = "Enemy";
            
        }

        public override void OnAddedToScene()
        {
            AddComponent(new EnemyMovement(this));
            LoadTexture("Enemy");
        }

        public override void EndTurn()
        {
            base.EndTurn();
            stunCount--;
        }
        public override void TakeDamage(int damage)
        {
            stunCount = 3;
            spriteRenderer.Color = Color.Gray;
            base.TakeDamage(damage);
        }
        
        public override void Death()
        {
            if (healthSystem.health == 0)
            {
                Debug.Log(Name);
                Debug.Log("I am dead");
                Map.instance.enemies.Remove(this);
                turnBasedSystem.RemoveActor(this);
                
            }
        }
    }

    public class EnemyMovement : Movement
    {
        public Actor player;

        public Enemy enemy;
        
        public EnemyMovement(Enemy actor)
        {
            entity = actor;
            enemy = actor;
            tilePosition = entity.startPosition; // Use startPosition (tile-based) for movement
            
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            
        }
        public List<Point> GetWalls()
        {
            // Making a list of walls 
            List<Point> walls = new List<Point>();

            foreach (var Result in map.tileMap)
            {
                Vector2 position = Result.Key;
                int tileType = Result.Value;

                if (tileType == 0 || tileType == 2 || tileType == 3)
                {

                    walls.Add(new Point((int)position.X, (int)position.Y));
                }
            }

            return walls;
        }

        public override void Update()
        {
            if (enemy.isStunted && enemy.isTurn)
            {
                entity.EndTurn();
            }
            else
            {
                base.Update();
            }
        }

        public override void Controller()
        {
            Vector2 targetPosition = tilePosition;

            player = (Player)entity.Scene.FindEntity("Player");
            // Getting tile position for my points 
            Point entityPosition = new Point((int)(entity.Position.X / 16), (int)(entity.Position.Y / 16));
            Point playerPosition = new Point((int)(player.Position.X / 16), (int)(player.Position.Y / 16));

            // Adding a list of wall that are points 
            List<Point> walls = GetWalls();


            // Making my grind how for looking. this should be from my tile map but this is a quick fix 
            AstarGridGraph grid = new AstarGridGraph(500, 500);

            // Adding my walls for each point in list of walls
            foreach (Point wall in walls)
            {
                grid.Walls.Add(wall);
            }

            // Getting the path for the searched path to and from
            List<Point> path = grid.Search(entityPosition, playerPosition);

            if (path != null && path.Count > 1)
            {
                // Getting the value of point that is a vector  
                Point nextStep = path[1];
                if (nextStep == playerPosition)
                {
                    entity.Attack(player);
                }
                else
                {
                    Debug.Log("Moving towards player");
                    targetPosition = new Vector2(nextStep.X, nextStep.Y);

                    InteractOrMove(targetPosition);
                }
            }
            else
            {
                Debug.Log("Moving towards player cause theres no path");
                targetPosition = MoveInRandomDirection();
                entity.EndTurn();
                //InteractOrMove(targetPosition);
            }
        }

        public Vector2 MoveInRandomDirection()
        {
            System.Random rng = new System.Random();
            int rngMove = rng.Next(0, 4);

            Vector2 move = tilePosition;

            switch (rngMove)
            {
                case 0:// Move Up
                    move.Y -= 1;
                    break;
                case 1:// Move Down
                    move.Y += 1;
                    break;
                case 2:// Move Left
                    move.X -= 1;
                    break;
                case 3:// Move Right
                    move.X += 1;
                    break;
            }

            return move;
        }
    }
}

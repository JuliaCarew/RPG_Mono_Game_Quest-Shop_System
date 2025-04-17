using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez;
using Nez.AI.Pathfinding;
using Nez.Textures;
using System.Linq;
using Nez.AI.UtilityAI;
using System.IO;
using System.Collections.Generic;
using System;
using Nez.Verlet;

namespace MonoGame
{
    public class Spider: Enemy
    {
        int range;
        public Spider()
        {
            //Position = startPosition;
            Name = "Spider";
        }
        public override void OnAddedToScene()
        {
            AddComponent(new RangerMovement(this));
            LoadTexture("Spider");
        }
    }

    public class RangerMovement : EnemyMovement
    {
        int range;
        public RangerMovement(Enemy actor) : base(actor)
        {
            entity = actor;
            tilePosition = entity.startPosition;
        }

        public override void Controller()
        {
            Vector2 targetPosition = tilePosition;

            player = (Player)entity.Scene.FindEntity("Player");
            // Getting tile position for my points 
            entityPosition = new Point((int)(entity.Position.X / 16), (int)(entity.Position.Y / 16));
            playerPosition = new Point((int)(player.Position.X / 16), (int)(player.Position.Y / 16));

            // Adding a list of wall that are points 
            List<Point> walls = GetWalls();


            // Making my grind how for looking. this should be from my tile map but this is a quick fix 
            grid = new AstarGridGraph(500, 500);

            // Adding my walls for each point in list of walls
            foreach (Point wall in walls)
            {
                grid.Walls.Add(wall);
            }

            if (CanShoot())
            {
                entity.Attack(player);
            }
            else
            {
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
                        //targetPosition = new Vector2(nextStep.X, nextStep.Y);

                        InteractOrMove(new Vector2(nextStep.X, nextStep.Y));
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
            
        }

        public bool CanShoot()
        {
            range = 5;

            if (entityPosition.X == playerPosition.X || entityPosition.Y == playerPosition.Y)
            {
                // For loop checking up for the player 
                for (int y = entityPosition.Y - 1; y >= entityPosition.Y - range; y--)
                {
                    Point checkUp = new Point(entityPosition.X, y);
                    if (grid.Walls.Contains(checkUp))
                    {
                        break;
                    }
                    if (checkUp == playerPosition)
                    {
                        return true;
                    }
                }

                // For loop checking down for the player  
                for (int y = entityPosition.Y + 1; y <= entityPosition.Y + range; y++)
                {
                    Point checkDown = new Point(entityPosition.X, y);
                    if (grid.Walls.Contains(checkDown))
                    {
                        break;
                    }
                    if (checkDown == playerPosition)
                    {
                        return true;
                    }
                }

                // For loop checking left for the player 
                for (int x = entityPosition.X - 1; x >= entityPosition.X - range; x--)
                {
                    Point checkLeft = new Point(x, entityPosition.Y);
                    if (grid.Walls.Contains(checkLeft))
                    {
                        break;
                    }
                    if (checkLeft == playerPosition)
                    {
                        return true;
                    }
                }

                // For loop checking right for the player 
                for (int x = entityPosition.X + 1; x <= entityPosition.X + range; x++)
                {
                    Point checkRight = new Point(x, entityPosition.Y);
                    if (grid.Walls.Contains(checkRight))
                    {
                        break;
                    }
                    if (checkRight == playerPosition)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

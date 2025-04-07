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

namespace MonoGame
{
    public class Spider: Enemy
    {
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
        public RangerMovement(Enemy actor) : base(actor)
        {
            entity = actor;
            tilePosition = entity.startPosition;
        }

        public override void Controller()
        {
            player = (Player)entity.Scene.FindEntity("Player");
            Point entityPosition = new Point((int)(entity.Position.X / 16), (int)(entity.Position.Y / 16));
            Point playerPosition = new Point((int)(player.Position.X / 16), (int)(player.Position.Y / 16));

            List<Point> walls = GetWalls();
            AstarGridGraph grid = new AstarGridGraph(500, 500);

            foreach (Point wall in walls)
            {
                grid.Walls.Add(wall);
            }

            // If the enemy X is the same as the player's X
            if (entityPosition.X == playerPosition.X || entityPosition.Y == playerPosition.Y)
            {
                Debug.Log("The player is in sight");

                for (int y = entityPosition.Y + 1; y < playerPosition.Y; y++)
                {
                    if (walls.Contains(new Point(entityPosition.X, y)))
                    {
                        Debug.Log("Wall hit while looking player is safe");
                        entity.EndTurn();
                        return;
                    }
                }
                
                for (int y = entityPosition.Y - 1; y > playerPosition.Y; y--)
                {
                    if (walls.Contains(new Point(entityPosition.X, y)))
                    {
                        Debug.Log("Wall hit while looking player is safe");
                        entity.EndTurn();
                        return;
                    }
                }

                for (int x = entityPosition.X + 1; x < playerPosition.X; x++)
                {
                    if (walls.Contains(new Point(x, entityPosition.Y)))
                    {
                        Debug.Log("Wall hit while looking player is safe");
                        entity.EndTurn();
                        return;
                    }
                }

                for (int x = entityPosition.X - 1; x > playerPosition.X; x--)
                {
                    if (walls.Contains(new Point(x, entityPosition.Y)))
                    {
                        Debug.Log("Wall hit while looking player is safe");
                        entity.EndTurn();
                        return;
                    }
                }
                entity.Attack(player);
            }
            else
            {
                Debug.Log("Player not in sight");
                entity.EndTurn();
            }

        }
    }
}

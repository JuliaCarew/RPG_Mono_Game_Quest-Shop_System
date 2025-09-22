using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez;
using Nez.AI.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using System;
using Nez.AI.GOAP;
using System.ComponentModel.Design;
using Nez.UI;

namespace MonoGame
{
    public class Boss : Enemy
    {
        public BossMovement bossMovement;
        public Boss()
        {
            healthSystem.health = 12;
            Name = "Boss";

        }
        public override void OnAddedToScene()
        {
            bossMovement = new BossMovement(this);
            AddComponent(bossMovement);
            AddComponent(new BossUI(this));
            LoadTexture("Boss");
        }

        public override void Death()
        {
            if (healthSystem.health == 0)
            {
                Core.Scene = new Victory();
                GameEvents.OnGameCompleted();
            }
        }
    }

    public class BossMovement : EnemyMovement
    {
        public enum Action
        {
            Nothing,
            Basic,
            Shoot,
            Charge
        }
        int range;

        public Action action;

        Action lastAction;
        public BossMovement(Enemy actor) : base(actor)
        {
            entity = actor;
            tilePosition = entity.startPosition;
        }


        public override void Controller()
        {
            player = (Player)entity.Scene.FindEntity("Player");
            entityPosition = new Point((int)(entity.Position.X / 16), (int)(entity.Position.Y / 16));
            playerPosition = new Point((int)(player.Position.X / 16), (int)(player.Position.Y / 16));

            List<Point> walls = GetWalls();
            grid = new AstarGridGraph(500, 500);
            foreach (Point wall in walls)
            {
                grid.Walls.Add(wall);
            }


            do
            {
                action = (Action)rng.Next(0, 4);
            } 
            while (action == lastAction);


            Debug.Log($"Boss action {action}");

            switch (action)
            {
                case Action.Nothing:
                    entity.EndTurn();
                    break;

                case Action.Basic:
                    // Basic attack and movement, only moves 1 tile 
                    base.Controller();
                    break;

                case Action.Shoot:
                    if (CanShoot())
                    {
                        entity.basicAttack(player);
                    }
                    else
                        entity.EndTurn();
                    break;

                case Action.Charge:
                    PerformChargeAttack();
                    break;
            }
            lastAction = action;
        }

        public void PerformChargeAttack()
        {
            Vector2 targetPosition = tilePosition;

            player = (Player)entity.Scene.FindEntity("Player");

            // Getting tile position for entity and player
            entityPosition = new Point((int)(entity.Position.X / 16), (int)(entity.Position.Y / 16));
            playerPosition = new Point((int)(player.Position.X / 16), (int)(player.Position.Y / 16));

            // Getting walls
            List<Point> walls = GetWalls();

            // Creating grid and adding walls
            grid = new AstarGridGraph(500, 500);
            foreach (Point wall in walls)
            {
                grid.Walls.Add(wall);
            }

            // Getting A* path
            List<Point> path = grid.Search(entityPosition, playerPosition);

            if (path != null && path.Count > 1)
            {
                // Use your ChargePath() method exactly how you wrote it
                Point nextStep = ChargePath();
                Debug.Log(nextStep);

                // If ChargePath returned a real point, do stuff
                if (nextStep == playerPosition)
                {
                    Debug.Log("Attacking player!");
                    Point regularNext = path[1];
                    InteractOrMove(new Vector2(regularNext.X, regularNext.Y));

                }
                else if (nextStep != new Point(0, 0))
                {
                    Debug.Log("Charging toward player.");
                    InteractOrMove(new Vector2(nextStep.X, nextStep.Y));
                }
                else
                {
                    Debug.Log("Path exists but no charge path. Moving normally.");
                    Point regularNext = path[1];
                    InteractOrMove(new Vector2(regularNext.X, regularNext.Y));
                }
            }
            else
            {
                Debug.Log("No path to player. Moving randomly.");
                targetPosition = MoveInRandomDirection();
                InteractOrMove(targetPosition);
                entity.EndTurn();
            }
        }

        public Point ChargePath()
        {
            range = 3;
            if (entityPosition.X == playerPosition.X || entityPosition.Y == playerPosition.Y)
            {
                // For loop checking up for the player 
                for (int y = entityPosition.Y - 1; y >= entityPosition.Y - range; y--)
                {
                    Point checkUp = new Point(entityPosition.X, y);
                    if (grid.Walls.Contains(checkUp))
                    {
                        Debug.Log("Break on the up loop");
                        break;
                    }

                    // Move 3 spaces up if possible or stop one step before the player
                    if (y == entityPosition.Y - range || y == playerPosition.Y + 1)
                    {
                        Debug.Log("Up y is one step before player y");
                        return checkUp;
                    }
                }

                // For loop checking down for the player 
                for (int y = entityPosition.Y + 1; y <= entityPosition.Y + range; y++)
                {
                    Point checkDown = new Point(entityPosition.X, y);
                    if (grid.Walls.Contains(checkDown))
                    {
                        Debug.Log("Break on the down loop");
                        break;
                    }

                    // Move 3 spaces down if possible or stop one step before the player
                    if (y == entityPosition.Y + range || y == playerPosition.Y - 1)
                    {
                        Debug.Log("Down y is one step before player y");
                        return checkDown;
                    }
                }

                // For loop checking left for the player 
                for (int x = entityPosition.X - 1; x >= entityPosition.X - range; x--)
                {
                    Point checkLeft = new Point(x, entityPosition.Y);
                    if (grid.Walls.Contains(checkLeft))
                    {
                        Debug.Log("Break on the left loop");
                        break;
                    }

                    // Move 3 spaces left if possible or stop one step before the player
                    if (x == entityPosition.X - range || x == playerPosition.X + 1)
                    {
                        Debug.Log("Left x is one step before player x");
                        return checkLeft;
                    }
                }

                // For loop checking right for the player 
                for (int x = entityPosition.X + 1; x <= entityPosition.X + range; x++)
                {
                    Point checkRight = new Point(x, entityPosition.Y);
                    if (grid.Walls.Contains(checkRight))
                    {
                        Debug.Log("Break on the right loop");
                        break;
                    }

                    // Move 3 spaces right if possible or stop one step before the player
                    if (x == entityPosition.X + range || x == playerPosition.X - 1)
                    {
                        Debug.Log("Right x is one step before player x");
                        return checkRight;
                    }
                }
            }

            // If no valid point is found, return (0, 0)
            Debug.Log("Break returning 0,0");
            return new Point(0, 0);
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
    public class BossUI : UICanvas
    {
        private Boss entity;
        public Table table;

        public BossUI(Boss actor)
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
            table.Add("Next Action");
            table.Row();
            table.Add($" {entity.bossMovement.action}");
            table.SetPosition(1200, 50);
        }
    }
}

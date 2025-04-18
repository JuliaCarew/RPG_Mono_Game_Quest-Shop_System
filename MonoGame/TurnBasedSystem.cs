using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Threading;


namespace MonoGame
{
    public class TurnBasedSystem : Component
    {
        public List<Actor> Actors;
        private int order = 0;
        public TurnBasedSystem()
        {
            Actors = new List<Actor>();
        }

        public void AddActor(Actor actor)
        {
            Actors.Add(actor);
            actor.turnBasedSystem = this;
        }

        public Actor GetActor(Vector2 vector2)
        {
            foreach (var actor in Actors)
            {
                if (actor.Position == vector2)
                {
                    if (actor == null)
                    {
                        Debug.Log("No actor was found");
                    }
                    return actor;
                }
            }
            return null;
        }
        public Actor FindPlayerActor()
        {
            foreach (var actor in Actors)
            {
                if (actor is Player)
                {
                    return actor;
                }
            }
            return null;
        }

        public void RemoveActor(Actor actor)
        {
            Actors.Remove(actor);
            actor.Destroy();
        }


        public void UpdateTurn()
        {
            Debug.Log("Update turn is being called");

            if (order < Actors.Count)
            {
                
                //Debug.Log("order < Actors.Count started");
                Actor actorTurn = Actors[order];
                
                Debug.Log("actorTurn");
                Debug.Log(actorTurn.Name);
                Debug.Log(actorTurn.Position);
                if (!actorTurn.isTurn)
                {
                    
                    //Debug.Log("is turn started");
                    actorTurn.StartTurn();

                }
                order++;
                Debug.Log(order);

            }
            else
            {
                Debug.Log("order is greater than actors count");
                order = 0;
                UpdateTurn();
            }
        }
    }
}

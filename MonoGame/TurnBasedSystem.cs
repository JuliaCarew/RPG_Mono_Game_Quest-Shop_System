using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Threading;


namespace MonoGame
{
    public class TurnBasedSystem : Component, IUpdatable
    {
        public List<Actor> Actors;
        private int order = 0;
        public TurnBasedSystem()
        {
            Actors = new List<Actor>();
        }

        public override void OnAddedToEntity()
        {

        }

        public void AddActor(Actor actor)
        {
            Actors.Add(actor);
            actor.turnBasedSystem = this;
        }

        public void Update()
        {

            //UpdateTurn();
        }
        public void UI()
        {
            foreach (var actor in Actors)
            {

            }
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
                actorTurn.isTurn = true;
                Debug.Log("actorTurn");
                Debug.Log(actorTurn.Name);

                // Start turn if it's this actor's turn
                if (actorTurn.isTurn)
                {
                    //Debug.Log("is turn started");
                    actorTurn.StartTurn();

                }
                //Thread.Sleep(250);
                //Debug.Log("Hit the bottom");

                
                order++;
                Debug.Log(order);

            }
            else
            {
                Debug.Log("order is greater than actors count");
                order = 1;
                UpdateTurn();
            }
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez;
using Nez.AI.Pathfinding;
using System.Collections.Generic;

namespace MonoGame
{
    public class Wizard: Enemy
    {
        public Wizard() 
        {
            //Position = startPosition;
            Name = "Wizard";

        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();
        }

    }

}

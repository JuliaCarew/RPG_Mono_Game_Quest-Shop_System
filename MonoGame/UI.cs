using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    public class UI: UICanvas
    {
        private Actor entity;

        public Table table;
        public UI(Actor actor)
        {
            entity = actor;
            Debug.Log("Im here");
            var skin = new Skin();
            table = Stage.AddElement(new Table());
            table.ToFront();
            table.SetFillParent(true);
            
            


        }
        public override void OnAddedToEntity()
        {
            table.Add(entity.Name);
            table.Row();
            table.Add("Health " + $"{entity.healthSystem.health}");
            table.y = entity.Position.Y - 10;
        }

        public override void Update()
        {
            if (!entity.isTurn)
            {
                table.SetIsVisible(false);
            }
            else
            {
                table.SetIsVisible(true);
            }
            table.ParentToLocalCoordinates(entity.Position);


        }

    }
}

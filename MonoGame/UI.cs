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
    public class UI : UICanvas
    {
        private Actor entity;
        public Table table;

        public UI(Actor actor)
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
            //table.SetIsVisible(entity.isTurn);

            table.Clear();
            table.Add(entity.Name);
            table.Row();
            table.Add("Health: " + entity.healthSystem.health);
            table.Row();

            Vector2 entityWorldPosition = entity.Position + new Vector2(8, -16);

            Vector2 screenWorldPosition = entity.Scene.Camera.WorldToScreenPoint(entityWorldPosition);

            table.SetPosition(screenWorldPosition.X, screenWorldPosition.Y);
        }
    }
}

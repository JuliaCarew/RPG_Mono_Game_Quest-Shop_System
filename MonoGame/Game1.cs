using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez.Sprites;
using Nez;
using Nez.Textures;
using MonoGame.QuestSsystem;
using MonoGame.ShopSystem;
using System.Numerics;
namespace MonoGame
{
    public class Game1 : Core
    {
        public Game1() : base()
        {
            //_graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene = new MainMenu();
        }
    }
}
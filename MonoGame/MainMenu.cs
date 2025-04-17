using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System.Linq;

namespace MonoGame
{
    public class MainMenu : Scene
    {
        public override void Initialize()
        {
            ClearColor = Color.Black;

            UICanvas canvas = CreateEntity("ui").AddComponent(new UICanvas());
            canvas.IsFullScreen = true;


            Skin skin = Skin.CreateDefaultSkin();

            TextButton startButton = new TextButton("Start Game", skin);
            startButton.SetPosition(600, 200);


            TextButton quitButton = new TextButton("Quit Game", skin);
            quitButton.SetPosition(600, 300);

            startButton.Center();
            quitButton.Center();


            startButton.OnClicked += StartGame;

            quitButton.OnClicked += QuitGame;

            canvas.Stage.AddElement(startButton);
            canvas.Stage.AddElement(quitButton);
        }

        private void StartGame(Button button)
        {
            Core.Scene = new Gameplay();
        }

        private void QuitGame(Button button)
        {
            Core.Exit();
        }
    }

    public class Gameplay : Scene
    {
        TurnManager turnManager;

        Map map;

        UICanvas canvas;

        public override void Initialize()
        {
            ClearColor = Color.Black;

            canvas = CreateEntity("ui").AddComponent(new UICanvas());
            turnManager = new TurnManager();
            map = new Map();

            AddEntity(turnManager);
            AddEntity(map);
        }

        public override void Update()
        {
            base.Update();
            
            Skin skin = Skin.CreateDefaultSkin();

            TextField LevelText = new TextField("Level " + map.Level, skin);
            TextField EnemyCountText = new TextField("Enemy Count : " + map.enemies.Count, skin);
            EnemyCountText.SetPosition(100, 0);
            canvas.Stage.AddElement(LevelText);
            canvas.Stage.AddElement(EnemyCountText);
        }
    }
    public class GameOver : Scene
    {
        public override void Initialize()
        {
            ClearColor = Color.Red;

            UICanvas canvas = CreateEntity("ui").AddComponent(new UICanvas());
            canvas.IsFullScreen = true;


            Skin skin = Skin.CreateDefaultSkin();

            TextButton gameOverButton = new TextButton("Game Over",skin);
            gameOverButton.SetPosition(600, 150);


            TextButton restartButton = new TextButton("Restart Game", skin);
            restartButton.SetPosition(600, 180);
            restartButton.Center();

            TextButton quitButton = new TextButton("Quit Game", skin);
            quitButton.SetPosition(600, 200);
            quitButton.Center();


            restartButton.OnClicked += RestartGame;
            quitButton.OnClicked += QuitGame;


            canvas.Stage.AddElement(gameOverButton);
            canvas.Stage.AddElement(restartButton);
            canvas.Stage.AddElement(quitButton);
        }

        private void RestartGame(Button button)
        {
            Core.Scene = new Gameplay();
        }

        private void QuitGame(Button button)
        {
            Core.Exit();
        }
    }
    public class Victory : Scene
    {
        public override void Initialize()
        {
            ClearColor = Color.Yellow;

            UICanvas canvas = CreateEntity("ui").AddComponent(new UICanvas());
            canvas.IsFullScreen = true;


            Skin skin = Skin.CreateDefaultSkin();

            TextButton gameOverButton = new TextButton("You Win!", skin);
            gameOverButton.SetPosition(600, 150);


            TextButton restartButton = new TextButton("Restart Game", skin);
            restartButton.SetPosition(600, 180);
            restartButton.Center();

            TextButton quitButton = new TextButton("Quit Game", skin);
            quitButton.SetPosition(600, 200);
            quitButton.Center();


            restartButton.OnClicked += RestartGame;
            quitButton.OnClicked += QuitGame;


            canvas.Stage.AddElement(gameOverButton);
            canvas.Stage.AddElement(restartButton);
            canvas.Stage.AddElement(quitButton);
        }

        private void RestartGame(Button button)
        {
            Core.Scene = new Gameplay();
        }

        private void QuitGame(Button button)
        {
            Core.Exit();
        }
    }
}

using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System.Linq;
using MonoGame.QuestSsystem;

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
        Quest_UI questUI;

        QuestManager questManager;

        TextField levelText;
        TextField enemyCountText;

        public override void Initialize()
        {
            ClearColor = Color.Black;

            canvas = CreateEntity("ui").AddComponent(new UICanvas());
            Skin skin = Skin.CreateDefaultSkin();

            levelText = new TextField("", skin);
            enemyCountText = new TextField("", skin);
            enemyCountText.SetPosition(100, 0);

            canvas.Stage.AddElement(levelText);
            canvas.Stage.AddElement(enemyCountText);

            // gameplay systems
            turnManager = new TurnManager();
            map = new Map();
            AddEntity(turnManager);
            AddEntity(map);

            // quest manager + UI
            var questEntity = CreateEntity("quest");
            questManager = questEntity.AddComponent(new QuestManager());
            questUI = questEntity.AddComponent(new Quest_UI());

            // UI with quests 
            questUI.UpdateQuests(questManager.currentQuests);
        }

        public override void Update()
        {
            base.Update();

            levelText.SetText("Level " + map.Level);
            enemyCountText.SetText("Enemy Count: " + map.enemies.Count);

            questUI.UpdateQuests(questManager.currentQuests);
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

            TextField gameOverText = new TextField("Game Over", skin);
            gameOverText.SetPosition(600, 100);


            TextButton restartButton = new TextButton("Restart Game", skin);
            restartButton.SetPosition(600, 200);
            //This will center the button text
            restartButton.Center();

            TextButton MenuButton = new TextButton("Main Menu", skin);
            MenuButton.SetPosition(600, 300);
            MenuButton.Center();

            TextButton quitButton = new TextButton("Quit Game", skin);
            quitButton.SetPosition(600, 400);
            quitButton.Center();

            // These give the button a funtion 
            restartButton.OnClicked += RestartGame;
            MenuButton.OnClicked += MainMenu;
            quitButton.OnClicked += QuitGame;


            canvas.Stage.AddElement(gameOverText);
            canvas.Stage.AddElement(restartButton);
            canvas.Stage.AddElement(quitButton);
        }
        private void RestartGame(Button button)
        {
            Core.Scene = new Gameplay();
        }
        private void MainMenu(Button button)
        {
            Core.Scene = new MainMenu();
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
            ClearColor = Color.LightCoral;

            UICanvas canvas = CreateEntity("ui").AddComponent(new UICanvas());
            canvas.IsFullScreen = true;


            Skin skin = Skin.CreateDefaultSkin();

            TextField victoryText = new TextField("You Win!", skin);
            victoryText.SetPosition(600, 100);

            TextButton restartButton = new TextButton("Restart Game", skin);
            restartButton.SetPosition(600, 200);
            restartButton.Center();

            TextButton MenuButton = new TextButton("Main Menu", skin);
            MenuButton.SetPosition(600, 300);
            MenuButton.Center();

            TextButton quitButton = new TextButton("Quit Game", skin);
            quitButton.SetPosition(600, 400);
            quitButton.Center();


            restartButton.OnClicked += RestartGame;
            MenuButton.OnClicked += MainMenu;
            quitButton.OnClicked += QuitGame;


            canvas.Stage.AddElement(victoryText);
            canvas.Stage.AddElement(restartButton);
            canvas.Stage.AddElement(quitButton);
        }
        private void RestartGame(Button button)
        {
            Core.Scene = new Gameplay();
        }
        private void MainMenu(Button button)
        {
            Core.Scene = new MainMenu();
        }

        private void QuitGame(Button button)
        {
            Core.Exit();
        }
    }
}

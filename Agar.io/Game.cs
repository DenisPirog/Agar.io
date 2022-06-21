using System;
using System.Collections.Generic;
using Agar.io.Objects;
using Agar.io.Factory;
using Agar.io.Interfaces;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Agar.io
{
    class Game
    {
        private string windowName;
        private static uint width;
        private static uint height;

        private List<GameObject> gameObjects;
        private List<IUpdatable> updatableObjects;
        private List<IDrawable> drawableObjects;

        private int playerCount;
        private int playerNumber;

        private int foodCount;

        private RenderWindow window;

        private Font font;
        private Text text;

        private IniFile ini;

        public Game()
        {
            ini = new IniFile("SettingsIni.txt");
            
            windowName = ini.Load("Window", "Name", "Agar.io");
            width = (uint)ini.Load("Window", "Width", 1600);
            height = (uint)ini.Load("Window", "Height", 900);
            playerCount = ini.Load("Game", "PlayerCount", 10);
            foodCount = ini.Load("Game", "FoodCount", 50);

            window = new RenderWindow(new VideoMode(width, height), windowName);
            gameObjects = new List<GameObject>();
            updatableObjects = new List<IUpdatable>();
            drawableObjects = new List<IDrawable>();
            playerNumber = 0;

            font = new Font("Data/OpenSans-Bold.ttf");
            text = new Text("", font);
            text.FillColor = Color.Black;
        }

        public void Start()
        {
            Init();
            GameLoop();
        }

        private void Init()
        {
            window.Closed += WindowClosed;
            window.SetFramerateLimit(60);

            CreateObjects();
        }

        private void CreateObjects()
        {
            for (int i = 0; i < playerCount; i++)
            {
                gameObjects.Add(PlayerFactory.CreatePlayer());
            }

            (gameObjects[playerNumber] as Player).controller = ControllerFactory.CreatePlayer();

            for (int i = 0; i < foodCount; i++)
            {
                gameObjects.Add(FoodFactory.CreateFood());
            }

            TryAddToLists();
        }

        private void TryAddToLists()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is IUpdatable)
                {
                    updatableObjects.Add(gameObject as IUpdatable);
                }

                if (gameObject is Drawable)
                {
                    drawableObjects.Add(gameObject as IDrawable);
                }
            }
        }

        private void GameLoop()
        {
            while (!IsEndGame())
            {
                UpdateObjects();
                DrawObjects();
            }
        }

        private void UpdateObjects()
        {
            foreach (IUpdatable updatable in updatableObjects.ToArray())
            {
                updatable.Update(gameObjects);
            }

            UpdateText();
        }

        private void UpdateText()
        {
            float smallRadius = gameObjects[playerNumber].Radius / 1.5f;

            Vector2f radius = new Vector2f(smallRadius, smallRadius);

            Vector2f textPos = gameObjects[playerNumber].Position + radius;

            text.Position = textPos;
            text.CharacterSize = (uint)gameObjects[playerNumber].Radius / 2;
            text.DisplayedString = ((int)gameObjects[playerNumber].Radius).ToString();
        }

        private void DrawObjects()
        {
            window.DispatchEvents();
            window.Clear(Color.White);

            foreach (IDrawable drawable in drawableObjects.ToArray())
            {
                drawable.Draw(window);
            }

            if (gameObjects[playerNumber].isAlive) window.Draw(text);

            window.Display();
        }
    
        private bool IsEndGame()
        {
            int alivePlayerCount = 0;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Player && gameObject.isAlive)
                {
                    alivePlayerCount += 1;
                }
            }

            return alivePlayerCount == 1;
        }

        public static Vector2u GetWindowSize()
        {
            return new Vector2u(width, height);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}

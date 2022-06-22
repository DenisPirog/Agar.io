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

        public List<GameObject> objectsToAdd;
        public List<GameObject> objectsToDelete;

        private int enemyCount;
        private int foodCount;

        private RenderWindow window;

        private IniFile ini;

        private static Game game;

        public Game()
        {
            ini = new IniFile("SettingsIni.txt");  
            windowName = ini.Load("Window", "Name", "Agar.io");
            width = (uint)ini.Load("Window", "Width", 1600);
            height = (uint)ini.Load("Window", "Height", 900);
            enemyCount = ini.Load("Game", "EnemyCount", 10);
            foodCount = ini.Load("Game", "FoodCount", 50);

            window = new RenderWindow(new VideoMode(width, height), windowName);

            gameObjects = new List<GameObject>();
            updatableObjects = new List<IUpdatable>();
            drawableObjects = new List<IDrawable>();
            objectsToAdd = new List<GameObject>();
            objectsToDelete = new List<GameObject>();
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
            game = this;
            CreateObjects();      
        }

        private void CreateObjects()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                PlayerFactory.CreatePlayer(PlayerType.AI);
            }

            PlayerFactory.CreatePlayer(PlayerType.Player);

            for (int i = 0; i < foodCount; i++)
            {
                FoodFactory.CreateFood();
            }
        }

        private void GameLoop()
        {
            while (!IsEndGame())
            {
                AddGameObjects();
                UpdateObjects();
                DrawObjects();
                DeleteGameObjects();             
            }
            EndGame();
        }

        private void UpdateObjects()
        {
            foreach (IUpdatable updatable in updatableObjects)
            {
                updatable.Update(gameObjects);
            }
        }

        private void DrawObjects()
        {
            window.DispatchEvents();
            window.Clear(Color.White);

            foreach (IDrawable drawable in drawableObjects)
            {
                drawable.Draw(window);
            }

            window.Display();
        }
    
        private bool IsEndGame()
        {
            int alivePlayerCount = 0;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Player)
                {
                    alivePlayerCount += 1;                 
                }
            }

            return alivePlayerCount == 1;
        }

        public static void Add(GameObject gameObject)
        {
            game.objectsToAdd.Add(gameObject);
        }

        public static void Delete(GameObject gameObject)
        {
            game.objectsToDelete.Add(gameObject);
        }

        private void AddGameObjects()
        {
            foreach(GameObject objectToAdd in objectsToAdd)
            {
                gameObjects.Add(objectToAdd);

                if (objectToAdd is IUpdatable)
                {
                    updatableObjects.Add(objectToAdd as IUpdatable);
                }
                    
                if (objectToAdd is IDrawable)
                {
                    drawableObjects.Add(objectToAdd as IDrawable);
                }          
            }

            objectsToAdd.Clear();
        }

        private void DeleteGameObjects()
        {
            foreach (GameObject objectToDelete in objectsToDelete)
            {
                gameObjects.Remove(objectToDelete);

                if (objectToDelete is IUpdatable)
                {
                    updatableObjects.Remove(objectToDelete as IUpdatable);
                }

                if (objectToDelete is IDrawable)
                {
                    drawableObjects.Remove(objectToDelete as IDrawable);
                }
            }

            objectsToDelete.Clear();
        }

        private void EndGame()
        {
            while (window.IsOpen)
            {
                window.Clear(gameObjects[0].FillColor);
                window.Display();
            }
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
using System;
using Agar.io.Objects;
using Agar.io.Utils;
using Agar.io.Factory;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Agar.io
{
    class Game
    {
        private static uint width = 1600;
        private static uint height = 900;

        private PlayerController[] controllers;

        private Player[] players;
        private int playerCount = 10;
        private int playerNumber = 0;

        private Food[] food;
        private int foodCount = 50;

        private Text text;
        private Font font;

        private RenderWindow window;

        public Game()
        {
            window = new RenderWindow(new VideoMode(width, height), "Agar.io");
            controllers = new PlayerController[playerCount];
            players = new Player[playerCount];
            food = new Food[foodCount];
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
                players[i] = PlayerFactory.CreatePlayer();
                controllers[i] = ControllerFactory.CreateController(players[i].Radius);
            }

            controllers[playerNumber].isBot = false;

            for (int i = 0; i < foodCount; i++)
            {
                food[i] = FoodFactory.CreateFood();
            }
        }

        private void GameLoop()
        {          
            while (!isEndGame())
            {
                TrySwitchPlayer();
                UpdatePlayers();
                UpdateText();
                DrawObjects();
            }
        }

        private void TrySwitchPlayer()
        {
            double minDis = double.MaxValue;
            int index = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                for (int i = 0; i < playerCount; i++)
                {
                    if (i == playerNumber || !players[i].isAlive || !players[playerNumber].isAlive)
                        continue;

                    double dis = players[playerNumber].Position.DistanceTo(players[i].Position);

                    if (dis < minDis)
                    {
                        minDis = dis;
                        index = i;
                    }
                }

                PlayerController old = controllers[playerNumber];

                controllers[playerNumber] = controllers[index];
                controllers[index] = old;

                playerNumber = index;
            }
        }

        private void UpdatePlayers()
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (players[i].isAlive)
                {
                    players[i].Update(controllers[i].GetInput(players[i]), players, food);
                }
            }
        }

        private void DrawObjects()
        {
            window.DispatchEvents();
            window.Clear(Color.White);
         
            foreach (Player player in players)
            {
                if (player.isAlive) window.Draw(player);
            }

            foreach (Food food in food)
            {
                if (food.isAlive) window.Draw(food);
            }

            if (players[playerNumber].isAlive) window.Draw(text);

            window.Display();
        }

        private void UpdateText()
        {
            Vector2f radius = new Vector2f(players[playerNumber].Radius / 1.5f, players[playerNumber].Radius / 1.5f);
            Vector2f textPos = players[playerNumber].Position + radius;

            text.Position = textPos;
            text.CharacterSize = (uint)players[playerNumber].Radius / 2;
            text.DisplayedString = ((int)players[playerNumber].Radius).ToString();
        }

        private bool isEndGame()
        {
            int alivePlayerCount = 0;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].isAlive)
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

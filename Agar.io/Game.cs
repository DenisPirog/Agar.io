using System;
using Agar.io.Objects;
using Agar.io.Factory;
using Agar.io.Controllers;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Agar.io
{
    class Game
    {
        private static uint width = 1600;
        private static uint height = 900;

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
            }

            players[playerNumber].controller = ControllerFactory.CreatePlayerController();

            for (int i = 0; i < foodCount; i++)
            {
                food[i] = FoodFactory.CreateFood();
            }
        }

        private void GameLoop()
        {          
            while (!IsEndGame())
            {
                UpdatePlayers();
                UpdateText();
                DrawObjects();
            }
        }

        private void UpdatePlayers()
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (players[i].isAlive)
                {
                    players[i].UpdatePlayer(players, food);
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

        private bool IsEndGame()
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

using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Agar.io
{
    class Game
    {
        public static uint width = 1600;
        public static uint height = 900;

        private Player[] players;
        private const int speed = 2;
        private const int playerCount = 10;
        private int playerNumber = 0;

        private Food[] food;
        private const int foodCount = 50;

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

            SpawnObjects();
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = Factory.CreatePlayer();
            }

            for (int i = 0; i < foodCount; i++)
            {
                food[i] = Factory.CreateFood();
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
                for (int i = 0; i < playerCount - 1; i++)
                {
                    if (i == playerNumber || !players[i].isAlive)
                        continue;

                    double dis = VectorExtensions.DistanceTo(players[playerNumber].Position, players[i].Position);

                    if (dis < minDis)
                    {
                        minDis = dis;
                        index = i;
                    }
                }
              
                playerNumber = index;
            }
        }

        private void UpdatePlayers()
        {
            Vector2f positionToMove;

            foreach (Player player in players)
            {
                if (player.isAlive)
                {
                    if (player == players[playerNumber]) positionToMove = player.Position + Input();
                    else positionToMove = player.CalculatePath();

                    player.TryMove(positionToMove);
                    player.TryEat(players, food);
                }
            }
        }

        private Vector2f Input()
        {
            Vector2f input = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) input.Y = -speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) input.Y = speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) input.X = -speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) input.X = speed;

            return input;
        }

        private void DrawObjects()
        {
            window.DispatchEvents();
            window.Clear(Color.White);
          
            foreach (Food food in food)
            {
                if (food.isAlive) window.Draw(food);
            }

            foreach (Player player in players)
            {
                if (player.isAlive) window.Draw(player);
            }

            if (players[playerNumber].isAlive) window.Draw(text);

            window.Display();
        }

        private void UpdateText()
        {
            text.Position = players[playerNumber].Position + new Vector2f(players[playerNumber].Radius / 1.5f, players[playerNumber].Radius / 1.5f);
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

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}

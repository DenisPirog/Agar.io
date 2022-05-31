using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Agar.io
{
    class Game
    {
        private const int windowWidth = 1600;
        private const int windowHeight = 900;

        private Player[] players;
        private Player player;
        private int speed = 4;
        private int playerCount = 10;

        private Drawable[] objectsToDraw;
        private Font font;

        private RenderWindow window;

        public Game()
        {
            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Agar.io");
            players = new Player[playerCount];
            font = new Font("Data/OpenSans-Bold.ttf");
            objectsToDraw = new Drawable[1];
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

            CreateField();
        }

        private void CreateField()
        {
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = PlayerFactory.CreatePlayer();
            }

            player = players[0];      

            //SpawnFood
        }

        private void GameLoop()
        {          
            while (!isEndGame())
            {
                UpdatePlayers();

                //UpdatePlayers
                //Move
                //TryEat
                //Collide
                DrawField();
            }
        }

        private void UpdatePlayers()
        {
            player.TryMove(player.Position + Input(), players, windowWidth, windowHeight);

            for (int i = 1; i <= players.Length - 1; i++)
            {
                players[i].TryMove(players[i].Position + new Vector2f(1, 1), players, windowWidth, windowHeight);
            }

            Text text = new Text("YOU", font);
            text.Position = new Vector2f(player.Position.X + 7.5f, player.Position.Y + 17);
            text.CharacterSize = 20;
            objectsToDraw[0] = text;
        }

        private Vector2f Input()
        {
            Vector2f input = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                input.Y = -speed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                input.Y = speed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                input.X = -speed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                input.X = speed;
            }

            return input;
        }

        private void DrawField()
        {
            window.DispatchEvents();
            window.Clear(Color.White);

            foreach (Player player in players)
            {
                window.Draw(player);
            }

            foreach (Drawable obj in objectsToDraw)
            {
                window.Draw(obj);
            }
            window.Display();
        }

        private bool isEndGame()
        {
            return false;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}

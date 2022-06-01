using SFML.System;
using SFML.Graphics;
using System;

namespace Agar.io
{
    public class Player : CircleShape
    {
        private Random rnd;

        public bool isAlive = true;
        public Vector2f targetPosition;

        public Player(int radius, Color color, Vector2f position, Vector2f target)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = Radius / 30;
            targetPosition = target;
            rnd = new Random();
        }

        public void TryMove(Vector2f newPosition, int width, int height)
        {
            bool inXBorder = newPosition.X <= width - Radius * 2 && newPosition.X >= 0;
            bool inYBorder = newPosition.Y <= height - Radius * 2 && newPosition.Y >= 0;
            bool inBorder = inXBorder && inYBorder;

            if (inBorder) Position = newPosition;
        }       

        public void TryEat(Player[] players, Food[] food)
        {
            for (int i = 0; i <= players.Length - 1; i++)
            {
                if (players[i] != this && players[i].isAlive && players[i].Radius < Radius && VectorExtensions.isColliding(this, players[i]))
                {
                    Radius += players[i].Eat();
                    RepelPlayer(players[i].Radius);
                }
            }

            for (int i = 0; i <= food.Length - 1; i++)
            {
                if (food[i].isAlive && VectorExtensions.isColliding(this, food[i]))
                {
                    Radius += food[i].Eat();
                    RepelPlayer(food[i].Radius / 2);
                }
            }
        }

        private void RepelPlayer(float force)
        {
            if (Position.X >= 800 && Position.Y >= 450) Position += new Vector2f(-force, -force);
            if (Position.X >= 800 && Position.Y <= 450) Position += new Vector2f(-force, 0);
            if (Position.X <= 800 && Position.Y >= 450) Position += new Vector2f(0, -force);
        }

        public Vector2f CalculatePath()
        {
            if (targetPosition == Position)
            {
                targetPosition = new Vector2f(rnd.Next(0, 1600 - (int)Radius * 2), rnd.Next(0, 900 - (int)Radius * 2));
            }

            if (targetPosition.X > Position.X) Position += new Vector2f(1, 0);
            if (targetPosition.X < Position.X) Position += new Vector2f(-1, 0);
            if (targetPosition.Y > Position.Y) Position += new Vector2f(0, 1);
            if (targetPosition.Y < Position.Y) Position += new Vector2f(0, -1);

            return Position;
        }

        public float Eat()
        {
            isAlive = false;
            return Radius / 2;
        }
    }
}

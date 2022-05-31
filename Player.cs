using System;
using SFML.System;
using SFML.Graphics;

namespace Agar.io
{
    public class Player : CircleShape
    {
        public int score = 0;

        public Player(int radius, Color color, Vector2f position)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = Radius / 30;
        }

        public void TryMove(Vector2f newPosition, Player[] players, int width, int height)
        {
            bool inXBorder = newPosition.X <= width - Radius * 2 && newPosition.X >= 0;
            bool inYBorder = newPosition.Y <= height - Radius * 2 && newPosition.Y >= 0;
            bool inBorder = inXBorder && inYBorder;
            bool isColliding = false;

            Vector2f oldPosition = Position;

            Position = newPosition;

            for (int i = 0; i <= players.Length - 1; i++)
            {
                if(players[i] != this)
                {
                    if (VectorExtensions.isColliding(this, players[i]))
                    {
                        isColliding = true;
                        double dis = VectorExtensions.DistanceTo(this.Position, players[i].Position);
                        newPosition += new Vector2f(-(float)dis, -(float)dis);
                    }
                }
            }

            Position = oldPosition;

            if (inBorder && !isColliding)
            {
                Position = newPosition;
            }
        }
    }
}

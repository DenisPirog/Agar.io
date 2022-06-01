using SFML.System;
using SFML.Graphics;

namespace Agar.io
{
    public class Food : CircleShape
    {
        public bool isAlive = true;

        public Food(int radius, Color color, Vector2f position)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = Radius / 30;
        }

        public float Eat()
        {
            isAlive = false;
            return Radius / 4;
        }
    }
}

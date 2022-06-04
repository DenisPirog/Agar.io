using SFML.Graphics;
using SFML.System;
using System;

namespace Agar.io
{
    public class Generator
    {
        private static Random rnd = new Random();

        public static Color GenerateColor()
        {
            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);

            return new Color(r, g, b);
        }

        public static Vector2f GeneratePosition(float radius)
        {
            float randomX = RandomFloat.Next(radius, Game.width - radius * 2);
            float randomY = RandomFloat.Next(radius, Game.height - radius * 2);

            return new Vector2f(randomX, randomY);
        }
    }
}

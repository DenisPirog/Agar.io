using System;
using SFML.System;
using SFML.Graphics;

namespace Agar.io
{
    public class FoodFactory
    {
        private static readonly Random rnd = new Random();

        public static Food CreateFood()
        {
            int radius = 10;

            Vector2f position = new Vector2f(rnd.Next(0, 1600 - radius * 2), rnd.Next(0, 900 - radius * 2));

            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);

            Color color = new Color(r, g, b);

            return new Food(radius, color, position);
        }
    }
}

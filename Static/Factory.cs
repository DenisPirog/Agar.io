using System;
using SFML.System;
using SFML.Graphics;

namespace Agar.io
{
    public class Factory
    {
        private static readonly Random rnd = new Random();

        public static Player CreatePlayer()
        {
            int radius = 0;

            switch (rnd.Next(1, 4))
            {
                case 1:
                    radius = 20;
                    break;
                case 2:
                    radius = 30;
                    break;
                case 3:
                    radius = 40;
                    break;
            }
      
            Vector2f position = VectorExtensions.GeneratePosition(radius);
            Vector2f target = VectorExtensions.GeneratePosition(radius);

            Color color = GenerateColor();

            int outlineThickness = radius / 30;

            return new Player(radius, color, position, target, outlineThickness);
        }

        public static Food CreateFood()
        {
            int radius = 10;

            Vector2f position = VectorExtensions.GeneratePosition(radius);

            Color color = GenerateColor();

            int outlineThickness = radius / 30;

            return new Food(radius, color, position, outlineThickness);
        }

        public static Color GenerateColor()
        {
            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);

            return new Color(r, g, b);
        }
    }
}

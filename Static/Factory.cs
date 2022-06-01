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
      
            Vector2f position = new Vector2f(rnd.Next(radius, 1600 - radius * 2), rnd.Next(radius, 900 - radius * 2));
            Vector2f target = new Vector2f(rnd.Next(radius, 1600 - radius * 2), rnd.Next(radius, 900 - radius * 2));

            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);

            Color color = new Color(r, g, b);

            return new Player(radius, color, position, target);
        }

        public static Food CreateFood()
        {
            int radius = 10;

            Vector2f position = new Vector2f(rnd.Next(radius, 1600 - radius * 2), rnd.Next(radius, 900 - radius * 2));

            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);

            Color color = new Color(r, g, b);

            return new Food(radius, color, position);
        }
    }
}

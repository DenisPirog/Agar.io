using System;
using SFML.System;
using SFML.Graphics;

namespace Agar.io
{
    public class Factory
    {
        public static Player CreatePlayer()
        {
            int radius = 0;

            switch (RandomFloat.Next(1, 4))
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
      
            Vector2f position = Generator.GeneratePosition(radius);
            Vector2f target = Generator.GeneratePosition(radius);

            Color color = Generator.GenerateColor();

            int outlineThickness = radius / 30;

            return new Player(radius, color, position, target, outlineThickness);
        }

        public static Food CreateFood()
        {
            int radius = 10;

            Vector2f position = Generator.GeneratePosition(radius);

            Color color = Generator.GenerateColor();

            int outlineThickness = radius / 30;

            return new Food(radius, color, position, outlineThickness);
        }
    }
}

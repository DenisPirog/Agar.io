using Agar.io.Objects;
using Agar.io.Utils;
using SFML.Graphics;
using SFML.System;

namespace Agar.io.Factory
{
    class FoodFactory
    {
        public static Food CreateFood()
        {
            int radius = 10;

            Vector2f position = Generator.GetPositionOnGameField(radius);

            Color color = Generator.GetRandomColor();

            int outlineThickness = radius / 30;

            return new Food(radius, color, position, outlineThickness);
        }
    }
}

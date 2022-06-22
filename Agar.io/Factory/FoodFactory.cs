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

            Vector2f origin = new Vector2f(radius, radius);

            Food food = new Food(radius, color, position, origin);

            Game.Add(food);
            return food;
        }
    }
}

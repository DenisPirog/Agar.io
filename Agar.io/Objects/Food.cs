using SFML.System;
using SFML.Graphics;

namespace Agar.io.Objects
{
    public class Food : GameObject
    {
        public Food(int radius, Color color, Vector2f position, int outlineThickness)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = outlineThickness;
        }
    }
}

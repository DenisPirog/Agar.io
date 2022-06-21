using SFML.System;
using SFML.Graphics;
using Agar.io.Interfaces;

namespace Agar.io.Objects
{
    public class Food : GameObject, IDrawable
    {
        public Food(int radius, Color color, Vector2f position, int outlineThickness)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            OutlineColor = Color.Black;
            OutlineThickness = outlineThickness;
            eatPoints = Radius / 2;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }
    }
}

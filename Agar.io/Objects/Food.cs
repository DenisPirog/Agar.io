using SFML.System;
using SFML.Graphics;
using Agar.io.Interfaces;

namespace Agar.io.Objects
{
    public class Food : GameObject, IDrawable
    {
        public Food(int radius, Color color, Vector2f position, Vector2f origin)
        {
            Radius = radius;
            FillColor = color;
            Position = position;
            Origin = origin;
            OutlineColor = Color.Black;
            OutlineThickness = Radius / 30;
            eatPoints = Radius / 2;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }
    }
}

using SFML.Graphics;
using SFML.System;

namespace Agar.io.Utils
{
    public class Generator
    {
        public static Color GetRandomColor()
        {
            byte r = AgarioRandom.NextByte(1, 255);
            byte g = AgarioRandom.NextByte(1, 255);
            byte b = AgarioRandom.NextByte(1, 255);

            return new Color(r, g, b);
        }

        public static Vector2f GetPositionOnGameField(float radius)
        {
            Vector2u windowSize = Game.GetWindowSize();

            float randomX = AgarioRandom.NextFloat(radius * 2, windowSize.X - radius * 2);
            float randomY = AgarioRandom.NextFloat(radius * 2, windowSize.Y - radius * 2);

            return new Vector2f(randomX, randomY);
        }
    }
}

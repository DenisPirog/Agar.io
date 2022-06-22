using System;
using Agar.io.Objects;
using SFML.System;

namespace Agar.io.Utils
{
    public static class MathExtensions
    {
        public static bool IsColliding(this GameObject object1, GameObject object2)
        {
            double distance = object1.Position.DistanceTo(object2.Position);

            if (distance < object1.Radius + object2.Radius)
            {
                return true;
            }

            return false;
        }

        public static double DistanceTo(this Vector2f from, Vector2f to)
        {
            double Dx = from.X - to.X;
            double Dy = from.Y - to.Y;

            double distance = Math.Sqrt(Dx * Dx + Dy * Dy);

            return distance;
        }
      
        public static bool IsEqual(this Vector2f first, Vector2f second)
        {
            bool isXEqual = Math.Abs(first.X - second.X) <= 0.01;
            bool isYEqual = Math.Abs(first.Y - second.Y) <= 0.01;
            return isXEqual && isYEqual;
        }

        public static bool IsInBorder(this Vector2f newPosition, float radius)
        {
            Vector2u windowSize = Game.GetWindowSize();

            bool inXBorder = newPosition.X >= radius && newPosition.X <= windowSize.X - radius;
            bool inYBorder = newPosition.Y >= radius && newPosition.Y <= windowSize.Y - radius;

            return inXBorder && inYBorder;
        }
    }
}

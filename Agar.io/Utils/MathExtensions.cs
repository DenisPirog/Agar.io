using System;
using SFML.System;
using SFML.Graphics;

namespace Agar.io.Utils
{
    public static class MathExtensions
    {
        public static bool isColliding(this CircleShape object1, CircleShape object2)
        {
            Vector2f obj1Pos = new Vector2f(object1.Position.X + object1.Radius, object1.Position.Y + object1.Radius);
            Vector2f obj2Pos = new Vector2f(object2.Position.X + object2.Radius, object2.Position.Y + object2.Radius);

            double distance = obj1Pos.DistanceTo(obj2Pos);

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
      
        public static bool isEqual(this Vector2f first, Vector2f second)
        {
            bool isXEqual = Math.Abs(first.X - second.X) <= 0.01;
            bool isYEqual = Math.Abs(first.Y - second.Y) <= 0.01;
            return isXEqual && isYEqual;
        }
    }
}

using System;
using SFML.System;
using SFML.Graphics;

namespace Agar.io
{
    public class VectorExtensions
    {
        public static double DistanceTo(Vector2f from, Vector2f to)
        {
            double Dx = from.X - to.X;
            double Dy = from.Y - to.Y;

            double distance = Math.Sqrt(Dx * Dx + Dy * Dy);

            return distance;
        }

        public static bool isColliding(CircleShape object1, CircleShape object2)
        {
            Vector2f obj1Fix = new Vector2f(object1.Position.X + object1.Radius, object1.Position.Y + object1.Radius);
            Vector2f obj2Fix = new Vector2f(object2.Position.X + object2.Radius, object2.Position.Y + object2.Radius);

            double distance = DistanceTo(obj1Fix, obj2Fix);

            if (distance < object1.Radius + object2.Radius)
            {
                return true;
            }   

            return false;
        }

        public static bool isPositionsAreEqual(Vector2f first, Vector2f second)
        {
            bool isXEqual = Math.Abs(first.X - second.X) <= 0.001;
            bool isYEqual = Math.Abs(first.Y - second.Y) <= 0.001;
            return isXEqual && isYEqual;
        }
    }
}

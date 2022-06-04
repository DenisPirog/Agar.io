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

        public static Vector2f GeneratePosition(float radius)
        {
            float randomX = RandomFloat.Next(radius, Game.width - radius * 2);
            float randomY = RandomFloat.Next(radius, Game.height - radius * 2);

            return new Vector2f(randomX, randomY);
        }
    }
}

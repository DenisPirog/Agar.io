using SFML.Graphics;

namespace Agar.io.Objects
{
    public class GameObject : CircleShape
    {
        public bool isAlive = true;

        public float Eat()
        {
            isAlive = false;
            return Radius / 2;
        }
    }
}

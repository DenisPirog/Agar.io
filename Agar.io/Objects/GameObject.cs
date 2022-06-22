using SFML.Graphics;

namespace Agar.io.Objects
{
    public class GameObject : CircleShape
    {
        public bool isAlive = true;
        public float eatPoints;

        public float Eat()
        {
            isAlive = false;
            Game.Delete(this);
            return eatPoints;
        }
    }
}

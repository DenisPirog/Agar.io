using SFML.Graphics;

namespace Agar.io.Objects
{
    public class GameObject : CircleShape
    {
        public float eatPoints;

        public float Eat()
        {
            Game.Delete(this);
            return eatPoints;
        }

        public void TakeDamage(int damage)
        {
            Radius -= damage;

            if (Radius <= 0)
            {
                Game.Delete(this);
            }
        }
    }
}

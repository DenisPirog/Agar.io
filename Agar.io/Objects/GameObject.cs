using SFML.Graphics;

namespace Agar.io.Objects
{
    public class GameObject : CircleShape
    {
        public float eatPoints;

        public float Eat()
        {
            Die();
            return eatPoints;
        }

        public void TakeDamage(int damage)
        {
            Radius -= damage;

            if (Radius <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Game.Delete(this);
        }
    }
}

using Agar.io.Objects;
using SFML.System;

namespace Agar.io.Factory
{
    class BulletFactory
    {
        public static Bullet CreateBullet(Player owner, Vector2i target)
        {
            int radius = 15;

            Vector2f origin = new Vector2f(radius, radius);

            Bullet bullet = new Bullet(radius, origin, owner, target);

            Game.Add(bullet);
            return bullet;
        }
    }
}

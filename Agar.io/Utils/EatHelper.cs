using Agar.io.Objects;

namespace Agar.io.Utils
{
    public static class EatHelper
    {
        public static bool CanEat(this GameObject gameObject, GameObject objectToEat)
            => gameObject.Radius - objectToEat.Radius > 0.001 &&
                !(objectToEat is Bullet);

        public static bool IsCollideWithAlive(this GameObject gameObject, GameObject objectToCollide)
        {
            if (gameObject.IsColliding(objectToCollide))
            {
                if (gameObject != objectToCollide)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

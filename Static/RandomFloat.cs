using System;

namespace Agar.io
{
    public class RandomFloat
    {
        private Random random;

        private static RandomFloat instance;

        static RandomFloat()
        {
            if(instance == null)
                instance = new RandomFloat();
            instance.random = new Random();
        }

        public static float Next(float from, float to)
            => instance.random.Next((int)from, (int)to);
    }
}

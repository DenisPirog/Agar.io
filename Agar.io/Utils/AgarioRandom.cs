using System;

namespace Agar.io.Utils
{
    public class AgarioRandom
    {
        private Random random;

        private static AgarioRandom instance;

        static AgarioRandom()
        {
            if (instance == null)
                instance = new AgarioRandom();
            instance.random = new Random();
        }

        public static byte NextByte(byte from, byte to)
            => (byte)instance.random.Next(from, to);

        public static int NextInt(int from, int to)
            => instance.random.Next(from, to);

        public static int NextFloat(float from, float to)
           => instance.random.Next((int)from, (int)to);
    }
}
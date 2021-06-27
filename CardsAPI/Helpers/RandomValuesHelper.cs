using System;

namespace CardsAPI.Helpers
{
    public static class RandomValuesHelper
    {
        private static readonly Random Random = new Random();

        public static long LongRandom(long min, long max)
        {
            long result = Random.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)Random.Next((Int32)min, (Int32)max);
            return result;
        }
    }
}

using System;

namespace ApplicationLayer.Services.Random
{
    public class RandomFacade : IRandomFacade
    {
        private readonly System.Random _random;

        public RandomFacade()
        {
            _random = new System.Random(Guid.NewGuid().GetHashCode());
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
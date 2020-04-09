using RandomApp.Api.Interfaces;
using System;

namespace RandomApp.Api.Services
{
    public class RandomService : IRandomService
    {
        public int GetRandomNumber(int minNumber, int maxNumber)
        {
            var random = new Random();

            return random.Next(minNumber, maxNumber);
        }
    }
}

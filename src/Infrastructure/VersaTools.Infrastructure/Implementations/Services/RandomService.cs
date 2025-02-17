using VersaTools.Application.Abstractions.Services;

namespace VersaTools.Infrastructure.Implementations.Services
{
    public class RandomService : IRandomService
    {
        private readonly Random _random;

        public RandomService()
        {
            _random = new Random();
        }

        public IEnumerable<int> GetRandomNumbers(int min, int max, int count, bool unique)
        {
            if (min > max)
                throw new ArgumentException("The minimum value cannot be greater than the maximum value.");

            if (unique)
            {
                int range = max - min + 1;
                if (count > range)
                    throw new ArgumentException("The number of unique values exceeds the available range.");

                List<int> numbers = Enumerable.Range(min, range).ToList();
                for (int i = numbers.Count - 1; i > 0; i--)
                {
                    int j = _random.Next(0, i + 1);
                    (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
                }
                return numbers.Take(count);
            }
            else
            {
                List<int> result = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    result.Add(_random.Next(min, max + 1));
                }
                return result;
            }
        }

        public string GetRandomChoice(params string[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException("No values were provided for selection.");

            return values[_random.Next(values.Length)];
        }
    }
}

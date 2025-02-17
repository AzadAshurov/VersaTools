namespace VersaTools.Application.Abstractions.Services
{
    public interface IRandomService
    {
        IEnumerable<int> GetRandomNumbers(int min, int max, int count, bool unique);
        string GetRandomChoice(params string[] values);
    }
}
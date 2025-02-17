namespace VersaTools.Application.Abstractions.Services
{
    public interface IKeyboardLayoutService
    {
        string ConvertToCyrillic(string input);
        string ConvertToLatin(string input);
    }

}

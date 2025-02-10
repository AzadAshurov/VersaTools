using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.Abstractions.Services
{
    public interface IAiService
    {
        Task<string> GetRegex(string request);
        Task<string> GenerateSqlQuery(string description);
        Task<string> GeneratePassword(int length, bool includeSymbols, bool includeNumbers);
        Task<string> GenerateRandomData(string dataType);
        Task<string> FormatJson(string json);
        Task<string> FormatYaml(string yaml);
        Task<string> GenerateCodeSnippet(string description, string language);
        Task<string> GenerateShellCommand(string description, bool isWindows);
        Task<string> GenerateMarkdown(string text);
        Task<string> DecodeAbbreviation(string abbreviation);
        Task<string> GenerateSlogan(string keywords);
        Task<string> GenerateProjectName(string theme);
        Task<string> GenerateGreeting(string occasion, string recipient);
        Task<string> CreateToDoList(string tasks);
        Task<string> GenerateProductDescription(string productName, string features);
        Task<string> ParaphraseText(string text);
        Task<string> GenerateSocialMediaPostIdea(string topic);
        Task<string> GenerateJoke(string topic);
        Task<string> CreateResume(string skills, string experience);
        Task<string> GetInspirationalQuote(string topic);
    }

}

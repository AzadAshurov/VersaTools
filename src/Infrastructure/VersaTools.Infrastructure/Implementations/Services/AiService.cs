using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.Abstractions.Services;

namespace VersaTools.Infrastructure.Implementations.Services
{
    public class AiService : IAiService
    {
        public async Task<string> GetRegex(string request)
        {
            string improvedRequest = $"In square brackets below user wrote his request for regex. Please generate regex code that matches his request [{request}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateSqlQuery(string description)
        {
            string improvedRequest = $"In square brackets below user described the desired SQL query. Please generate a valid SQL query that meets the following description [{description}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GeneratePassword(int length, bool includeSymbols, bool includeNumbers)
        {
            string improvedRequest = $"Please generate a secure password with the following parameters: Length: {length}, Include Symbols: {includeSymbols}, Include Numbers: {includeNumbers}.";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateRandomData(string dataType)
        {
            string improvedRequest = $"In square brackets below user specified the type of random data to generate. Please generate random data for type [{dataType}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> FormatJson(string json)
        {
            string improvedRequest = $"In square brackets below is a JSON string that might be minified or malformed. Please format it into a human-readable JSON: [{json}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> FormatYaml(string yaml)
        {
            string improvedRequest = $"In square brackets below is a YAML/TOML/XML string that might be minified or malformed. Please format it into a human-readable version: [{yaml}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateCodeSnippet(string description, string language)
        {
            string improvedRequest = $"In square brackets below user described a code snippet in {language}. Please generate a {language} code snippet that meets the following description [{description}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateShellCommand(string description, bool isWindows)
        {
            string shellType = isWindows ? "Powershell" : "Bash";
            string improvedRequest = $"In square brackets below user described a command for {shellType}. Please generate a {shellType} command that accomplishes the following task [{description}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateMarkdown(string text)
        {
            string improvedRequest = $"In square brackets below is plain text. Please convert it into a properly formatted Markdown text: [{text}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> DecodeAbbreviation(string abbreviation)
        {
            string improvedRequest = $"In square brackets below is a technical abbreviation. Please provide its full form and a brief explanation: [{abbreviation}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateSlogan(string keywords)
        {
            string improvedRequest = $"In square brackets below are keywords. Please generate a catchy slogan that incorporates these keywords: [{keywords}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateProjectName(string theme)
        {
            string improvedRequest = $"In square brackets below user provided a theme. Please generate a creative project or brand name related to this theme: [{theme}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateGreeting(string occasion, string recipient)
        {
            string improvedRequest = $"In square brackets below user described an occasion and a recipient. Please generate a personalized greeting message for the occasion '{occasion}' addressed to '{recipient}'.";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> CreateToDoList(string tasks)
        {
            string improvedRequest = $"In square brackets below is a comma-separated list of tasks. Please generate a structured To-Do list based on the following tasks: [{tasks}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateProductDescription(string productName, string features)
        {
            string improvedRequest = $"In square brackets below is the product name and its features. Please generate a compelling product description for '{productName}' with the following features: [{features}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> ParaphraseText(string text)
        {
            string improvedRequest = $"In square brackets below is a piece of text. Please paraphrase the text, preserving its meaning but using different wording: [{text}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateSocialMediaPostIdea(string topic)
        {
            string improvedRequest = $"In square brackets below is a topic. Please generate an idea for a social media post on this topic: [{topic}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GenerateJoke(string topic)
        {
            string improvedRequest = $"In square brackets below is a topic. Please generate a light-hearted joke related to this topic: [{topic}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> CreateResume(string skills, string experience)
        {
            string improvedRequest = $"In square brackets below are the skills and experiences of a person. Please generate a professional resume using the following details: Skills: [{skills}], Experience: [{experience}]";
            return await Ai.SendToAi(improvedRequest);
        }

        public async Task<string> GetInspirationalQuote(string topic)
        {
            string improvedRequest = $"In square brackets below is a topic. Please generate an inspirational quote related to this topic: [{topic}]";
            return await Ai.SendToAi(improvedRequest);
        }
    }

}

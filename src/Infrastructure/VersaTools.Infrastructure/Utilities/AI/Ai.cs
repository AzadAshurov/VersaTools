
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ai
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiUrl = "http://127.0.0.1:8000/generate";

    public static async Task<string> SendToAi(string prompt)
    {
        var requestData = new { prompt };
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();

        var responseJson = JObject.Parse(responseString);
        return responseJson["choices"]?[0]?["message"]?["content"]?.ToString() ?? "";
    }
}



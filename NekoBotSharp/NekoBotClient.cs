namespace NekoBotSharp;

public class NekoBotClient
{
    private static readonly string ApiLink = "https://nekobot.xyz/api";

    /// <summary>
    /// Get an image url from the NekoBot API
    /// </summary>
    /// <param name="imageType">Specify the type of the image</param>
    /// <returns></returns>
    public static async Task<string> GetImageUrl(NekoImageTypes imageType)
    {
        string type = imageType == NekoImageTypes.FourK ? "4k" : imageType.ToString().ToLower();
        JsonResponse response = await JsonResponse.GetResponseAsync($"{ApiLink}/image?type={type}");
        if (response.Success) return response.Message;
        throw new HttpRequestException("Request failed: Success status returned false");
    }
}

class JsonResponse
{
    public bool Success { get; private set; }
    public string Message { get; private set; } = "";
    public string Color { get; private set; } = "";
    public string Version { get; private set; } = "";

    public static async Task<JsonResponse> GetResponseAsync(string link)
    {
        string json = await GetJson(link);
        // Clean curly braces
        json = json.Trim('{', '}');

        // Split key-value pairs
        var pairs = json.Split(',');
        JsonResponse response = new JsonResponse();

        foreach (var pair in pairs)
        {
            var kv = pair.Split(new[] { ':' }, 2);
            if (kv.Length != 2) continue;

            string key = kv[0].Trim().Trim('"');
            string value = kv[1].Trim().Trim('"');

            switch (key)
            {
                case "success":
                    response.Success = value == "true";
                    break;
                case "message":
                    response.Message = value;
                    break;
                case "color":
                    response.Color = value;
                    break;
                case "version":
                    response.Version = value;
                    break;
            }
        }
        return response;
    }

    private static async Task<string> GetJson(string url)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
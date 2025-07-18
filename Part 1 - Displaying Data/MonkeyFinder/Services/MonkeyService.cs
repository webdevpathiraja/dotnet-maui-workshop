using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient;
    public MonkeyService()
    {
        httpClient = new HttpClient();
    }

    List<Monkey> monkeyList = new();

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList.Count > 0)
            return monkeyList;

        var url = "https://montemagno.com/monkeys.json";

        try
        {
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();

                // Add null check
                if (monkeyList == null)
                {
                    monkeyList = new List<Monkey>();
                }
            }
            else
            {
                // Log the error status
                System.Diagnostics.Debug.WriteLine($"HTTP Error: {response.StatusCode}");
                monkeyList = new List<Monkey>();
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            System.Diagnostics.Debug.WriteLine($"Exception in GetMonkeys: {ex.Message}");
            monkeyList = new List<Monkey>();
        }

        return monkeyList;
    }
}
// WeatherService.cs
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "9d750f5202d39bf4b6470b9f745bd1ad";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse> GetWeatherForecast(string city)
    {
        var response = await _httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<WeatherResponse>(responseString);
    }
}

public class WeatherResponse
{
    // Define properties according to OpenWeatherMap API response
    public List<List> List { get; set; }
}

public class List
{
    public Main Main { get; set; }
    public Wind Wind { get; set; }
    public List<Weather> Weather { get; set; }
}

public class Main
{
    public double Temp { get; set; }
    public double Temp_Min { get; set; }
    public double Temp_Max { get; set; }
    public int Humidity { get; set; }
}

public class Wind
{
    public double Speed { get; set; }
}

public class Weather
{
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

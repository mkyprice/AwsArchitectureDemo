using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AwsArchitectureDemo.Controllers;

[ApiController]
[Route("/[controller]")]
public class WeatherForecastController : ControllerBase
{
	private readonly ILogger<WeatherForecastController> _logger;

	public WeatherForecastController(ILogger<WeatherForecastController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	[Route("/")]
	public string Get()
	{
		return "Service is available";
	}

	[HttpGet]
	[Route("/get/{page}/{count}")]
	public IEnumerable<WeatherForecast> Get(int page, int count)
	{
		return ReadForecasts().Skip(page).Take(count);
	}

	[HttpGet]
	[Route("/get/{day}/{month}/{year}")]
	public WeatherForecast? Get(int day, int month, int year)
	{
		return ReadForecasts()
			.FirstOrDefault(d => d.Date.Day == day && d.Date.Month == month && d.Date.Year == year);
	}

	private WeatherForecast[] ReadForecasts()
	{
		WeatherForecast[] weather = null;
		for (int i = 0; i < 5000; i++)
		{
			string json = System.IO.File.ReadAllText("Weather.json");
			weather = JsonConvert.DeserializeObject<WeatherForecast[]>(json);
		}
		return weather;
	}
}

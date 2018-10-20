using System.Threading.Tasks;

namespace SpaceApp
{
	public interface IWeatherService
	{
		Weather GetWeather (string zipCode, string country, string measure);
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        public async Task<Forecast> GetForecast(double latitude, double longitude)
        {
            var language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var apiKey = "b38b0666ec3594a828ac3b8ac707684f";
            var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&lang={language}&appid={apiKey}";


        }
    }
}

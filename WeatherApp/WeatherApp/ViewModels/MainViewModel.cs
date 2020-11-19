using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Essentials;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IWeatherService weatherService;

        private string city;
        public string City {
            get => city;
            set => Set(ref city, value);
        }
        private ObservableCollection<ForecastGroup> days;
        public ObservableCollection<ForecastGroup> Days {
            get => days;
            set => Set(ref days, value);
        }


        public MainViewModel(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        private async Task LoadData()
        {
            var location = await Geolocation.GetLocationAsync();
            var forecast = await weatherService.GetForecast(location.Latitude, location.Longitude);


            var itemGroups = new List<ForecastGroup>();

            foreach (var item in forecast.Items) {
                if (!itemGroups.Any()) {
                    itemGroups.Add(this.DefaultForecastGroup(item));
                    continue;
                }

                var group = itemGroups.SingleOrDefault(x => x.Date == item.DateTime.Date);

                if (group == null) {
                    itemGroups.Add(this.DefaultForecastGroup(item));
                }
            }

            this.Days = new ObservableCollection<ForecastGroup>(itemGroups);
            this.City = forecast.City;
        }
        private ForecastGroup DefaultForecastGroup(ForecastItem item)
        {
            return new ForecastGroup(new List<ForecastItem>() { item }) { Date = item.DateTime.Date };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public event EventHandler<string> LocationNotify;

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

        private bool isRefreshing;
        public bool IsRefreshing {
            get => isRefreshing;
            set => Set(ref isRefreshing, value);
        }


        public MainViewModel(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task LoadData()
        {
            IsRefreshing = true;
            Location location = new Location();

            try {
                location = await Geolocation.GetLocationAsync();
            }
            catch (Exception ex) {
                this.LocationNotify?.Invoke(this, ex.Message);
            }

            Forecast forecast = await weatherService.GetForecast(location.Latitude, location.Longitude);

            var itemGroups = new List<ForecastGroup>();

            foreach (var item in forecast.Items) {
                if (!itemGroups.Any()) {
                    itemGroups.Add(this.DefaultForecastGroup(item));
                    continue;
                }

                var group = itemGroups.SingleOrDefault(x => x.Date == item.DateTime.Date);

                if (group == null) {
                    itemGroups.Add(this.DefaultForecastGroup(item));
                    continue;
                }

                group.Items.Add(item);
            }

            this.Days = new ObservableCollection<ForecastGroup>(itemGroups);
            this.City = forecast.City;

            IsRefreshing = false;
        }

        private ForecastGroup DefaultForecastGroup(ForecastItem item)
        {
            return new ForecastGroup(new List<ForecastItem>() { item }) { Date = item.DateTime.Date };
        }


        public ICommand Refresh => new Command(async () => {
            await LoadData();
        });
    }
}

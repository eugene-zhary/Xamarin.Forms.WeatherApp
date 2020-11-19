using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = Resolver.Resolve<MainViewModel>();
            (BindingContext as MainViewModel).LocationNotify += MainView_LocationNotify;
        }

        private void MainView_LocationNotify(object sender, string e)
        {
            DisplayAlert("Info", "turn your location to see the weather in your area", "ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainViewModel viewModel)
                MainThread.BeginInvokeOnMainThread(async () => 
                {
                    await viewModel.LoadData();
                });

        }
    }
}
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public class Bootstrapper
    {

        public static void Init()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<OpenWeatherMapWeatherService>().As<IWeatherService>();
            containerBuilder.RegisterType<MainViewModel>();

            IContainer container = containerBuilder.Build();

            Resolver.Initialize(container);
        }
    }
}

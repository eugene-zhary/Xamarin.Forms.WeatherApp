using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class ForecastGroup: List<ForecastItem>
    {
        public DateTime Date { get; set; }
        public string DateAsString => Date.ToShortDateString();
        public List<ForecastItem> Items => this;

        public ForecastGroup()
        {

        }

        public ForecastGroup(IEnumerable<ForecastItem> items)
        {
            this.AddRange(items);
        }
    }
}

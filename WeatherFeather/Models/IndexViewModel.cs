using Microsoft.AspNetCore.Mvc.Rendering;
using WeatherFeather.Domain.Entities;

namespace WeatherFeather.Models
{
    public class IndexViewModel
    {
        public IEnumerable<WeatherItem> PagedWeatherItems { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList YearSelectList { get; set; }
        public SelectList MonthSelectList { get; set; }
    }
}

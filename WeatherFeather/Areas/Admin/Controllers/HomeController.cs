using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherFeather.Domain;
using WeatherFeather.Models;

namespace WeatherFeather.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController
        : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            // Get data
            var weatherItems = this._dataManager.WeatherItems.GetWeatherItems();

            // Set pagination options
            var pageSize = 30;
            var count = await weatherItems.CountAsync();
            var pagedWeatherItems = await weatherItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            // Model for pagination
            PageViewModel pageViewModel = new PageViewModel(count, pageNumber, pageSize);

            // Get year info for drop down list
            var years = new SelectList(weatherItems.Select(s => s.Year).Distinct());

            // Get month info for drop down list
            var months = new SelectList(weatherItems.Select(s => s.Month).Distinct());

            // Final model to render
            IndexViewModel indexViewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                PagedWeatherItems = pagedWeatherItems,
                YearSelectList = years,
                MonthSelectList = months
            };

            return View(indexViewModel);
        }
    }
}

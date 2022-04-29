using Microsoft.AspNetCore.Mvc;
using WeatherFeather.Domain;
using WeatherFeather.Domain.Entities;
using WeatherFeather.Service;

namespace WeatherFeather.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WeatherItemsController
        : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WeatherItemsController
        (
            DataManager dataManager,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this._dataManager = dataManager;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Edit(Guid id)
        {
            var entity = id == default ? new WeatherItem() : this._dataManager.WeatherItems.GetWeatherItemById(id);

            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(WeatherItem model)
        {
            if (ModelState.IsValid)
            {
                this._dataManager.WeatherItems.SaveWeatherItem(model);

                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            this._dataManager.WeatherItems.DeleteWeatherItem(id);

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WeatherFeather.Domain;
using WeatherFeather.Domain.Entities;
using WeatherFeather.Service;

namespace WeatherFeather.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TextFieldsController
        : Controller
    {
        private readonly DataManager _dataManager;

        public TextFieldsController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        public IActionResult Edit(string codeWord)
        {
            var entity = this._dataManager.TextFields.GetTextFieldByCodeWord(codeWord);

            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(TextField model)
        {
            if (ModelState.IsValid)
            {
                this._dataManager.TextFields.SaveTextField(model);

                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            return View(model);
        }
    }
}

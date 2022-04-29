using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherFeather.Domain;
using WeatherFeather.Models;
using WeatherFeather.Service;

namespace WeatherFeather.Controllers
{
    public class ViewWeatherDataController
        : Controller
    {
        private readonly DataManager _dataManager;
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ViewWeatherDataController
        (
            DataManager dataManager,
            AppDbContext appDbContext,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this._dataManager = dataManager;
            this._appDbContext = appDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index
        (
            string sortCol = "Year",
            string sortParam = "Desc",
            string yearSearchString = "",
            string monthSearchString = "",
            int pageNumber = 1
        )
        {
            // Get raw data
            var rawWeatherItems = this._dataManager.WeatherItems.GetWeatherItems();

            // Searched data
            var searchedWeatherItems = rawWeatherItems;

            // Searching by year
            ViewBag.YearSearchString = yearSearchString;
            if (!string.IsNullOrEmpty(yearSearchString))
            {
                searchedWeatherItems = rawWeatherItems.Where(w => w.Year.Contains(yearSearchString));
            }

            // Searching by month (after searching by year)
            ViewBag.MonthSearchString = monthSearchString;
            if (!string.IsNullOrEmpty(monthSearchString))
            {
                searchedWeatherItems = searchedWeatherItems.Where(w => w.Month.Contains(monthSearchString));
            }

            // Sorting data
            ViewBag.SortCol = sortCol;
            ViewBag.SortParam = sortParam == "Desc" ? "Asc" : "Desc";
            var tempSortParam = ViewBag.SortParam;

            switch (sortCol)
			{
            case "Year":
                switch (tempSortParam)
				{
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.Year);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.Year);
                    break;
                default:
                    break;
				}

                break;
            case "Month":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.Date.Substring(3, 2));
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.Date.Substring(3, 2));
                    break;
                default:
                    break;
                }
                
                break;
            case "Date":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.Date);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.Date);
                    break;
                default:
                    break;
                }

                break;
            case "Time":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.Time);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.Time);
                    break;
                default:
                    break;
                }

                break;
            case "AtmosphericTemperature":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.AtmosphericTemperature);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.AtmosphericTemperature);
                    break;
                default:
                    break;
                }

                break;
            case "Humidity":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.Humidity);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.Humidity);
                    break;
                default:
                    break;
                }

                break;
            case "DewPoint":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.DewPoint);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.DewPoint);
                    break;
                default:
                    break;
                }

                break;
            case "AtmosphericPressure":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.AtmosphericPressure);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.AtmosphericPressure);
                    break;
                default:
                    break;
                }

                break;
            case "WindDirection":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.WindDirection);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.WindDirection);
                    break;
                default:
                    break;
                }

                break;
            case "WindSpeed":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.WindSpeed);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.WindSpeed);
                    break;
                default:
                    break;
                }

                break;
            case "Cloudiness":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.Cloudiness);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.Cloudiness);
                    break;
                default:
                    break;
                }

                break;
            case "CloudBase":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.CloudBase);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.CloudBase);
                    break;
                default:
                    break;
                }

                break;
            case "HorizontalVisibility":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.HorizontalVisibility);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.HorizontalVisibility);
                    break;
                default:
                    break;
                }

                break;
            case "WeatherEvents":
                switch (tempSortParam)
                {
                case "Asc":
                    searchedWeatherItems = searchedWeatherItems.OrderBy(w => w.WeatherEvents);
                    break;
                case "Desc":
                    searchedWeatherItems = searchedWeatherItems.OrderByDescending(w => w.WeatherEvents);
                    break;
                default:
                    break;
                }

                break;
            default:
                break;
			}

            // Set pagination options
            var pageSize = 30;
            if (yearSearchString != null || monthSearchString != null)
			{
                if (searchedWeatherItems.Count() <= 31)
				{
                    pageSize = searchedWeatherItems.Count();
                }
			}
            var count = await searchedWeatherItems.CountAsync();
            var pagedWeatherItems = await searchedWeatherItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            // Model for pagination
            PageViewModel pageViewModel = new PageViewModel(count, pageNumber, pageSize);

            // Get year info for drop down list
            var years = new SelectList(rawWeatherItems.Select(s => s.Year).Distinct());

            // Get month info for drop down list
            var months = new SelectList(rawWeatherItems.Select(s => s.Month).Distinct());

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

        public IActionResult Reset()
		{
            ViewData.Clear();

            return RedirectToAction(nameof(ViewWeatherDataController.Index), nameof(ViewWeatherDataController).CutController());
        }
    }
}

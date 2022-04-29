using Microsoft.AspNetCore.Mvc;
using WeatherFeather.Models;
using WeatherFeather.Service;
using WeatherFeather.Domain;
using WeatherFeather.Domain.Entities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace WeatherFeather.Controllers
{
    [Area("Admin")]
    public class UploadWeatherDataController
        : Controller
    {
        private readonly DataManager _dataManager;
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly int _fileSizeLimit;

        public UploadWeatherDataController
        (
            DataManager dataManager,
            AppDbContext appDbContext,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this._dataManager = dataManager;
            this._appDbContext = appDbContext;
            this._webHostEnvironment = webHostEnvironment;
            this._fileSizeLimit = (int)Config.FileSizeLimit;
        }

        public IActionResult Index()
        {
            ViewBag.FileSizeLimit = this._fileSizeLimit / 1024;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFileCollection uploadedFiles)
        {
            // Files without errors counter
            int successfullyUploadedFilesCount = 0;
            
            // Sheet parsing errors counter
            int sheetErrorsCount = 0;
            // Row parsing errors counter
            int rowErrorsCount = 0;
            
            // Successfully uploaded row data counter
            int successfullyUploadedRowDataCount = 0;
            
            // Is too large flag
            bool bIsTooLarge = false;
            // Too large files counter
            int tooLargeFilesCount = 0;

            if (ModelState.IsValid && uploadedFiles != null)
            {
                foreach (var uploadedFile in uploadedFiles)
                {
                    string filePath = "";

                    // Check file size
                    if (uploadedFile.Length > this._fileSizeLimit)
                    {
                        bIsTooLarge = true;
                        ++tooLargeFilesCount;
                        break;
                    }
                    else
                    {
                        // Upload only Excel files
                        if (uploadedFile.FileName.Contains(".xlsx") || uploadedFile.FileName.Contains(".xls"))
                        {
							filePath = this._webHostEnvironment.WebRootPath + "/files/" + uploadedFile.FileName;

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
							{
								await uploadedFile.CopyToAsync(fileStream);

                                FileModel fileRowDB = new FileModel()
                                {
                                    Id = Guid.NewGuid(),
                                    Name = uploadedFile.FileName,
                                    Path = filePath
                                };

                                this._appDbContext.Files.Add(fileRowDB);
                            }

                            // Parse data from Excel file
                            var workbook = new XSSFWorkbook(filePath);
                            var formatter = new DataFormatter();

                            for (int i = 0; i < workbook.NumberOfSheets; ++i)
                            {
                                try
                                {
                                    var sheet = workbook.GetSheetAt(i);

                                    if (sheet != null)
                                    {
                                        // Get year from first data cell
                                        var year = formatter.FormatCellValue(workbook.GetSheetAt(0).GetRow(4).GetCell(0)).Substring(6, 4);

                                        // Get month from sheet name
                                        var sheetName = sheet.SheetName;
                                        var monthIndex = sheetName.IndexOf(" ");
                                        var month = sheet.SheetName[..monthIndex];

                                        for (int row = 4; row < sheet.LastRowNum; ++row)
                                        {
                                            try
                                            {
                                                var currentRow = sheet.GetRow(row);

                                                var weatherItem = new WeatherItem()
                                                {
                                                    Year = year,
                                                    Month = month,
                                                    Date = formatter.FormatCellValue(currentRow.GetCell(0)),
                                                    Time = formatter.FormatCellValue(currentRow.GetCell(1)),
                                                    AtmosphericTemperature = formatter.FormatCellValue(currentRow.GetCell(2)),
                                                    Humidity = formatter.FormatCellValue(currentRow.GetCell(3)),
                                                    DewPoint = formatter.FormatCellValue(currentRow.GetCell(4)),
                                                    AtmosphericPressure = formatter.FormatCellValue(currentRow.GetCell(5)),
                                                    WindDirection = formatter.FormatCellValue(currentRow.GetCell(6)),
                                                    WindSpeed = formatter.FormatCellValue(currentRow.GetCell(7)),
                                                    Cloudiness = formatter.FormatCellValue(currentRow.GetCell(8)),
                                                    CloudBase = formatter.FormatCellValue(currentRow.GetCell(9)),
                                                    HorizontalVisibility = formatter.FormatCellValue(currentRow.GetCell(10)),
                                                    WeatherEvents = formatter.FormatCellValue(currentRow.GetCell(11))
                                                };

                                                if (!this._appDbContext.WeatherItems.Contains(weatherItem))
                                                {
                                                    this._dataManager.WeatherItems.SaveWeatherItem(weatherItem);
                                                    ++successfullyUploadedRowDataCount;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                ++rowErrorsCount;
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    ++sheetErrorsCount;
                                }
                            }
                        }
                    }

                    if (sheetErrorsCount == 0 && rowErrorsCount == 0 && !bIsTooLarge)
					{
                        ++successfullyUploadedFilesCount;
                    }
                }

                ViewBag.SuccessfullyUploadedFilesCount = successfullyUploadedFilesCount;
                ViewBag.SuccessfullyUploadedRowDataCount = successfullyUploadedRowDataCount;
                ViewBag.RowErrorsCount = rowErrorsCount;
                ViewBag.SheetErrorsCount = sheetErrorsCount;
                ViewBag.TooLargeFilesCount = tooLargeFilesCount;
                ViewBag.StatusMsg = "data_catched";
            }

            return View();
        }
    }
}

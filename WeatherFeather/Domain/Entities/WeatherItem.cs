using System.ComponentModel.DataAnnotations;

namespace WeatherFeather.Domain.Entities
{
    public class WeatherItem
        : EntityBase
    {
        [Required(ErrorMessage = "Заполните год")]
        [Display(Name = "Год")]
        public string Year { get; set; }

        [Required(ErrorMessage = "Заполните месяц")]
        [Display(Name = "Месяц")]
        public string Month { get; set; }

        [Required(ErrorMessage = "Заполните дату")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Заполните время")]
        [Display(Name = "Время")]
        public string Time { get; set; }

        [Display(Name = "Температура воздуха T")]
        public string? AtmosphericTemperature { get; set; }

        [Display(Name = "Относительная влажность, %")]
        public string? Humidity { get; set; }

        [Display(Name = "Точка росы Td")]
        public string? DewPoint { get; set; }

        [Display(Name = "Атмосферное давление, мм. рт. ст.")]
        public string? AtmosphericPressure { get; set; }

        [Display(Name = "Направление ветра")]
        public string? WindDirection { get; set; }

        [Display(Name = "Скорость ветра, м/c")]
        public string? WindSpeed { get; set; }

        [Display(Name = "Облачность, %")]
        public string? Cloudiness { get; set; }

        [Display(Name = "Нижняя граница облачности h")]
        public string? CloudBase { get; set; }

        [Display(Name = "Горизонтальная видимость VV, км")]
        public string? HorizontalVisibility { get; set; }

        [Display(Name = "Погодные явления")]
        public string? WeatherEvents { get; set; }
    }
}

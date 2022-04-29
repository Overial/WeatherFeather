using System.ComponentModel.DataAnnotations;

namespace WeatherFeather.Models
{
    public class FileModel
    {
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Путь")]
        public string Path { get; set; }
    }
}

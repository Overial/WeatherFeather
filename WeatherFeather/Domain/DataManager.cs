using WeatherFeather.Domain.Repositories.Abstract;

namespace WeatherFeather.Domain
{
    // Helper class
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IWeatherItemsRepository WeatherItems { get; set; }

        public DataManager
        (
            ITextFieldsRepository textFields,
            IWeatherItemsRepository weatherItems
        )
        {
            this.TextFields = textFields;
            this.WeatherItems = weatherItems;
        }
    }
}

using WeatherFeather.Domain.Entities;

namespace WeatherFeather.Domain.Repositories.Abstract
{
    public interface IWeatherItemsRepository
    {
        IQueryable<WeatherItem> GetWeatherItems();

        WeatherItem GetWeatherItemById(Guid id);

        void SaveWeatherItem(WeatherItem entity);

        void DeleteWeatherItem(Guid id);
    }
}

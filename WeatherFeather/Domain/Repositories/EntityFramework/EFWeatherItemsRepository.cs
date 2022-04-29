using Microsoft.EntityFrameworkCore;
using WeatherFeather.Domain.Entities;
using WeatherFeather.Domain.Repositories.Abstract;

namespace WeatherFeather.Domain.Repositories.EntityFramework
{
    public class EFWeatherItemsRepository
        : IWeatherItemsRepository
    {
        // DI
        private readonly AppDbContext _appDbContext;

        public EFWeatherItemsRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IQueryable<WeatherItem> GetWeatherItems()
        {
            return this._appDbContext.WeatherItems;
        }

        public WeatherItem GetWeatherItemById(Guid id)
        {
            return this._appDbContext.WeatherItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveWeatherItem(WeatherItem entity)
        {
            if (entity.Id == default)
            {
                this._appDbContext.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this._appDbContext.Entry(entity).State = EntityState.Modified;
            }

            this._appDbContext.SaveChanges();
        }

        public void DeleteWeatherItem(Guid id)
        {
            this._appDbContext.WeatherItems.Remove(new WeatherItem() { Id = id });

            this._appDbContext.SaveChanges();
        }
    }
}

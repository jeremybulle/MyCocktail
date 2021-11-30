using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class GlassExtension
    {
        public static Glass ToModel(this GlassDao glassDao)
        {
            return new Glass() { 
                Id = glassDao.Id,
                Name = glassDao.Name 
            };
        }
    }
}

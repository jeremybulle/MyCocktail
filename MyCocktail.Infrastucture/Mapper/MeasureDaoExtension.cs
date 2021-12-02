using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class MeasureDaoExtension
    {
        public static Measure ToModel(this MeasureDao measureDao)
        {
            return new Measure()
            {
                Id = measureDao.Id,
                Ingredient = measureDao.Ingredient.ToModel(),
                Quantity = measureDao.Quantity
            };
        }
    }
}

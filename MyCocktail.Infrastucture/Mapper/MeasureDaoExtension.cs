using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class MeasureDaoExtension
    {
        public static Measure ToModel(this MeasureDao measureDao)
        {
            return new Measure() { 
                Id = measureDao.Id,
                IngredientName = measureDao.Ingredient.Name, 
                Quantity = measureDao.Quantity 
            };
        }
    }
}

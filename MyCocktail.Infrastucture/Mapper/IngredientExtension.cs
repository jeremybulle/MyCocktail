using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;
using System;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class IngredientExtension
    {
        public static IngredientDao ToDao(this Ingredient ingredientToMap)
        {
            return new IngredientDao()
            {
                Id = ingredientToMap.Id ?? Guid.NewGuid(),
                Name = ingredientToMap.Name
            };
        }
    }
}

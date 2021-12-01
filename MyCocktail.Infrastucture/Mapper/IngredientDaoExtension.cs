using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Dao;
using System.Collections.Generic;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class IngredientDaoExtension
    {
        public static Ingredient ToModel(this IngredientDao ingredientDao)
        {
            return new Ingredient() { 
                Id = ingredientDao.Id,
                Name = ingredientDao.Name
            };
        }

        public static IEnumerable<Ingredient> ToModel(this IEnumerable<IngredientDao> ingredientDaos)
        {
            if (ingredientDaos.IsNullOrEmpty())
            {
                return null;
            }
            var ingredientsToReturn = new List<Ingredient>();

            foreach (var ingredient in ingredientDaos)
            {
                ingredientsToReturn.Add(ingredient.ToModel());
            }

            return ingredientsToReturn;
        }
    }
}

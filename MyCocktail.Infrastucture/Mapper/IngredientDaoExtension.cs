using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class IngredientDaoExtension
    {
        public static Ingredient ToModel(this IngredientDao ingredientDao)
        {
            return new Ingredient() { Name = ingredientDao.Name };
        }

        public static IngredientDto ToDto(this IngredientDao ingredientDao)
        {
            return new IngredientDto
            {
                Id = ingredientDao.Id.ToString(),
                Name = ingredientDao.Name
            };
        }

        public static IEnumerable<IngredientDto> ToDto(this IEnumerable<IngredientDao> ingredientsDao)
        {
            if (ingredientsDao.IsNullOrEmpty())
            {
                return null;
            }
            var ingredientsToReturn = new List<IngredientDto>();

            foreach (var ingredient in ingredientsDao)
            {
                ingredientsToReturn.Add(ingredient.ToDto());
            }

            return ingredientsToReturn;
        }
    }
}

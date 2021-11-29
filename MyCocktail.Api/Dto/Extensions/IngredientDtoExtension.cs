using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Dto.Extensions
{
    public static class IngredientDtoExtension
    {
        public static Ingredient ToModel(this IngredientDto ingredientDto)
        {
            return new Ingredient()
            {
                Name = ingredientDto.Name
            };
        }
    }
}

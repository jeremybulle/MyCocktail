using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Api.Mapper
{
    public static class IngredientMapper
    {
        public static IngredientDto ToDto(this Ingredient ingredientToConvert)
        {
            if(ingredientToConvert == null)
            {
                return null;
            }

            return new IngredientDto()
            {
                Id = ingredientToConvert.Id.ToString(),
                Name = ingredientToConvert.Name
            };
        }

        public static IEnumerable<IngredientDto> ToDto(this IEnumerable<Ingredient> ingredientsToConvert)
        {
            if(ingredientsToConvert.IsNullOrEmpty())
            {
                return null;
            }

            return ingredientsToConvert.Select(i => i.ToDto());
        }
    }
}

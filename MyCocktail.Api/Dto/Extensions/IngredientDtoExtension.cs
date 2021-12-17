using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;

namespace MyCocktail.Api.Dto.Extensions
{
    public static class IngredientDtoExtension
    {
        public static Ingredient ToModel(this IngredientDto ingredientDto)
        {
            if (ingredientDto == null)
            {
                return null;
            }
            return new Ingredient()
            {
                Id = ingredientDto.Id == null ? null : new Guid(ingredientDto.Id),
                Name = ingredientDto.Name
            };
        }
    }
}

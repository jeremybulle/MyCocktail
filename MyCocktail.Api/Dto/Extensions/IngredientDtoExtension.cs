using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;

namespace MyCocktail.Api.Dto.Extensions
{
    public static class IngredientDtoExtension
    {
        public static Ingredient ToModel(this IngredientDto ingredientDto)
        {
            return new Ingredient()
            {
                Id = ingredientDto == null ? null : new Guid(ingredientDto.Id),
                Name = ingredientDto.Name
            };
        }
    }
}

using MyCocktail.Domain.Aggregates.DrinkAggregate;
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
                Id = new Guid(ingredientDto.Id),
                Name = ingredientDto.Name
            };
        }
    }
}

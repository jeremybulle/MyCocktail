using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Mapper
{
    public static class DrinkMapper
    {
        public static DrinkPartialDto ToPartialDto(this Drink drinkToConvert)
        {
            if (drinkToConvert == null)
            {
                return null;
            }

            return new DrinkPartialDto()
            {
                AlcoholicName = drinkToConvert.Alcoholic.Name,
                CategoryName = drinkToConvert.Category.Name,
                GlassName = drinkToConvert.Glass.Name,
                Name = drinkToConvert.Name,
                UrlPicture = drinkToConvert.UrlPicture.ToString(),
                Id = drinkToConvert.Id.ToString()
            };
        }

        public static IEnumerable<DrinkPartialDto> ToPartialDto(this IEnumerable<Drink> drinksToConvert)
        {
            if (drinksToConvert.IsNullOrEmpty())
            {
                return null;
            }

            return drinksToConvert.Select(d => d.ToPartialDto());
        }

        public static DrinkDto ToDto(this Drink drinkToConvert)
        {
            if(drinkToConvert == null)
            {
                return null;
            }

            return new DrinkDto()
            {
                Id = drinkToConvert.Id.ToString(),
                Alcoholic = drinkToConvert.Alcoholic.ToDto(),
                Category = drinkToConvert.Category.ToDto(),
                DateModified = drinkToConvert.DateModified.ToString(),
                Glass = drinkToConvert.Glass.ToDto(),
                IdOwner = drinkToConvert.IdOwner.ToString(),
                IdSource = drinkToConvert.IdSource,
                Instruction = drinkToConvert.Instruction,
                Name = drinkToConvert.Name,
                UrlPicture = drinkToConvert.UrlPicture.ToString(),
                Measures = drinkToConvert.GetMeasures().ToDto()
            };

        }

        public static IEnumerable<DrinkDto> ToDto(this IEnumerable<Drink> drinksToConvert)
        {
            if (drinksToConvert.IsNullOrEmpty())
            {
                return null;
            }

            return drinksToConvert.Select(d => d.ToDto());
        }
    }
}

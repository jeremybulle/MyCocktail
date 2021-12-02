using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Api.Mapper
{
    public static class MeasureMapper
    {
        public static MeasureDto ToDto(this Measure measureToConvert)
        {
            if (measureToConvert == null)
            {
                return null;
            }

            return new MeasureDto()
            {
                Id = measureToConvert.Id.ToString(),
                Quantity = measureToConvert.Quantity,
                Ingedient = measureToConvert.Ingredient.ToDto(),
            };
        }

        public static IEnumerable<MeasureDto> ToDto(this IEnumerable<Measure> measuresToConvert)
        {
            if (measuresToConvert.IsNullOrEmpty())
            {
                return null;
            }

            return measuresToConvert.Select(m => m.ToDto());
        }
    }
}

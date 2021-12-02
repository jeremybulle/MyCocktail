using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.DrinkAggregate;

namespace MyCocktail.Api.Mapper
{
    public static class AlcoholicMapper
    {
        public static AlcoholicDto ToDto(this Alcoholic alcoholicToConvert)
        {
            if (alcoholicToConvert == null)
            {
                return null;
            }

            return new AlcoholicDto()
            {
                Id = alcoholicToConvert.Id.ToString(),
                Name = alcoholicToConvert.Name
            };
        }
    }
}

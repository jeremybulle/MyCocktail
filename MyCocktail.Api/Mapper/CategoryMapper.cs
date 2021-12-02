using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.DrinkAggregate;

namespace MyCocktail.Api.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryDto()
            {
                Id = category.Id.ToString(),
                Name = category.Name
            };
        }
    }
}

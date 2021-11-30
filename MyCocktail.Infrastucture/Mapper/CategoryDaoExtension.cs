using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class CategoryDaoExtension
    {
        public static Category ToModel(this CategoryDao categoryDao)
        {
            return new Category()
            {
                Id = categoryDao.Id,
                Name = categoryDao.Name,
            };
        }
    }
}

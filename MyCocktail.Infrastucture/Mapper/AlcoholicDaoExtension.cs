using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class AlcoholicDaoExtension
    {
        public static Alcoholic ToModel(this AlcoholicDao alcoholicDao)
        {
            return new Alcoholic() { Id = alcoholicDao.Id, Name = alcoholicDao.Name };
        }
    }
}

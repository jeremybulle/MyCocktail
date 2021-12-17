using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;
using System;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class GlassExtension
    {
        public static Glass ToModel(this GlassDao glassDao)
        {
            if(glassDao == null)
            {
                throw new ArgumentNullException(nameof(glassDao));
            }
            return new Glass()
            {
                Id = glassDao.Id,
                Name = glassDao.Name
            };
        }
    }
}

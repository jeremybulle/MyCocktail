using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;
using System;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class DrinkDaoExtension
    {
        public static Drink ToModel(this DrinkDao drinkDao)
        {
            if (drinkDao != null)
            {
                var drinkToReturn = new Drink()
                {
                    Id = drinkDao.Id,
                    Alcoholic = drinkDao.Alcoholic.ToModel(),
                    Category = drinkDao.Category.ToModel(),
                    DateModified = drinkDao.DateModified,
                    Glass = drinkDao.Glass.ToModel(),
                    IdSource = drinkDao.IdSource,
                    IdOwner = drinkDao.OwnerId,
                    Instruction = drinkDao.Instruction,
                    Name = drinkDao.Name,
                    UrlPicture = drinkDao.UrlPicture != null ? new Uri(drinkDao.UrlPicture) : null,
                };

                foreach (var measure in drinkDao.Measures)
                {
                    drinkToReturn.AddMeasure(new Measure() { Id = measure.Id, Quantity = measure.Quantity, Ingredient = new Ingredient() { Id = measure.Ingredient.Id, Name = measure.Ingredient.Name } });
                }

                return drinkToReturn;
            }
            return null;
        }
    }
}

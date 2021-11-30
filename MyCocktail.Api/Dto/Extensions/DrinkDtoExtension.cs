using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;

namespace MyCocktail.Api.Dto.Extensions
{
    public static class DrinkDtoExtension
    {
        public static Drink ToModel(this DrinkDto drinkDto)
        {
            Drink drinkToRetunr;
            try
            {
                drinkToRetunr = new Drink()
                {
                    Name = drinkDto.Name,
                    Instruction = drinkDto.Instruction,
                    UrlPicture = drinkDto.UrlPicture.IsNullOrEmpty() ? null : new Uri(drinkDto.UrlPicture),
                    Alcoholic = new Alcoholic() { Name = drinkDto.Alcoholic.Name },
                    Category = new Category() { Name = drinkDto.Category.Name },
                    Glass = new Glass() { Name = drinkDto.Glass.Name },
                    IdSource = drinkDto.IdSource,
                    IdOwner = Guid.Parse(drinkDto.IdOwner)
                };

                var measures = new List<Measure>();

                foreach (var measureDto in drinkDto.Measures)
                {
                    drinkToRetunr.AddMeasure(measureDto.Ingedient.Name, measureDto.Quantity);
                }
            }
            catch
            {
                throw new ArgumentException(nameof(DrinkDto));
            }

            return drinkToRetunr;
        }
    }
}

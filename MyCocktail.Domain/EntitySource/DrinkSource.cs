using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;

namespace MyCocktail.Domain.EntitySource
{
    public class DrinksSource
    {
        public DrinkSource[] drinks { get; set; }
    }

    /// <summary>
    /// Data object for carry drink send by original cocktailDb API
    /// </summary>
    public class DrinkSource
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public string strDrinkAlternate { get; set; }
        public string strTags { get; set; }
        public string strVideo { get; set; }
        public string strCategory { get; set; }
        public string strIBA { get; set; }
        public string strAlcoholic { get; set; }
        public string strGlass { get; set; }
        public string strInstructions { get; set; }
        public string strInstructionsES { get; set; }
        public string strInstructionsDE { get; set; }
        public string strInstructionsFR { get; set; }
        public string strInstructionsIT { get; set; }
        public string strInstructionsZHHANS { get; set; }
        public string strInstructionsZHHANT { get; set; }
        public string strDrinkThumb { get; set; }
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        public string strMeasure1 { get; set; }
        public string strMeasure2 { get; set; }
        public string strMeasure3 { get; set; }
        public string strMeasure4 { get; set; }
        public string strMeasure5 { get; set; }
        public string strMeasure6 { get; set; }
        public string strMeasure7 { get; set; }
        public string strMeasure8 { get; set; }
        public string strMeasure9 { get; set; }
        public string strMeasure10 { get; set; }
        public string strMeasure11 { get; set; }
        public string strMeasure12 { get; set; }
        public string strMeasure13 { get; set; }
        public string strMeasure14 { get; set; }
        public string strMeasure15 { get; set; }
        public string strImageSource { get; set; }
        public string strImageAttribution { get; set; }
        public string strCreativeCommonsConfirmed { get; set; }
        public string dateModified { get; set; }

        public IDictionary<int, string> GetIngredientStr()
        {
            var dict = new Dictionary<int, string>();

            dict[1] = strIngredient1.IsNullOrEmpty() ? null : strIngredient1;
            dict[2] = strIngredient2.IsNullOrEmpty() ? null : strIngredient2;
            dict[3] = strIngredient3.IsNullOrEmpty() ? null : strIngredient3;
            dict[4] = strIngredient4.IsNullOrEmpty() ? null : strIngredient4;
            dict[5] = strIngredient5.IsNullOrEmpty() ? null : strIngredient5;
            dict[6] = strIngredient6.IsNullOrEmpty() ? null : strIngredient6;
            dict[7] = strIngredient7.IsNullOrEmpty() ? null : strIngredient7;
            dict[8] = strIngredient8.IsNullOrEmpty() ? null : strIngredient8;
            dict[9] = strIngredient9.IsNullOrEmpty() ? null : strIngredient9;
            dict[10] = strIngredient10.IsNullOrEmpty() ? null : strIngredient10;
            dict[11] = strIngredient11.IsNullOrEmpty() ? null : strIngredient11;
            dict[12] = strIngredient12.IsNullOrEmpty() ? null : strIngredient12;
            dict[13] = strIngredient13.IsNullOrEmpty() ? null : strIngredient13;
            dict[14] = strIngredient14.IsNullOrEmpty() ? null : strIngredient14;
            dict[15] = strIngredient15.IsNullOrEmpty() ? null : strIngredient15;

            return dict;
        }

        public IDictionary<int, string> GetMeasureStr()
        {
            var dict = new Dictionary<int, string>();

            dict[1] = strMeasure1.IsNullOrEmpty() ? null : strMeasure1;
            dict[2] = strMeasure2.IsNullOrEmpty() ? null : strMeasure2;
            dict[3] = strMeasure3.IsNullOrEmpty() ? null : strMeasure3;
            dict[4] = strMeasure4.IsNullOrEmpty() ? null : strMeasure4;
            dict[5] = strMeasure5.IsNullOrEmpty() ? null : strMeasure5;
            dict[6] = strMeasure6.IsNullOrEmpty() ? null : strMeasure6;
            dict[7] = strMeasure7.IsNullOrEmpty() ? null : strMeasure7;
            dict[8] = strMeasure8.IsNullOrEmpty() ? null : strMeasure8;
            dict[9] = strMeasure9.IsNullOrEmpty() ? null : strMeasure9;
            dict[10] = strMeasure10.IsNullOrEmpty() ? null : strMeasure10;
            dict[11] = strMeasure11.IsNullOrEmpty() ? null : strMeasure11;
            dict[12] = strMeasure12.IsNullOrEmpty() ? null : strMeasure12;
            dict[13] = strMeasure13.IsNullOrEmpty() ? null : strMeasure13;
            dict[14] = strMeasure14.IsNullOrEmpty() ? null : strMeasure14;
            dict[15] = strMeasure15.IsNullOrEmpty() ? null : strMeasure15;

            return dict;
        }

        public Drink ToModel()
        {
            var alcoholic = new Alcoholic() { Name = strAlcoholic.Trim().ToLower() };
            var category = new Category() { Name = strCategory.Trim().ToLower() };
            var glass = new Glass() { Name = strGlass.Trim().ToLower() };

            var drinkToRetunr = new Drink()
            {
                IdSource = idDrink,
                Name = strDrink.Trim().ToLower(),
                Alcoholic = alcoholic,
                Category = category,
                Glass = glass,
                UrlPicture = new Uri(strDrinkThumb.Trim()),
                Instruction = strInstructions.Trim(),
                DateModified = dateModified.IsNullOrEmpty() ? DateTime.Now : DateHelper.DateFromString(dateModified),
                IdOwner = null
            };

            var ingredients = GetIngredientStr();
            var measures = GetMeasureStr();
            ingredients.PurgeEmptyAndNullValue();

            foreach (var entry in ingredients)
            {
                string measurestr;
                if (measures.TryGetValue(entry.Key, out measurestr))
                {
                    drinkToRetunr.AddMeasure(entry.Value.Trim(), measurestr);
                }
            }

            return drinkToRetunr;

        }
    }
}

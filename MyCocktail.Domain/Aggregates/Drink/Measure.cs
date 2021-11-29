using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.Drink
{
    /// <summary>
    /// Measures is an <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Ingredient"/> associated to a quantity.
    /// Measures compose <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/> 
    /// </summary>
    public class Measure
    {
        private string _name;
        public string IngredientName
        {
            get
            {
                return new string(_name);
            }
            init
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(IngredientName));
                }
                _name = value.Trim();
                _name = IngredientName.ToLower();
            }
        }

        private string _quantity;
        public string Quantity
        {
            get
            {
                return new string(_quantity);
            }
            init
            {
                if (value.IsNullOrEmpty())
                {
                    _quantity = null;
                }
                else
                {
                    _quantity = value.Trim();
                    _quantity = Quantity.ToLower();
                }

            }
        }

        public override bool Equals(object obj)
        {
            return obj is Measure measure &&
                   IngredientName == measure.IngredientName &&
                   Quantity == measure.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IngredientName, Quantity);
        }
    }
}

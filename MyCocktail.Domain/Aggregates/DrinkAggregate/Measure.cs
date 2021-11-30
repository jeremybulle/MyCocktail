using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Measures is an <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Ingredient"/> associated to a quantity.
    /// Measures compose <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/> 
    /// </summary>
    public class Measure
    {
        #region Id
        private Guid? _id;
        public Guid? Id
        {
            get
            {
                if (_id == null)
                {
                    return null;
                }

                return _id == null ? null : new Guid(_id.ToString());
            }
            init
            {
                _id = value;
            }
        }
        #endregion

        #region IngredientName
        private string _ingredientName;
        public string IngredientName
        {
            get
            {
                return new string(_ingredientName);
            }
            init
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(IngredientName));
                }
                _ingredientName = value.Trim();
                _ingredientName = IngredientName.ToLower();
            }
        }
        #endregion

        #region Quantity
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
        #endregion


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

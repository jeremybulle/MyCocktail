using MyCocktail.Domain.Helper;
using System;

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

        #region Ingredient
        private Ingredient _ingredient;
        public Ingredient Ingredient
        {
            get
            {
                return new Ingredient()
                {
                    Id = _ingredient.Id,
                    Name = _ingredient.Name,
                };
            }
            init
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Ingredient));
                }
                _ingredient = value;
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
                   Ingredient.Name == measure.Ingredient.Name &&
                   Quantity == measure.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ingredient.Name, Quantity);
        }
    }
}

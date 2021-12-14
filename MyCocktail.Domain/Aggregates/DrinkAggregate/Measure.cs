using MyCocktail.Domain.Helper;
using System;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Measures is an <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> associated to a quantity.
    /// Measures is a component of <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> 
    /// </summary>
    public class Measure
    {
        #region Id
        private Guid? _id;

        /// <summary>
        /// Unique Id
        /// </summary>
        public Guid? Id
        {
            get
            {
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

        /// <summary>
        /// <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> composing this Measure
        /// </summary>
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

        /// <summary>
        /// Quantity related to this measure and this measure's <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/>
        /// </summary>
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

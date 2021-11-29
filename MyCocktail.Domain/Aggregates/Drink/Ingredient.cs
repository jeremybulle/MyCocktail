using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.Drink
{
    /// <summary>
    /// Ingredient used in <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Measure"/> to composed <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/>
    /// </summary>
    public class Ingredient
    {
        private string _name;
        public string Name
        {
            get
            {
                return new string(_name);
            }
            init
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(Name));
                }
                _name = value.Trim();
                _name = Name.ToLower();
            }
        }

        public static bool operator ==(Ingredient a, Ingredient b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Ingredient a, Ingredient b)
        {

            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Ingredient ingredient &&
                   Name == ingredient.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}

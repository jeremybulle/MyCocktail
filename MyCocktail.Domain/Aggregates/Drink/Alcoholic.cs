using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.Drink
{
    /// <summary>
    /// Precise Alcohol presence in a <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/>
    /// </summary>
    public class Alcoholic
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

        public static bool operator ==(Alcoholic a, Alcoholic b)
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

        public static bool operator !=(Alcoholic a, Alcoholic b)
        {

            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Alcoholic alcoholic &&
                   Name == alcoholic.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}

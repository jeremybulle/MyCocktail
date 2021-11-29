using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.Drink
{
    /// <summary>
    /// Category of <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/>, for exemple : coffe/tea, beer, shot, etc...
    /// </summary>
    public class Category
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

        public static bool operator ==(Category a, Category b)
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

        public static bool operator !=(Category a, Category b)
        {

            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}

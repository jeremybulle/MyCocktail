using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Precise Alcohol presence in a <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/>
    /// </summary>
    public class Alcoholic
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

        #region Name
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
        #endregion


        //public static bool operator ==(Alcoholic a, Alcoholic b)
        //{
        //    if (a == null && b == null)
        //    {
        //        return true;
        //    }
        //    if (a == null || b == null)
        //    {
        //        return false;
        //    }

        //    return a.Equals(b);
        //}

        //public static bool operator !=(Alcoholic a, Alcoholic b)
        //{

        //    return !(a == b);
        //}

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

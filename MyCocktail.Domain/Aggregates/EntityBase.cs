using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates
{
    public class EntityBase
    {
        #region Id
        protected Guid? _id;
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

        #region Name
        protected string _name;
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

        public override bool Equals(object obj)
        {
            return obj is EntityBase entityBase &&
                   Name == entityBase.Name &&
                   Id == entityBase.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name,Id);
        }
    }
}

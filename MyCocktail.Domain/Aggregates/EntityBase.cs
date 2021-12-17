using MyCocktail.Domain.Helper;
using System;

namespace MyCocktail.Domain.Aggregates
{
    /// <summary>
    /// Class base for several entity in this project
    /// </summary>
    public class EntityBase
    {
        #region Id
        protected Guid? _id;
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

        #region Name
        protected string _name;
        /// <summary>
        /// Entity's name, can not be bull or empty, it will be trimed and lowercased
        /// </summary>
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
            return HashCode.Combine(Name, Id);
        }
    }
}

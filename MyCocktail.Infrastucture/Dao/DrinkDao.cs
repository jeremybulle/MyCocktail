using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class DrinkDao
    {
        public Guid Id;
        public string Name;
        public string UrlPicture;
        public string IdSource;
        public string Instruction;
        public DateTime DateModified;

        public Guid? CategoryId;
        public virtual CategoryDao Category { get; set; }

        public Guid GlassId;
        public virtual GlassDao Glass { get; set; }

        public Guid AlcoholicId;
        public virtual AlcoholicDao Alcoholic { get; set; }

        public Guid? OwnerId;
        public virtual UserDao Owner { get; set; }

        public virtual ICollection<MeasureDao> Measures { get; set; }

        public virtual ICollection<FavoriteDao> Favorites { get; set; }
    }
}

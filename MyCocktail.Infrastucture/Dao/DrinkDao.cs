using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Dao
{
    public class DrinkDao
    {
        public Guid Id;
        public string Name;
        public string UrlPicture;
        public string IdSource;
        public string Instruction;
        public DateTime DateModified;

        public Guid? CategoryId;
        public CategoryDao Category;

        public Guid GlassId;
        public GlassDao Glass;

        public Guid AlcoholicId;
        public AlcoholicDao Alcoholic;

        public Guid? OwnerId;
        public UserDao Owner;

        public ICollection<MeasureDao> Measures;

        public ICollection<FavoriteDao> Favorites;
    }
}

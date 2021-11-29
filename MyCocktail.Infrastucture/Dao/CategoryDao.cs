using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Dao
{
    public class CategoryDao
    {
        public Guid Id;
        public string Name;
        public ICollection<DrinkDao> Drinks;
    }
}

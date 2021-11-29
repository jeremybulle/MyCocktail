using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class AlcoholicDaoExtension
    {
        public static Alcoholic ToModel(this AlcoholicDao alcoholicDao)
        {
            return new Alcoholic() { Name = alcoholicDao.Name };
        }
    }
}

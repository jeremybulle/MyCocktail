using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class CategoryDaoExtension
    {
        public static Category ToModel(this CategoryDao categoryDao)
        {
            return new Category()
            {
                Name = categoryDao.Name,
            };
        }
    }
}

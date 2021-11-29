using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class GlassExtension
    {
        public static Glass ToModel(this GlassDao glassDao)
        {
            return new Glass() { Name = glassDao.Name };
        }
    }
}

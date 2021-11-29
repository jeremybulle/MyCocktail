using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Helper
{
    public static class DateHelper
    {
        /// <summary>
        /// Convert string date as DateTime year month day
        /// </summary>
        /// <param name="strDate">should be like yyyy-mm-dd hh:mm:ss</param>
        /// <returns></returns>
        public static DateTime DateFromString(string strDate)
        {
            if (strDate.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(strDate));
            }
            var ymd = strDate.Split(" ");
            ymd = ymd[0].Split("-");

            return new DateTime(int.Parse(ymd[0]), int.Parse(ymd[1]), int.Parse(ymd[2]));
        }
    }
}

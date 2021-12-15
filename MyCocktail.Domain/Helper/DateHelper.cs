using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Domain.Helper
{
    public static class DateHelper
    {
        /// <summary>
        /// Convert string date as <see langword="Datetime"/> year month day
        /// </summary>
        /// <param name="strDate">should be like yyyy-mm-dd hh:mm:ss</param>
        /// <returns>A <see langword="Datetime"/></returns>
        public static DateTime DateFromString(string strDate)
        {
            if (strDate.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(strDate));
            }
            var ymd = strDate.Split(" ")[0];
            var hms = strDate.Split(" ")[1];

            var ymdhms = new List<String>();

            ymdhms.AddRange(ymd.Split("-"));
            ymdhms.AddRange(hms.Split(":"));

            var ymdhmsConverted = new int[ymd.Length + hms.Length];

            for (int i = 0; i < ymdhms.Count; i++)
            {
                try
                {
                    var number = Int32.Parse(ymdhms[i].ToString());
                    ymdhmsConverted[i] = number < 0 ? throw new ArgumentException(nameof(strDate)) : number;
                }
                catch (Exception)
                {

                    throw new ArgumentException(nameof(strDate));
                }
            }

            return new DateTime(ymdhmsConverted[0], ymdhmsConverted[1], ymdhmsConverted[2], ymdhmsConverted[3], ymdhmsConverted[4], ymdhmsConverted[5]);
        }
    }
}

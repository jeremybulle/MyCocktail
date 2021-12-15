using System;
using System.Linq;

namespace MyCocktail.Domain.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// Allow to know if a <see langword="string"/> contains unauthorized <see langword="char"/>
        /// </summary>
        /// <param name="stringToTest"><see langword="string"/> to test</param>
        /// <param name="UnAutorizedChar"><see langword="string"/> containing all unauthorized <see langword="char"/></param>
        /// <returns><see langword="true"/> if tested <see langword="string"/> contains at least one unauthorized <see langword="char"/>, <see langword="false"/> if tested <see langword="string"/> does not contain unauthorized <see langword="char"/></returns>
        public static bool ContainUnAuhtorizedChar(this string stringToTest, string UnAutorizedChar = "@0123456789/:.;,?§!%*¨^£$¤*-+{}²&~#()|\\<>°")
        {
            if (stringToTest == null)
            {
                throw new ArgumentNullException(nameof(stringToTest));
            }

            if (UnAutorizedChar == null)
            {
                throw new ArgumentNullException(nameof(UnAutorizedChar));
            }

            if (stringToTest.Any(letter => UnAutorizedChar.Contains(letter)))
            {
                return true;
            }
            return false;
        }
    }
}

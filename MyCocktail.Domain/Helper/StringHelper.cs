using System;
using System.Linq;

namespace MyCocktail.Domain.Helper
{
    public static class StringHelper
    {
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

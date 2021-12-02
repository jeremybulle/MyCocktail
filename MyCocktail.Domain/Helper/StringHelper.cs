﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Helper
{
    public static class StringHelper
    {
        public static bool ContainUnAuhtorizedChar(this string stringToTest, string UnAutorizedChar = "@0123456789/:.;,?§!%*¨^£$¤*-+{}²&~#()|\\<>°")
        {
            if (stringToTest.Any(letter => UnAutorizedChar.Contains(letter)))
            {
                return true;
            }
            return false;
        }
    }
}

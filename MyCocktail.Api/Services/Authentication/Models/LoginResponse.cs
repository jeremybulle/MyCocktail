﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Services.Authentication.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}

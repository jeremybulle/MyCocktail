using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Mapper
{
    public static class GlassMapper
    {
        public static GlassDto ToDto(this Glass glassToConvert)
        {
            if(glassToConvert == null)
            {
                return null;
            }

            return new GlassDto()
            {
                Id = glassToConvert.Id.ToString(),
                Name = glassToConvert.Name
            };
        }
    }
}

﻿using MyCocktail.Domain.Helper;
using System;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Precise Alcohol presence in a <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/>
    /// </summary>
    public class Alcoholic : EntityBase
    {
        public override bool Equals(object obj)
        {
            return obj is Alcoholic alcoholic &&
                   Name == alcoholic.Name &&
                   Id == alcoholic.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name,Id);
        }
    }
}

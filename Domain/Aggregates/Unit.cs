using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public enum UnitType
    {
        Single,
        Package,
        Bulk,
    }
    public class Unit: Entity<int>
    {
        public UnitType Type { get; set; }
        public static Unit CreateUnit(string name, UnitType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid unit data.");
            return new Unit
            {
                Type = type,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public abstract class Entity<Tid>
    {
        public Tid? Id { get; set; }
    }
}

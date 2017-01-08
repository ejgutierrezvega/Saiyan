using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saiyan.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid id { get; set; }
    }
}

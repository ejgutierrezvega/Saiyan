using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saiyan.Repository
{
    public sealed class IdEngine : IIdEngine
    {
        public Guid NewGuid()
        {
            return IdGenerator.NewGuid(DateTime.UtcNow.Ticks);
        }
    }
}

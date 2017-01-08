using System;

namespace Saiyan.Repository
{
    public interface IIdEngine
    {
        Guid NewGuid();
    }
}

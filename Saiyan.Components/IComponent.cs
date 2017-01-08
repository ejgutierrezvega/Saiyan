
using System;
using System.Collections.Generic;

namespace Saiyan.Components
{
    public interface IComponent<T>
    {
        IEnumerable<T> Get();
        T GetById(Guid id);
        void Add(T model);
    }
}

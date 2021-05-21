using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Core.Interfaces
{
    public interface ISeedableEntity<T>
    {
        T Update(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Core.Interfaces
{
    public interface ICloneableEntity<T>
    {
        T Clone();
    }
}

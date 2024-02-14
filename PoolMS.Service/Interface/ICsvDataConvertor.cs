using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.Interface
{
    public interface ICsvDataConvertor<T> : IDataConvertor<T> where T : class
    {
    }
}

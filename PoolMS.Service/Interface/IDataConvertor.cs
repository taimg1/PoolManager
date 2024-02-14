using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.Interface
{
    public interface IDataConvertor<T> :IFilter
    {
        Task<IEnumerable<T>> Read(string filepath);
        Task<string> Write(IEnumerable<T> data); 
    }
}

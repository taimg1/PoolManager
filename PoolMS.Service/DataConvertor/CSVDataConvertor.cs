using PoolMS.Service.Interface;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.DataConvertor
{
    public class CSVDataConvertor<T>
    {
        public string Filter => "CSV files (*.csv)|*.csv";

        public async Task<IEnumerable<T>> Read(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var content = await reader.ReadToEndAsync();
            return CsvSerializer.DeserializeFromString<List<T>>(content);
        }

        public async Task<string> Write(IEnumerable<T> data)
        {
            var content = CsvSerializer.SerializeToString(data);
            return content;
        }
    }
}

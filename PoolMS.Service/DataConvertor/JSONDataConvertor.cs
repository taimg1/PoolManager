using PoolMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoolMS.Service.DataConvertor
{
    public class JSONDataConvertor<T>: IJsonDataConvertor<T> where T : class
    {
        public string Filter => "JSON files (*.json)|*.json";

        public async Task<IEnumerable<T>> Read(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var content = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<IEnumerable<T>>(content);
        }

        public async Task<string> Write(IEnumerable<T> data)
        {
            var content = JsonSerializer.Serialize(data);
            return content;
        }
    }
    
}

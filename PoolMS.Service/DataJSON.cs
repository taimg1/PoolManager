using DataManager.Service.Service;
using DataSet.Service;
using System.Text.Json;

namespace LW_53.Service.DataManager
{
    public class DataJSON<T> : ISingleEntityDataManager<T>
    {
        string IFilter.Filter => "Json files (*.json)|*.json";

        IEnumerable<T> ISingleEntityDataManager<T>.Read(string path)
        {
            string json = File.OpenText(path).ReadToEnd();
            return JsonSerializer.Deserialize<IEnumerable<T>>(json).ToList();
        }

        void ISingleEntityDataManager<T>.Write(string path, IEnumerable<T> data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
        }
    }
}

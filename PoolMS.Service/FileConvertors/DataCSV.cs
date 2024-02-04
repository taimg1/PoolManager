using DataManager.Service.Service;
using DataSet.Service;
using ServiceStack.Text;



namespace LW_53.Service.DataManager
{
    public class DataCSV<T> : ISingleEntityDataManager<T>
    {

        string IFilter.Filter => "CSV files (*.csv)|*.csv";

        IEnumerable<T> ISingleEntityDataManager<T>.Read(string path)
        {
            return CsvSerializer.DeserializeFromString<IEnumerable<T>>(File.ReadAllText(path));

        }

        void ISingleEntityDataManager<T>.Write(string path, IEnumerable<T> data)
        {
            File.WriteAllText(path, CsvSerializer.SerializeToCsv(data));
        }
    }
}

using DataManager.Service.Service;

namespace DataSet.Service
{
    public interface ISingleEntityDataManager<T> : IFilter
    {

        public IEnumerable<T> Read(string path);
        void Write(string path, IEnumerable<T> data);
    }

}

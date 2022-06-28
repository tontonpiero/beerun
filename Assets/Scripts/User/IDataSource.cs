using System.Threading.Tasks;

namespace BeeRun
{
    public interface IDataSource<T> where T : new()
    {
        Task<T> LoadData();
        void SaveData(T data);
    }
}

using System.Threading.Tasks;

namespace BeeRun
{
    public interface ILevelManager
    {
        void Load(string name, bool additive = false);
        Task LoadAsync(string name, bool additive = false);
        void Restart();
        Task RestartAsync();
    }
}
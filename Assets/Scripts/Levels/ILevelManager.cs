namespace BeeRun
{
    public interface ILevelManager
    {
        void Load(string name, bool additive = false);
        void Restart();
    }
}
using WebApplication3.Models;

namespace WebApplication3.Service.Remote
{
    public interface IRemoteService
    {
        Task<int> setKeyRomote(int id, int key);
        Task<int> getKeyRomote(int id);
        Task<Remotes> GetRemote(int id);
    }
}

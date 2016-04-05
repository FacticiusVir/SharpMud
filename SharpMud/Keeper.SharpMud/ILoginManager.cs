using System.Threading.Tasks;

namespace Keeper.SharpMud
{
    public interface ILoginManager
    {
        Task<LoginResult> Login(ISession session);
    }
}

using System.Net;
using System.Threading.Tasks;

namespace Keeper.SharpMud
{
    public interface ISession
    {
        EndPoint RemoteEndPoint { get; }

        bool IsOpen { get; }

        Task Close();

        Task SendAsync(string message);

        Task<string> ReceiveAsync();
    }
}
using System.Threading.Tasks;

namespace Keeper.SharpMud
{
    public interface ICommand<in TState>
    {
        Task ExecuteAsync(ISession session, TState gameState, string[] parts);
    }
}
using System.Threading.Tasks;

namespace Keeper.SharpMud
{
    public class QuitCommand
        : ICommand<object>
    {
        private readonly string message;

        public QuitCommand(string message = null)
        {
            this.message = message;
        }

        public async Task ExecuteAsync(ISession session, object state, string[] parts)
        {
            if (this.message != null)
            {
                await session.SendAsync(this.message);
            }

            await session.Close();
        }
    }
}
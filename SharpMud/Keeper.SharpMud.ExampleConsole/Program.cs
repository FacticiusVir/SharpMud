using System.Threading;

namespace Keeper.SharpMud.ExampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server(5000);

            server.Start();

            var waitHandle = new AutoResetEvent(false);

            server.NewSession += async session =>
            {
                await session.SendAsync("Test!");

                await session.Close();

                waitHandle.Set();
            };

            waitHandle.WaitOne();

            server.Stop();
        }
    }
}
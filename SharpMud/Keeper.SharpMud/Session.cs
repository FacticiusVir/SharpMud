using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Keeper.SharpMud
{
    public class Session
        : ISession
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        private bool isClosed;

        public Session(TcpClient client)
        {
            this.client = client;

            var stream = client.GetStream();

            this.reader = new StreamReader(stream);
            this.writer = new StreamWriter(stream);
        }

        public EndPoint RemoteEndPoint
        {
            get
            {
                return this.client.Client.RemoteEndPoint;
            }
        }

        public bool IsOpen
        {
            get
            {
                return !this.isClosed;
            }
        }

        public async Task SendAsync(string message)
        {
            await this.writer.WriteLineAsync(message);
            await this.writer.FlushAsync();
        }

        public Task<string> ReceiveAsync()
        {
            return this.reader.ReadLineAsync();
        }

        public async Task Close()
        {
            await Task.Run(() =>
                {
                    if (!this.isClosed)
                    {
                        this.isClosed = true;

                        this.client.Close();
                        this.client = null;
                        this.reader = null;
                        this.writer = null;
                    }
                });
        }
    }
}
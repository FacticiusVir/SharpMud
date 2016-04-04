using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Keeper.SharpMud
{
    public class Server
        : IServer
    {
        private readonly TcpListener listener;
        private readonly List<Session> sessions = new List<Session>();

        public Server(IPEndPoint endPoint)
        {
            this.listener = new TcpListener(endPoint);
        }

        public Server(int port, IPAddress address = null)
        {
            this.listener = new TcpListener(address ?? IPAddress.Any, port);
        }

        public void Start()
        {
            this.listener.Start();

            this.BeginAccept();
        }

        public void Stop()
        {
            this.listener.Stop();

            foreach(var session in this.sessions)
            {
                session.Close();
            }

            this.sessions.Clear();
        }

        private void BeginAccept()
        {
            Task.Run(async () =>
            {
                var client = await this.listener.AcceptTcpClientAsync();

                this.BeginAccept();

                var newSession = new Session(client);

                this.sessions.Add(newSession);
                
                this.OnNewSession(newSession);
            });
        }

        private void OnNewSession(Session session)
        {
            var newSession = this.NewSession;

            if (newSession != null)
            {
                newSession(session);
            }
        }

        public event Action<ISession> NewSession;
    }
}

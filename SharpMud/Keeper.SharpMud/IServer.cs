using System;

namespace Keeper.SharpMud
{
    public interface IServer
    {
        event Action<ISession> NewSession;

        void Start();
        void Stop();
    }
}
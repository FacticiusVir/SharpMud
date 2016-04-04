using System.Diagnostics;

namespace Keeper.SharpMud
{
    public interface ILogging
    {
        void Log(string message, TraceLevel level = TraceLevel.Info);
    }
}

using System;

namespace Dragoraptor
{
    public interface IVictoryChecker
    {
        bool IsVictory { get; }
        event Action<bool> OnCanVictoryStateChanged;
    }
}
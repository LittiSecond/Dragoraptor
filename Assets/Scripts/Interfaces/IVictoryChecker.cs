using System;

namespace Interfaces
{
    public interface IVictoryChecker
    {
        event Action<bool> OnCanVictoryStateChanged;
    }
}
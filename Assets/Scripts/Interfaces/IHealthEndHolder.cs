using System;

namespace Dragoraptor
{
    public interface IHealthEndHolder
    {
        event Action OnHealthEnd;
    }
}
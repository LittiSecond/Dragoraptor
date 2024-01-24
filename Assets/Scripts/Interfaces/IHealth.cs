using System;


namespace Dragoraptor
{
    public interface IHealth
    {
        event Action<int> OnHealthChanged;
        public int Health { get; }
        public int MaxHealth { get; }
    }
}

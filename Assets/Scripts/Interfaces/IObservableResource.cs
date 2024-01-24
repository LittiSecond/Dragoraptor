using System;


namespace Dragoraptor
{
    public interface IObservableResource
    {
        event Action<int> OnMaxValueChanged;
        event Action<int> OnValueChanged;
        int MaxValue { get; }
        int Value { get; }
    }
}

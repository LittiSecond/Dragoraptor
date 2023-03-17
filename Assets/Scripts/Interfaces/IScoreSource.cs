using System;


namespace Dragoraptor
{
    public interface IScoreSource
    {
        event Action<int> OnScoreChanged;
        int GetScore();
    }
}

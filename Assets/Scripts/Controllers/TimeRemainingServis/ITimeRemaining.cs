using System;


namespace Dragoraptor
{
    public interface ITimeRemaining
    {
        Action Method { get; }
        bool IsRepeating { get; }
        float Duration { get; }
        float TimeCounter { get; set; }
    }
}

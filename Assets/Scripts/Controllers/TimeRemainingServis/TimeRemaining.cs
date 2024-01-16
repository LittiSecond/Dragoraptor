using System;


namespace Dragoraptor
{
    public sealed class TimeRemaining : ITimeRemaining
    {
        #region ITimeRemaining

        public Action Method { get; }
        public bool IsRepeating { get; }
        public float Duration { get; }
        public float TimeCounter { get; set; }

        #endregion

        
        public TimeRemaining(Action method, float duration, bool isRepeating = false)
        {
            Method = method;
            Duration = duration;
            TimeCounter = duration;
            IsRepeating = isRepeating;
        }

    }
}
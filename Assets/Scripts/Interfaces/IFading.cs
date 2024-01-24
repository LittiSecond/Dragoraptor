using System;


namespace Dragoraptor
{
    public interface IFading
    {
        public event Action OnFadingEnd;
        float FadingDuration { set; }
        void StartFading();
    }
}

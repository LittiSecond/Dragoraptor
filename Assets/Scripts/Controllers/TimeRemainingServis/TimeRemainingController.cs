using UnityEngine;
using System.Collections.Generic;


namespace Dragoraptor
{
    public sealed class TimeRemainingController : IExecutable
    {
        #region Fields

        private readonly List<ITimeRemaining> _timeRemainings;

        #endregion


        #region ClassLifeCycles

        public TimeRemainingController()
        {
            _timeRemainings = TimeRemainingExtensions.TimeRemainings;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            float time = Time.deltaTime;
            for (var i = 0; i < _timeRemainings.Count; i++)
            {
                ITimeRemaining obj = _timeRemainings[i];
                obj.TimeCounter -= time;
                if (obj.TimeCounter <= 0.0f)
                {
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemaining();
                    }
                    else
                    {
                        obj.TimeCounter = obj.Duration;
                    }
                    obj?.Method?.Invoke();
                }
            }
        }

        #endregion
    }
}

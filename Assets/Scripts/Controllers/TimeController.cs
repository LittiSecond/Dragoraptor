﻿using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class TimeController
    {
        #region Fields

        private const float TIME_TACT = 1.0f;

        public event Action OnTimeUp;

        private ITimeView _timeView;
        private ITimeRemaining _timer;

        private float _startTime;
        private float _levelDuration;

        private bool _isTiming;

        #endregion


        #region Methods

        public void StartTimer()
        {
            if (_isTiming)
            {
                StopTimer();
            }

            if (_timer == null)
            {
                _timer = new TimeRemaining(OnTimeTact, TIME_TACT, true);
            }
            else if (_timer.Duration != _levelDuration)
            {
                _timer = new TimeRemaining(OnTimeTact, TIME_TACT, true);
            }

            _startTime = Time.time;
            _timer.AddTimeRemaining();
            _isTiming = true;
            _timeView?.SetTime((int)_levelDuration);
        }

        public void StopTimer()
        {
            if (_isTiming)
            {
                _timer.RemoveTimeRemaining();
                _isTiming = false;
            }
        }

        public void SetLevelDuration(float seconds)
        {
            _levelDuration = seconds;
        }

        public void SetTimeView(ITimeView timeView)
        {
            _timeView = timeView;
        }

        private void OnTimeTact()
        {
            float timeLeft = _levelDuration - (Time.time - _startTime);
            _timeView?.SetTime((int)timeLeft);
            if (timeLeft <= 0.0)
            {
                StopTimer();
                OnTimeUp?.Invoke();
            }
        }

        #endregion
    }
}
using System;


namespace Dragoraptor
{
    public class VictoryController : IVictoryChecker
    {

        private PlayerSatiety _playerSatiety;
        private IHealthEndHolder _healthEndHolder;
        private TimeController _timeController;

        private bool _isCharacterAlive;
        private bool _isSatietyConditionMet;
        private bool _isSatietyFull;
        private bool _isTimeUp;


        public VictoryController(PlayerSatiety ps, IHealthEndHolder iheh, TimeController tc)
        {
            _playerSatiety = ps;
            _playerSatiety.OnMaxSatietyReached += SatietyFull;
            _playerSatiety.OnVictorySatietyReached += () => _isSatietyConditionMet = true;
            _healthEndHolder = iheh;
            _healthEndHolder.OnHealthEnd += (() => _isCharacterAlive = false);
            _timeController = tc;
            _timeController.OnTimeUp += () => _isTimeUp = true;
        }
        
        
        #region IVictoryChecker

        public bool IsVictory 
        {
            get
            {
                return _isCharacterAlive && ( 
                    (_isSatietyConditionMet && _isTimeUp) || _isSatietyFull );
            }
        }
        
        public event Action<bool> OnCanVictoryStateChanged;

        #endregion


        public void LevelStart()
        {
            _isCharacterAlive = true;
            _isSatietyFull = false;
            _isSatietyConditionMet = false;
            _isTimeUp = false;
            OnCanVictoryStateChanged?.Invoke(false);
        }

        private void SatietyFull()
        {
            _isSatietyFull = true;
            OnCanVictoryStateChanged?.Invoke(true);
        }

    }
}
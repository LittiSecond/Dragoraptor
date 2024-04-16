using System;
using Interfaces;

namespace Dragoraptor
{
    public class VictoryController : IVictoryChecker
    {

        private PlayerSatiety _playerSatiety;
        private IHealthEndHolder _healthEndHolder;

        private bool _isCharacterAlive;
        private bool _isSatietyFull;


        public VictoryController(PlayerSatiety ps, IHealthEndHolder iheh)
        {
            _playerSatiety = ps;
            _playerSatiety.OnMaxSatietyReached += SatietyFull;
            _healthEndHolder = iheh;
            _healthEndHolder.OnHealthEnd += (() => _isCharacterAlive = false);
        }
        
        
        #region IVictoryChecker

        public bool IsVictory { get; }
        
        public event Action<bool> OnCanVictoryStateChanged;

        #endregion


        public void LevelStart()
        {
            _isCharacterAlive = true;
            _isSatietyFull = false;
            OnCanVictoryStateChanged?.Invoke(false);
        }

        private void SatietyFull()
        {
            _isSatietyFull = true;
        }
        
    }
}
using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class LevelProgressControler
    {

        #region Fields

        private readonly PlayerSatiety _playerSatiety;
        private readonly ScoreController _scoreController;

        private bool _isCharacterAlive;
        private bool _isSatietyConditionMet;
        private bool _isTimeUp;

        #endregion


        #region ClassLifeCycles

        public LevelProgressControler(PlayerHealth ph, PlayerSatiety ps, ScoreController sc)
        {
            ph.OnHealthEnd += OnCharacterKilled;
            _playerSatiety = ps;
            _playerSatiety.OnVictorySatietyReached += OnSatietyConditionMet;
            _scoreController = sc;
        }

        #endregion


        #region Methods

        public void LevelStart()
        {
            _scoreController.ClearScore();
            _isCharacterAlive = true;
            _isSatietyConditionMet = false;
            _isTimeUp = false;

            LevelDescriptor levelDescriptor = Services.Instance.GameProgress.GetCurrentLevel();
            _playerSatiety.SetVictorySatiety(levelDescriptor.SatietyToSucces);
        }

        public void LevelEnd()
        {

        }

        public void OnCharacterKilled()
        {
            _isCharacterAlive = false;
        }

        public bool CheckIsVictory()
        {
            return _isCharacterAlive && _isSatietyConditionMet && (!_isTimeUp);
        }

        private void OnSatietyConditionMet()
        {
            _isSatietyConditionMet = true;
        }

        #endregion
    }
}
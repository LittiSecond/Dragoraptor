using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class LevelProgressController : IHuntResultsSource
    {

        private readonly PlayerSatiety _playerSatiety;
        private readonly ScoreController _scoreController;
        private readonly TimeController _timeController;

        private float _satietyToSuccess;
        private float _victoryScoreMultipler;
        private float _defeatScoreMultipler;
        private float _nullSatietyScoreMultipler;
        private float _satietyConditionScoreMultipler;

        private float _scoreСoefficient;

        private bool _isCharacterAlive;
        private bool _isSatietyConditionMet;
        private bool _isTimeUp;


        public LevelProgressController(GamePlaySettings gamePlaySettings, PlayerHealth ph, PlayerSatiety ps,  
            ScoreController sc, TimeController tc)
        {
            _victoryScoreMultipler = gamePlaySettings.VictoryScoreMultipler;
            _defeatScoreMultipler = gamePlaySettings.DefeatScoreMultipler;
            _nullSatietyScoreMultipler = gamePlaySettings.NullSatietyScoreMultipler;
            _satietyConditionScoreMultipler = gamePlaySettings.SatietySuccefScoreMultipler;

            ph.OnHealthEnd += OnCharacterKilled;
            _playerSatiety = ps;
            _playerSatiety.OnVictorySatietyReached += OnSatietyConditionMet;
            _scoreController = sc;
            _timeController = tc;
            _timeController.OnTimeUp += OnTimeUp;
        }


        public void LevelStart()
        {
            _scoreController.ClearScore();
            _isCharacterAlive = true;
            _isSatietyConditionMet = false;
            _isTimeUp = false;

            LevelDescriptor levelDescriptor = Services.Instance.GameProgress.GetCurrentLevel();
            _satietyToSuccess = levelDescriptor.SatietyToSucces;
            _playerSatiety.SetVictorySatiety(_satietyToSuccess);

            _scoreСoefficient = (_satietyConditionScoreMultipler - _nullSatietyScoreMultipler) / (_satietyToSuccess * _playerSatiety.MaxValue);

            _timeController.SetLevelDuration(levelDescriptor.LevelDuration);
            _timeController.StartTimer();
        }

        public void LevelEnd()
        {
            if (!_isTimeUp)
            {
                _timeController.StopTimer();
            }
        }

        public void OnCharacterKilled()
        {
            _isCharacterAlive = false;
        }

        public bool CheckIsVictory()
        {
            return _isCharacterAlive && _isSatietyConditionMet && _isTimeUp;
        }

        private void OnSatietyConditionMet()
        {
            _isSatietyConditionMet = true;
        }

        private void OnTimeUp()
        {
            _isTimeUp = true;
            Services.Instance.GameStateManager.BreakHunt();
        }

        private float CalculateSatietyScoreMultipler()
        {
            int currentSatiety = _playerSatiety.Value;
            float satietyMultipler = _scoreСoefficient * currentSatiety + _nullSatietyScoreMultipler;
            return satietyMultipler;
        }

        private int CalculateTotalScore()
        {
            int currentScore = _scoreController.GetScore();
            float victoryMultipler = CheckIsVictory() ? _victoryScoreMultipler : _defeatScoreMultipler;
            int totalScore = (int)(currentScore * victoryMultipler * CalculateSatietyScoreMultipler());
            return totalScore;
        }


        #region IHuntResultsSource

        public IHuntResults GetHuntResults()
        {
            HuntResults huntResults = new HuntResults();
            huntResults.IsAlive = _isCharacterAlive && _isTimeUp;
            huntResults.IsSatietyCompleted = _isSatietyConditionMet;
            bool isVictory = CheckIsVictory();
            huntResults.IsSucces = isVictory;
            huntResults.BaseScore = _scoreController.GetScore();
            huntResults.CollectedSatiety = _playerSatiety.Value;
            huntResults.MaxSatiety = _playerSatiety.MaxValue;
            huntResults.SatietyCondition = (int)(_satietyToSuccess * _playerSatiety.MaxValue);
            huntResults.SatietyScoreMultipler = CalculateSatietyScoreMultipler();
            huntResults.TotalScore = CalculateTotalScore();
            huntResults.VictoryScoreMultipler = isVictory ? _victoryScoreMultipler : _defeatScoreMultipler;

            return huntResults;
        }

        #endregion
    }
}
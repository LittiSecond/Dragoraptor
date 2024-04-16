using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class LevelProgressController : IHuntResultsSource
    {

        private readonly PlayerSatiety _playerSatiety;
        private readonly ScoreController _scoreController;
        private readonly TimeController _timeController;
        private readonly IVictoryChecker _victoryController;
        private HuntResults _lastHuntResults;

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
            ScoreController sc, TimeController tc, VictoryController vc)
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
            _lastHuntResults = new HuntResults();
            _victoryController = vc;
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

            //_timeController.SetLevelDuration(levelDescriptor.LevelDuration);
            //_timeController.StartTimer();
        }

        public void LevelEnd()
        {
            if (!_isTimeUp)
            {
                //_timeController.StopTimer();
            }
            //Debug.Log("LevelProgressController->LevelEnd: ");
        }

        public void RegistrateHuntResults()
        {
            Services.Instance.GameProgress.RegistrateHuntResults(GetHuntResults());
        }

        public void OnCharacterKilled()
        {
            _isCharacterAlive = false;
        }

        // public bool CheckIsVictory()
        // {
        //     return _isCharacterAlive && _isSatietyConditionMet && _isTimeUp;
        // }

        private void OnSatietyConditionMet()
        {
            _isSatietyConditionMet = true;
        }

        private void OnTimeUp()
        {
            _isTimeUp = true;
            //Services.Instance.GameStateManager.BreakHunt();
        }

        private float CalculateSatietyScoreMultipler()
        {
            int currentSatiety = _playerSatiety.Value;
            float satietyMultipler = _scoreСoefficient * currentSatiety + _nullSatietyScoreMultipler;
            return satietyMultipler;
        }

        private int CalculateTotalScore(bool isVictory)
        {
            int currentScore = _scoreController.GetScore();
            float victoryMultipler = isVictory ? _victoryScoreMultipler : _defeatScoreMultipler;
            int totalScore = (int)(currentScore * victoryMultipler * CalculateSatietyScoreMultipler());
            return totalScore;
        }


        #region IHuntResultsSource

        public IHuntResults GetHuntResults()
        {
            _lastHuntResults.IsAlive = _isCharacterAlive;// && _isTimeUp;
            _lastHuntResults.IsSatietyCompleted = _isSatietyConditionMet;
            bool isVictory = _victoryController.IsVictory;
            _lastHuntResults.IsSucces = isVictory;
            _lastHuntResults.BaseScore = _scoreController.GetScore();
            _lastHuntResults.CollectedSatiety = _playerSatiety.Value;
            _lastHuntResults.MaxSatiety = _playerSatiety.MaxValue;
            _lastHuntResults.SatietyCondition = (int)(_satietyToSuccess * _playerSatiety.MaxValue);
            _lastHuntResults.SatietyScoreMultipler = CalculateSatietyScoreMultipler();
            _lastHuntResults.TotalScore = CalculateTotalScore(isVictory);
            _lastHuntResults.VictoryScoreMultipler = isVictory ? _victoryScoreMultipler : _defeatScoreMultipler;

            return _lastHuntResults;
        }

        #endregion
    }
}
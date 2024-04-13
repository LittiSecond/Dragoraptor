using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public class UiLevelsMap : BaseScreenBehaviour,  ILevelMapView
    {

        [SerializeField] private UiMissionIndicator[] _missionIndicators;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _huntButton;

        public event Action OnCloseButtonClick;

        private Dictionary<int, UiMissionIndicator> _levelIndicators;
        private Dictionary<int, LevelStatus> _tempLevelStatuses;

        private int _selectedLevel;


        private void Start()
        {
            //Debug.Log("UiLevelsMap->Start:");
            _levelIndicators = new();
            foreach (var indicator in _missionIndicators)
            {
                _levelIndicators.Add(indicator.LevelID, indicator);
                indicator.OnClick += LevelIndicatorClick;
                indicator.IsSelected = false;
                if (_tempLevelStatuses != null)
                {
                    if (_tempLevelStatuses.TryGetValue(indicator.LevelID, out LevelStatus status))
                    {
                        indicator.Status = status;
                    }
                }
            }

            _selectedLevel = -1;
            DeactivateHuntButton();
            
            _huntButton.onClick.AddListener(HuntButtonClick);
            _closeButton.onClick.AddListener((() => OnCloseButtonClick?.Invoke()));
            
        }

        #region ILevelMapView
        
        public event Action<int> OnLevelClick;

        public void SetLevelStatus(int levelNumber, LevelStatus newStatus)
        {
            if (_levelIndicators == null)
            {
                _tempLevelStatuses ??= new();

                _tempLevelStatuses[levelNumber] = newStatus;
            }
            else
            {
                if (_levelIndicators.TryGetValue(levelNumber, out var indicator))
                {
                    indicator.Status = newStatus;

                    if (_selectedLevel > 0 && _selectedLevel == levelNumber)
                    {
                        if (newStatus == LevelStatus.Hidden || newStatus == LevelStatus.NotAvailable)
                        {
                            DeactivateHuntButton();
                        }
                        else
                        {
                            ActivateHuntButton();
                        }
                    }
                }
                else
                {
                    Debug.LogError("UiLevelsMap->SetLevelStatus: wrong levelNumber = " + levelNumber.ToString());
                }
            }
        }

        public void SetLevelSelected(int levelNumber)
        {
            if (_selectedLevel == levelNumber) return;
            
            if (_selectedLevel > 0)
            {
                _levelIndicators[_selectedLevel].IsSelected = false;
                _selectedLevel = -1;
                DeactivateHuntButton();
            }

            if (levelNumber > 0)
            {
                if (_levelIndicators.TryGetValue(levelNumber, out var level))
                {
                    level.IsSelected = true;
                    _selectedLevel = levelNumber;
                    ActivateHuntButton();
                }
                else
                {
                    Debug.LogError("UiLevelsMap->SetLevelSelected: wrong levelNumber = " + levelNumber.ToString());
                }
            }
            
        }
        
        #endregion


        private void LevelIndicatorClick(int levelNumber)
        {
            OnLevelClick?.Invoke(levelNumber);
        }

        private void HuntButtonClick()
        {
            Debug.Log("UiLevelsMap->HuntButtonClick:");
            Services.Instance.GameStateManager.SwitchToHunt();
        }
        
        private void ActivateHuntButton()
        {
            _huntButton.interactable = true;
        }

        private void DeactivateHuntButton()
        {
            _huntButton.interactable = false;
        }
        
    }
}
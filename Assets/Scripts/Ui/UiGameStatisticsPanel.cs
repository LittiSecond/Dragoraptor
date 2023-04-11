using System;
using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor
{
    public sealed class UiGameStatisticsPanel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Dropdown _levelSelectDropdown;
        [SerializeField] private Text _levelsComplited;
        [SerializeField] private Text _totalScore;
        [SerializeField] private Text _totalHunts;
        [SerializeField] private Text _lastHuntScore;
        [SerializeField] private Text _bestScore;

        #endregion


        #region Properties

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _levelSelectDropdown.onValueChanged.AddListener(OnLevelSelectChanged);
        }

        #endregion


        #region Methods

        private void OnLevelSelectChanged(int select)
        {
            Debug.Log("UiGameStatisticsPanel->OnLevelSelectChanged: select = " + select.ToString());
        }

        public void SetProgressData(ProgressData data)
        {
            _levelSelectDropdown.options.Clear();
            int currentLevel = data.CurrentLevelNumber;
            for (int i = 0; i < data.Levels.Count; i++)
            {
                int levelNumber = data.Levels[i].LevelNumber;
                _levelSelectDropdown.options.Add(new Dropdown.OptionData(levelNumber.ToString()));
                if (levelNumber == currentLevel)
                {
                    _levelSelectDropdown.value = i;
                }
            }

        }


        #endregion

    }
}
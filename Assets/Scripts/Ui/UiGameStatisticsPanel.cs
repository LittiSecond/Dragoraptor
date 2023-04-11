using System;
using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
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

        private LevelProgressInfo[] _progresses;

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

            _bestScore.text = _progresses[select].BestScore.ToString();
        }

        public void SetProgressData(ProgressData data)
        {
            _levelSelectDropdown.options.Clear();
            int currentLevel = data.CurrentLevelNumber;
            _progresses = data.Levels.ToArray();
            for (int i = 0; i < data.Levels.Count; i++)
            {
                LevelStatus levelStatus = data.Levels[i].Status;
                if (levelStatus == LevelStatus.Avilable || levelStatus == LevelStatus.Finished)
                {
                    int levelNumber = data.Levels[i].LevelNumber;
                    _levelSelectDropdown.options.Add(new Dropdown.OptionData(levelNumber.ToString()));
                    if (levelNumber == currentLevel)
                    {
                        _levelSelectDropdown.value = i;
                    }
                }
            }
            _levelsComplited.text = data.ComplitedLevels.ToString();
            _totalScore.text = data.TotalScore.ToString();
            _totalHunts.text = data.HuntsTotal.ToString();
            _lastHuntScore.text = data.LastScore.ToString();

            int index = currentLevel - 1;
            if (index < 0)
            {
                index = 0;
            }
            _bestScore.text = data.Levels[index].BestScore.ToString();
        }


        #endregion

    }
}
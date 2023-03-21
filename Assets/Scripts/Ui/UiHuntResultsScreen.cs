using System;
using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class UiHuntResultsScreen : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _aliveCheckBox;
        [SerializeField] private Image _ateCheckBox;
        [SerializeField] private Text _victoryText;
        [SerializeField] private Text _bigText;
        [SerializeField] private Text _resultScoreText;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _restartButton;

        [Space(10), SerializeField] private Sprite _yesSprite;
        [SerializeField] private Sprite  _noSprite;
        [SerializeField] private Color  _yesSpriteColor;
        [SerializeField] private Color   _noSpriteColor;


        private TextGenerator _textGenerator;
        private IHuntResultsSource _huntResultsSource;
        private Action _restartListener;
        private Action _returnListener;


        #endregion


        #region UnityMethods

        private void Awake()
        {
            _textGenerator = new TextGenerator();
            _returnButton.onClick.AddListener(OnReturnButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        #endregion


        #region Methods

        public void Show()
        {
            gameObject.SetActive(true);
            CreateTextHuntResults();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetHuntResultsSource(IHuntResultsSource huntResultsSource)
        {
            _huntResultsSource = huntResultsSource;
        }

        private void CreateTextHuntResults()
        {
            IHuntResults huntResults = _huntResultsSource.GetHuntResults();
            if (huntResults != null)
            {
                CreateCheckBoxValue(_aliveCheckBox, huntResults.IsAlive);
                CreateCheckBoxValue(_ateCheckBox, huntResults.IsSatietyCompleted);
                _victoryText.text = _textGenerator.CreateVictoryText(huntResults.IsSucces);
                _bigText.text = _textGenerator.CreateBigDescription(huntResults);


            }

        }

        private void CreateCheckBoxValue(Image checkBox, bool valueType)
        {
            if (valueType)
            {
                checkBox.sprite = _yesSprite;
                checkBox.color  = _yesSpriteColor;
            }
            else
            {
                checkBox.sprite = _noSprite;
                checkBox.color  = _noSpriteColor;
            }
        }

        public void AddListeners(Action returnListener, Action restartListener)
        {
            _returnListener = returnListener;
            _restartListener = restartListener;
        }

        private void OnReturnButtonClick()
        {
            _returnListener();
        }

        private void OnRestartButtonClick()
        {
            _restartListener();
        }

        #endregion
    }
}
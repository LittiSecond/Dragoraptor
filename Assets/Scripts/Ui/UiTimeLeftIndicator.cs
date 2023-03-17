using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public class UiTimeLeftIndicator : MonoBehaviour, ITimeView
    {
        #region Fields

        private const string SEPARATOR = ":";
        private const string LETTER_NULL = "0";
        private const int SECONDS_IN_MINUTE = 60;
        private const int TWO_NUMERAL_MIN_NUMBER = 10;

        [SerializeField] private Text _text;

        #endregion


        #region ITimeView

        public void SetTime(int timeSeconds)
        {
            int minuts;
            int seconds;

            minuts = timeSeconds / SECONDS_IN_MINUTE;
            seconds = timeSeconds % SECONDS_IN_MINUTE;

            string text = minuts.ToString() + SEPARATOR;

            if (seconds < TWO_NUMERAL_MIN_NUMBER)
            {
                text += LETTER_NULL;
            }
            text += seconds.ToString();
            _text.text = text;
        }

        #endregion
    }
}
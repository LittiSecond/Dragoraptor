using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public class UiTimeLeftIndicator : MonoBehaviour
    {
        #region Fields

        private const string SEPARATOR = ":";
        private const int SECONDS_IN_MINUTE = 60;

        [SerializeField] private Text _text;

        #endregion


        #region Methods

        public void SetValue(float timeSeconds)
        {
            int minuts;
            int seconds;

            int totalSeconds = (int)timeSeconds;

            minuts = totalSeconds / SECONDS_IN_MINUTE;
            seconds = totalSeconds % SECONDS_IN_MINUTE;

            _text.text = minuts.ToString() + SEPARATOR + seconds.ToString();
        }

        #endregion
    }
}
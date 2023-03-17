using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class UiScoreIndicator : MonoBehaviour, IScoreView
    {
        #region Fields

        [SerializeField] private Text _text;

        #endregion


        #region IScoreView

        public void SetScore(int score)
        {
            _text.text = score.ToString();
        }

        #endregion
    }
}
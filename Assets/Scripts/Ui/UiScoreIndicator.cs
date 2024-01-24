using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class UiScoreIndicator : MonoBehaviour, IScoreView
    {

        [SerializeField] private Text _text;


        #region IScoreView

        public void SetScore(int score)
        {
            _text.text = score.ToString();
        }

        #endregion
    }
}
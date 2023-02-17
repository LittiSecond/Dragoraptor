using UnityEngine;


namespace Dragoraptor.Ui
{
    public abstract class BaseScreenBehaviour : MonoBehaviour, IScreenBehaviour
    {
        #region Fields

        private bool _isActive = true;

        #endregion


        #region IScreenBehaviour

        public void Show()
        {
            if (!_isActive)
            {
                _isActive = true;
                gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            if (_isActive)
            {
                _isActive = false;
                gameObject.SetActive(false);
            }
        }

        #endregion
    }
}
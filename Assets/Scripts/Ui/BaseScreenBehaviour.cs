using UnityEngine;


namespace Dragoraptor.Ui
{
    public abstract class BaseScreenBehaviour : MonoBehaviour
    {

        #region Fields

        private bool _isActive = true;

        #endregion



        #region Methods

        public virtual void Show()
        {
            if (!_isActive)
            {
                _isActive = true;
                gameObject.SetActive(true);
            }
        }

        public virtual void Hide()
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
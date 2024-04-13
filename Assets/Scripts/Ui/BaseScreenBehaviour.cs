using UnityEngine;


namespace Dragoraptor.Ui
{
    public abstract class BaseScreenBehaviour : MonoBehaviour, IScreenBehaviour
    {

        protected bool _isActive = true;


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

    }
}
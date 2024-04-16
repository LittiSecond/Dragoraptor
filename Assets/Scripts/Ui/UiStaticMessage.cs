using UnityEngine;

namespace Dragoraptor.Ui
{
    public class UiStaticMessage : MonoBehaviour, IMessage
    {

        #region IMessage

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        #endregion
    }
}
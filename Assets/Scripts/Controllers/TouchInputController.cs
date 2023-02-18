using UnityEngine;
using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class TouchInputController : IExecutable
    {
        #region Fields

        private PlayerMovement _playerMovement;

        private bool _isEnabled;

        #endregion


        #region Methods

        public void On()
        {
            _isEnabled = true;
        }

        public void Off()
        {
            _isEnabled = false;
        }

        public void SetPlayerMovement(PlayerMovement pm) => _playerMovement = pm;

        private void WorkTouch(Touch touch )
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 position = Services.Instance.SceneGeometry.ConvertScreenPositionToWorld(touch.position);
                ObjctType type = AreaChecker.CheckPoint(position);
                if (type == ObjctType.Ground)
                {
                    _playerMovement.SetDestination(position.x);
                }
                Debug.Log("TouchInputController->WorkTouch: type = " + type.ToString());
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.touches[0];

                    if (!UiChecker.CheckIsUiElement(touch.position))
                    {
                        WorkTouch(touch);
                    }
                }
            }
        }

        #endregion

    }
}

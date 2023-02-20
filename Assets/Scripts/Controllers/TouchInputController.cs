using UnityEngine;
using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class TouchInputController : IExecutable
    {
        #region Fields

        private WalkController _playerWalk;
        private JumpController _playerJump;

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

        public void SetPlayerWalk(WalkController pw) => _playerWalk = pw;
        public void SetPlayerJump(JumpController pj) => _playerJump = pj;

        private void WorkTouch(Touch touch )
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 position = Services.Instance.SceneGeometry.ConvertScreenPositionToWorld(touch.position);
                ObjctType type = AreaChecker.CheckPoint(position);
                if (type == ObjctType.Ground)
                {
                    _playerWalk.SetDestination(position.x);
                }
                else if (type == ObjctType.Player)
                {
                    _playerJump.TouchBegin(position);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 position = Services.Instance.SceneGeometry.ConvertScreenPositionToWorld(touch.position);
                _playerJump.TouchEnd(position);
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

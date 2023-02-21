using UnityEngine;
using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class TouchInputController : IExecutable
    {
        #region Fields


        private readonly WalkController _walkController;
        private readonly JumpController _jumpController;
        private readonly JumpPainter _jumpPainter;

        private CharacterState _state;

        private bool _isEnabled;

        #endregion


        #region ClassLifeCycles

        public TouchInputController(CharacterStateHolder csh, WalkController wc, JumpController jk, JumpPainter jp )
        {
            csh.OnStateChanged += OnStateChanged;
            _walkController = wc;
            _jumpController = jk;
            _jumpPainter = jp;
        }

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

        private void WorkTouch(Touch touch )
        {
            if (_state == CharacterState.Idle || _state == CharacterState.Walk)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 position = Services.Instance.SceneGeometry.ConvertScreenPositionToWorld(touch.position);
                    ObjctType type = AreaChecker.CheckPoint(position);
                    if (type == ObjctType.Ground)
                    {
                        _walkController.SetDestination(position.x);
                    }
                    else if (type == ObjctType.Player)
                    {
                        _jumpController.TouchBegin();
                        _jumpPainter.SetTouchPosition(position);
                    }
                }

            }
            else if (_state == CharacterState.PrepareJump)
            {
                Vector2 position = Services.Instance.SceneGeometry.ConvertScreenPositionToWorld(touch.position);
                if (touch.phase == TouchPhase.Ended)
                {
                    _jumpController.TouchEnd(position);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    _jumpPainter.SetTouchPosition(position);
                }
            }
        }

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
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

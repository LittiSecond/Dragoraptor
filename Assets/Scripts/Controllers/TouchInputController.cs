using UnityEngine;
using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class TouchInputController : IExecutable
    {

        private readonly WalkController _walkController;
        private readonly JumpController _jumpController;
        private readonly JumpPainter _jumpPainter;
        private readonly PlayerHorizontalDirection _horizontalDirection;
        private readonly AttackController _attackController;

        private CharacterState _state;

        private bool _isEnabled;


        public TouchInputController(CharacterStateHolder csh, WalkController wc, JumpController jk, JumpPainter jp,
            PlayerHorizontalDirection hd, AttackController ac )
        {
            csh.OnStateChanged += OnStateChanged;
            _walkController = wc;
            _jumpController = jk;
            _jumpPainter = jp;
            _horizontalDirection = hd;
            _attackController = ac;
        }


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
                        _horizontalDirection.SetDestination(position);
                    }
                    else if (type == ObjctType.Player)
                    {
                        _jumpController.TouchBegin();
                        _jumpPainter.SetTouchPosition(position);
                    }
                    else
                    {
                        _attackController.TouchBegin();
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
                    _horizontalDirection.SetTouchPosition(position);
                }
            }
            else if (_state == CharacterState.FliesUp || _state == CharacterState.FliesDown)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _attackController.TouchBegin();
                }
            }
        }

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
        }


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

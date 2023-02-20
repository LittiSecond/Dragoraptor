using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpPainter : IExecutable
    {
        #region Fields

        private LineRenderer _trajectoryRenderer;
        private PowerLinePainter _powerLinePainter;

        private CharacterState _state;

        private bool _isEnabled;
        private bool _haveBody;

        #endregion


        #region ClassLifeCycles

        public JumpPainter(CharacterStateHolder csh)
        {
            csh.OnStateChanged += OnStateChanged;
            _powerLinePainter = new PowerLinePainter();
        }

        #endregion


        #region Methods

        public void SetBody(PlayerBody playerBody)
        {
            if (playerBody)
            {
                (LineRenderer, LineRenderer) lr = playerBody.GetLineRenderers();
                _trajectoryRenderer = lr.Item1;
                _trajectoryRenderer.enabled = false;
                _powerLinePainter.SetData(playerBody.transform , lr.Item2);
                _haveBody = true;
            }
            else
            {
                _trajectoryRenderer = null;
                _powerLinePainter.ClearData();
                _haveBody = false;
            }
        }

        public void SetTouchPosition(Vector2 position)
        {
           _powerLinePainter.SetTouchPosition(position);
        }

        private void OnStateChanged(CharacterState newState)
        {
            if (newState != _state)
            {
                if (newState == CharacterState.PrepareJump)
                {
                    DrawingOn();
                }
                else if (_state == CharacterState.PrepareJump)
                {
                    DrawingOff();
                }

                _state = newState;
            }

        }

        private void DrawingOn()
        {
            if (_haveBody)
            {
                _powerLinePainter.DrawingOn();
                _isEnabled = true;
            }
        }

        private void DrawingOff()
        {
            if (_haveBody)
            {
                _powerLinePainter.DrawingOff();
            }
            _isEnabled = false;
        }
        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled )
            {
                _powerLinePainter.Execute();
            }
        }

        #endregion

    }
}

using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpPainter : IExecutable
    {
        #region Fields

        private PowerLinePainter _powerLinePainter;
        private TrajectoryPainter _trajectoryPainter;

        private CharacterState _state;

        private bool _isEnabled;
        private bool _haveBody;

        #endregion


        #region ClassLifeCycles

        public JumpPainter(CharacterStateHolder csh, GamePlaySettings gamePlaySettings, JumpCalculator jc)
        {
            csh.OnStateChanged += OnStateChanged;
            _powerLinePainter = new PowerLinePainter(gamePlaySettings);
            _trajectoryPainter = new TrajectoryPainter(jc);
        }

        #endregion


        #region Methods

        public void SetBody(PlayerBody playerBody)
        {
            if (playerBody)
            {
                (LineRenderer, LineRenderer) lr = playerBody.GetLineRenderers();
                _powerLinePainter.SetData(playerBody.transform , lr.Item2);
                _trajectoryPainter.SetData(playerBody.transform, lr.Item1);
                _haveBody = true;
            }
            else
            {
                _powerLinePainter.ClearData();
                _trajectoryPainter.ClearData();
                _haveBody = false;
            }
        }

        public void SetTouchPosition(Vector2 position)
        {
            _powerLinePainter.SetTouchPosition(position);
            _trajectoryPainter.SetTouchPosition(position);
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
                _trajectoryPainter.DrawingOn();
                _isEnabled = true;
            }
        }

        private void DrawingOff()
        {
            if (_haveBody)
            {
                _powerLinePainter.DrawingOff();
                _trajectoryPainter.DrawingOff();
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
                _trajectoryPainter.Execute();
            }
        }

        #endregion

    }
}

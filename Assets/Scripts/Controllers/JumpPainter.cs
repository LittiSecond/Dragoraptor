using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpPainter : IExecutable
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _transform;
        private LineRenderer _trajectoryRenderer;
        private LineRenderer _powerRenderer;

        private bool _isEnabled;
        private bool _haveBody;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        #endregion


        #region Methods

        public void SetBody(PlayerBody pb)
        {
            if (pb)
            {
                _playerBody = pb;
                _transform = _playerBody.transform;
                (LineRenderer, LineRenderer) lr = _playerBody.GetLineRenderers();
                _trajectoryRenderer = lr.Item1;
                _powerRenderer = lr.Item2;
                _haveBody = true;
            }
            else
            {
                _playerBody = null;
                _transform = null;
                _trajectoryRenderer = null;
                _powerRenderer = null;
                _haveBody = false;
            }
        }


        public void On()
        {
            if (_haveBody)
            {
                _isEnabled = true;
            }
        }

        public void Off()
        {
            _isEnabled = false;
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled )
            {

            }
        }

        #endregion

    }
}

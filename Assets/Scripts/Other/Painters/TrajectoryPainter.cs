using UnityEngine;


namespace Dragoraptor
{
    sealed class TrajectoryPainter : BaseLinePainter
    {

        #region Fields

        private const float TIME_STEP = 0.1f;

        private const int TRAECTORY_CAPACITY = 32;

        private readonly JumpCalculator _jumpCalculator;
        
        private Vector2 _gravity;

        private Vector2[] _traectory;
        private float _xMin;
        private float _xMax;

        private int _count;

        #endregion


        #region ClassLifeCycles

        public TrajectoryPainter(JumpCalculator jc)
        {
            _jumpCalculator = jc;
            _traectory = new Vector2[TRAECTORY_CAPACITY];

            _gravity = Physics2D.gravity;

            Rect visibleArea = Services.Instance.SceneGeometry.GetVisibleArea();
            _xMin = visibleArea.xMin;
            _xMax = visibleArea.xMax;
        }

        #endregion


        #region Methods

        public override void SetData(Transform bodyTransform, LineRenderer lineRenderer)
        {
            base.SetData(bodyTransform, lineRenderer);
            _lineRenderer.SetPosition(0, Vector2.zero);
        }

        public override void Execute()
        {

            Vector2 jumpDirection = _bodyPosition - _touchPosition;
            Vector2 impulse = _jumpCalculator.CalculateJampImpulse(jumpDirection);

            if (impulse == Vector2.zero)
            {
                _lineRenderer.enabled = false;
            }
            else
            {
                _lineRenderer.enabled = true;

                CalculateTraectory(impulse);
                SetPositionsToLineRenderer();
            }
        }

        private void CalculateTraectory(Vector2 velocity)
        {
            float bodyY = _bodyTransform.position.y;
            _count = 1;

            for (int i = 1; i < TRAECTORY_CAPACITY; i++)
            {
                float time = i * TIME_STEP;

                Vector2 current;
                current.x = velocity.x * time;
                current.y = velocity.y * time + _gravity.y * time * time / 2;

                _traectory[i] = current;
                _count++;

                if ( current.x <= _xMin || current.x > _xMax || current.y <= bodyY )
                {
                    break;
                }
            }
        }

        private void SetPositionsToLineRenderer()
        {
            if (_lineRenderer.positionCount != _count)
            {
                _lineRenderer.positionCount = _count;
            }

            for (int i = 1; i < _count; i++)
            {
                _lineRenderer.SetPosition(i, _traectory[i]);
            }
        }


        #endregion
    }
}

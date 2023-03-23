using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcDirectionByScale : INpcDirection
    {
        #region Fields

        private Transform _transform;

        private float _startXScale;
        private bool _isDirectionLeft;

        #endregion


        #region Properties

        public bool IsDirectionToLeft
        {
            get
            {
                return _isDirectionLeft;
            }
            set
            {
                if (_isDirectionLeft != value)
                {
                    Vector3 scale = _transform.localScale;

                    if (value)
                    {
                        scale.x = _startXScale * (-1.0f);
                    }
                    else
                    {
                        scale.x = _startXScale;
                    }
                    _transform.localScale = scale;
                    _isDirectionLeft = value;
                }
            }
        }

        #endregion


        #region ClassLifeCycles

        public NpcDirectionByScale(Transform transform)
        {
            _transform = transform;
            _startXScale = _transform.localScale.x;
            _isDirectionLeft = _startXScale < 0.0f;
        }

        #endregion
    }
}
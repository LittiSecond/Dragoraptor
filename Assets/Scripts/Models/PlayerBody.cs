using UnityEngine;
using System; 


namespace Dragoraptor
{
    public sealed class PlayerBody : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Rigidbody2D _rigedbody;
        [SerializeField] private LineRenderer _trajectoryRenderer;
        [SerializeField] private LineRenderer _powerRenderer;
        [SerializeField] private Animator _bodyAnimator;
        [SerializeField] private SpriteRenderer _bodySpriteRenderer;

        public event Action OnGroundContact;

        private Direction _direction;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (OnGroundContact != null)
            {
                if (collision.gameObject.layer == (int)SceneLayer.Ground)
                {
                    OnGroundContact();
                }
            }
        }

        private void OnEnable()
        {
            _direction = (_bodySpriteRenderer.flipX)? Direction.Rigth : Direction.Left;
        }

        #endregion


        #region Methods

        public Rigidbody2D GetRigidbody()
        {
            return _rigedbody;
        }

        public (LineRenderer, LineRenderer) GetLineRenderers()
        {
            return (_trajectoryRenderer, _powerRenderer);
        }

        public Animator GetBodyAnimator()
        {
            return _bodyAnimator;
        }

        public void SetDirection(Direction direction)
        {
            if (_direction != direction)
            {
                _direction = direction;
                _bodySpriteRenderer.flipX = _direction == Direction.Rigth;
            }
        }

        #endregion

    }
}
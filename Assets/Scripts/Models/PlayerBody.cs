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

        private bool _isDirectionRight;

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
            _isDirectionRight = _bodySpriteRenderer.flipX;
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

        public void SetDirectionIsRight(bool isRight)
        {
            if (_isDirectionRight != isRight)
            {
                _isDirectionRight = isRight;
                _bodySpriteRenderer.flipX = _isDirectionRight;
            }
        }

        #endregion

    }
}
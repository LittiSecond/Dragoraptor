using UnityEngine;


namespace Dragoraptor
{
    public sealed class Bird1Logick : NpcBaseLogick
    {
        #region Fields

        [SerializeField] private Animator _animator;
        [SerializeField] private Fading _fading;
        [SerializeField] private float _destroyDelay = 5.1f;

        private Bird1Movement _movement;
        private NpcBaseDirection _direction;
        private Bird1Fall _fall;
        private Bird1Animation _animation;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _direction = new NpcBaseDirection(_mainSprite);
            _movement = new Bird1Movement(transform, _rigidbody, _direction);
            AddExecutable(_movement);
            AddInitializable(_movement);
            AddCleanable(_movement);
            _fall = new Bird1Fall(_collider, _rigidbody);
            AddCleanable(_fall);
            _animation = new Bird1Animation(_animator);
            AddExecutable(_fading);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == (int)SceneLayer.Ground)
            {
                _fall.OnGroundContact();
                _animation.SetGrounded();
                DestroyItselfDelay(_destroyDelay);
                _collider.enabled = false;
                _fading.StartFading();
            }
        }

        #endregion


        #region Methods

        public override void SetAdditionalData(NpcData additionalData)
        {
            NpcDataWay data = additionalData as NpcDataWay;
            if (data != null)
            {
                _movement.SetWay(data.Way);
            }
        }

        protected override void OnHealthEnd()
        {
            _movement.StopMovementLogick();
            _fall.StartFall();
            _animation.SetFall();
        }

        public override void Initialize()
        {
            base.Initialize();
            _animation.SetFlying();
            _collider.enabled = true;
        }

        #endregion

    }
}

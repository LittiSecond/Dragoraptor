using UnityEngine;


namespace Dragoraptor
{
    public sealed class Bird1Logick : NpcBaseLogick
    {
        #region Fields

        private Bird1Movement _movement;
        private NpcBaseDirection _direction;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _direction = new NpcBaseDirection(_mainSprite);
            _movement = new Bird1Movement(transform, _rigidbody, _direction);
            AddExecutable(_movement);
            AddInitializable(_movement);
        }

        #endregion
    }
}

namespace Dragoraptor
{
    public sealed class ShipType3Logick : NpcBaseLogick
    {
        #region Fields

        private ShipType3Movement _movement;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _movement = new ShipType3Movement(transform, _rigidbody);
            AddExecutable(_movement);
        }

        #endregion

    }
}

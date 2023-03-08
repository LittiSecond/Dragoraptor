using UnityEngine;


namespace Dragoraptor
{
    public sealed class ShipType3Logick : NpcBaseLogick
    {
        #region Fields

        [SerializeField] private Transform _bulletStartPoint;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _damag;

        private ShipType3Movement _movement;
        private ShipType3Attack _attack;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _movement = new ShipType3Movement(transform, _rigidbody);
            AddExecutable(_movement);
            AddCleanable(_movement);

            _attack = new ShipType3Attack(_bulletStartPoint, _reloadTime, _damag);
            AddExecutable(_attack);
            AddInitializable(_attack);

        }

        #endregion

    }
}

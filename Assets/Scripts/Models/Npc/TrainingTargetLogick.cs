using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class TrainingTargetLogick : NpcBaseLogick
    {

        private const int REGUIRED_DATA_QUANTITY = 2;

        [SerializeField] private float _speed;

        private NpcMoveToPosition _moveToPosition;
        private Vector2 _destination;


        protected override void Awake()
        {
            base.Awake();
            _moveToPosition = new NpcMoveToPosition(_rigidbody, transform, _speed);
            AddCleanable(_moveToPosition);
        }


        public override void SetAdditionalDataArray(float[] datas)
        {
            base.SetAdditionalDataArray(datas);
            if (datas != null)
            {
                if (datas.Length >= REGUIRED_DATA_QUANTITY)
                {
                    _destination.x = datas[0];
                    _destination.y = datas[1];
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            _moveToPosition.MoveToPosition(_destination);
        }

    }
}

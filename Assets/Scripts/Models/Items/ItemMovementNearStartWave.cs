using UnityEngine;

namespace Dragoraptor
{
    public class ItemMovementNearStartWave : ItemMovementBase
    {

        private Vector3 _startPosition;
        private float _xMax;
        private float _yMax;
        private float _xDuration;
        private float _yDuration;
        private float _xPhase;
        private float _yPhase;
        private float _xTimeCounter;
        private float _yTimeCounter;

        private bool _isDataReady;
        
        
        public ItemMovementNearStartWave(Transform itemRoot) : base(itemRoot)
        {
            
        }

        public override void SetData(float[] data)
        {
            if (data != null)
            {
                _xMax = data[0];
                _yMax = data[1];
                _xDuration = data[2];
                _yDuration = data[3];
                _xPhase = data[4];
                _yPhase = data[5];
                
                _isDataReady = (_xMax >= 0.0f || _yMax >= 0.0f || _xDuration > 0.0f || _yDuration > 0.0f);
            }
        }

        public override void Start()
        {
            base.Start();
            _startPosition = _itemRoot.position;
            _xTimeCounter = _xDuration * _xPhase * Mathf.PI;
            _yTimeCounter = _yDuration * _yPhase * Mathf.PI;
        }

        // public override void Stop()
        // {
        //     base.Stop();
        //     if (_isDataReady)
        //     {
        //         
        //         
        //         
        //     }
        // }

        #region IExecutable

        public override void Execute()
        {
            if (_isDataReady)
            {
                float deltaTime = Time.deltaTime;
                
                _xTimeCounter += deltaTime;
                if (_xTimeCounter > _xDuration)
                {
                    _xTimeCounter -= _xDuration;
                }
                float xPhasa = _xTimeCounter / _xDuration * Mathf.PI * 2 ;
                float dx = Mathf.Cos(xPhasa) * _xMax;
                
                _yTimeCounter += deltaTime;
                if (_yTimeCounter > _yDuration)
                {
                    _yTimeCounter -= _yDuration;
                }
                float yPhasa = _yTimeCounter / _yDuration * Mathf.PI * 2 ;
                float dy = Mathf.Sin(yPhasa) * _yMax;

                Vector3 newPosition = new Vector3()
                {
                    x = _startPosition.x + dx,
                    y = _startPosition.y + dy,
                    z = _startPosition.z
                };

                _itemRoot.position = newPosition;
            }
        }

        #endregion
        
    }
}
using UnityEngine;


namespace Dragoraptor
{
    public sealed class CharacterIntermediary
    {
        #region Fields

        private Transform _playerCharacterTransform;

        private bool _havePlayerCharacterTransform;

        #endregion


        #region Methods

        public void SetPlayerCharacterTransform(Transform transform)
        {
            _playerCharacterTransform = transform;
            _havePlayerCharacterTransform = _playerCharacterTransform != null;
        }

        public Vector3? GetPlayerPosition()
        {
            Vector3? position = null;
            if (_havePlayerCharacterTransform)
            {
                position = _playerCharacterTransform.position; 
            }

            return position;
        }

        #endregion
    }
}

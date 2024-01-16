using UnityEngine;


namespace Dragoraptor
{
    public sealed class CharacterIntermediary
    {

        private Transform _playerCharacterTransform;
        private PickUpController _pickUpController;
        private ScoreController _scoreController;

        private bool _havePlayerCharacterTransform;


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

        public bool PickUp(PickableResource[] content)
        {
            return _pickUpController.PickUp(content);
        }

        public void SetControllers(PickUpController puc, ScoreController sc)
        {
            _pickUpController = puc;
            _scoreController = sc;
        }

        public void AddScore(int amount)
        {
            _scoreController.AddScore(amount);
        }

    }
}

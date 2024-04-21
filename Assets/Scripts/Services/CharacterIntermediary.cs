using UnityEngine;


namespace Dragoraptor
{
    public sealed class CharacterIntermediary
    {

        private Transform _playerCharacterTransform;
        private PickUpController _pickUpController;
        private ScoreController _scoreController;
        private ICharOnGroundChecker _charOnGroundChecker;

        private bool _havePlayerCharacterTransform;

        public bool IsCharOnGround => _charOnGroundChecker.IsCharacterOnGround;
        
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

        public void SetControllers(PickUpController puc, ScoreController sc, ICharOnGroundChecker cogc)
        {
            _pickUpController = puc;
            _scoreController = sc;
            _charOnGroundChecker = cogc;
        }

        public void AddScore(int amount)
        {
            _scoreController.AddScore(amount);
        }

    }
}

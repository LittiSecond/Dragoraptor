using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {
        #region Fields

        private PlayerMovement _playerMovement;
        private TouchInputController _touchInputController;
        private PlayerBody _playerBody;

        private bool _haveCharacterBody;

        #endregion


        #region ClassLifeCycles

        public PlayerCharacterController(PlayerMovement pm, TouchInputController tic )
        {
            _playerMovement = pm;
            _touchInputController = tic;
        }

        #endregion


        #region Methods

        public void CreateCharacter()
        {
            if (!_haveCharacterBody)
            {
                _playerBody = GameObject.FindObjectOfType<PlayerBody>();
                _playerMovement.SetBody(_playerBody);

                _haveCharacterBody = true;
            }
        }

        public void CharacterControllOn()
        {
            _touchInputController.On();
        }

        public void CharacterControllOff()
        {
            _touchInputController.Off();
        }

        #endregion

    }
}

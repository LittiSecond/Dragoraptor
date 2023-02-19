using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {
        #region Fields

        private PlayerWalk _playerWalk;
        private PlayerJump _playerJump;
        private TouchInputController _touchInputController;
        private PlayerBody _playerBody;

        private bool _haveCharacterBody;

        #endregion


        #region ClassLifeCycles

        public PlayerCharacterController(PlayerWalk pm, PlayerJump pj, TouchInputController tic )
        {
            _playerWalk = pm;
            _playerJump = pj;
            _touchInputController = tic;
        }

        #endregion


        #region Methods

        public void CreateCharacter()
        {
            if (!_haveCharacterBody)
            {
                _playerBody = GameObject.FindObjectOfType<PlayerBody>();
                _playerWalk.SetBody(_playerBody);
                _playerJump.SetBody(_playerBody);

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

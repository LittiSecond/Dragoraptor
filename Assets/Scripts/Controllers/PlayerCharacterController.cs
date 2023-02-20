using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {
        #region Fields

        private WalkController _walkController;
        private JumpController _jumpController;
        private JumpPainter _jumpPainter;
        private TouchInputController _touchInputController;
        private PlayerBody _playerBody;

        private bool _haveCharacterBody;

        #endregion


        #region ClassLifeCycles

        public PlayerCharacterController(WalkController wc, JumpController jc, JumpPainter jp, TouchInputController tic )
        {
            _walkController = wc;
            _jumpController = jc;
            _jumpPainter = jp;
            _touchInputController = tic;
        }

        #endregion


        #region Methods

        public void CreateCharacter()
        {
            if (!_haveCharacterBody)
            {
                _playerBody = GameObject.FindObjectOfType<PlayerBody>();
                _walkController.SetBody(_playerBody);
                _jumpController.SetBody(_playerBody);
                _jumpPainter.SetBody(_playerBody);

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

using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {
        #region Fields

        private readonly CharacterStateHolder _stateHolder;
        private readonly TouchInputController _touchInputController;
        private readonly WalkController _walkController;
        private readonly JumpController _jumpController;
        private readonly JumpPainter _jumpPainter;
        private readonly FlightObserver _flightObserver;
        private PlayerBody _playerBody;

        private bool _haveCharacterBody;

        #endregion


        #region ClassLifeCycles

        public PlayerCharacterController(CharacterStateHolder csh, TouchInputController tic, WalkController wc, JumpController jc, JumpPainter jp, FlightObserver fo)
        {
            _stateHolder = csh;
            _touchInputController = tic;
            _walkController = wc;
            _jumpController = jc;
            _jumpPainter = jp;
            _flightObserver = fo;
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
                _flightObserver.SetBody(_playerBody);

                _haveCharacterBody = true;
            }
        }

        public void CharacterControllOn()
        {
            _touchInputController.On();
            _stateHolder.SetState(CharacterState.Idle);
        }

        public void CharacterControllOff()
        {
            _touchInputController.Off();
        }

        #endregion

    }
}

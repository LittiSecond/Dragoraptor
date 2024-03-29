﻿
namespace Dragoraptor
{
    public sealed class Controllers
    {
        #region Fields

        private readonly IExecutable[] _executeControllers;

        #endregion


        #region Properties
        public int Length => _executeControllers.Length;
        public IExecutable this[int index] => _executeControllers[index];

        #endregion


        #region ClassLifeCycles

        public Controllers(GamePlaySettings gamePlaySettings)
        {
            CharacterStateHolder characterStateHolder = new CharacterStateHolder();
            JumpCalculator jumpCalculator = new JumpCalculator(gamePlaySettings);
            WalkController walkController = new WalkController(characterStateHolder, gamePlaySettings);
            JumpPainter jumpPainter = new JumpPainter(characterStateHolder, gamePlaySettings, jumpCalculator);
            JumpController jumpController = new JumpController(characterStateHolder, jumpCalculator);
            FlightObserver flightObserver = new FlightObserver(characterStateHolder);
            AnimationController animationController = new AnimationController(characterStateHolder);
            HorizontalDirection horizontalDirection = new HorizontalDirection();

            TouchInputController touchInputController = new TouchInputController(characterStateHolder, 
                walkController, jumpController, jumpPainter, horizontalDirection);

            IBodyUser[] bodyUsers = new IBodyUser[]
            {
                walkController,
                jumpController,
                jumpPainter,
                flightObserver,
                animationController, 
                horizontalDirection
            };

            PlayerCharacterController playerCharacterController = new PlayerCharacterController(characterStateHolder, 
                gamePlaySettings, touchInputController, bodyUsers);

            _executeControllers = new IExecutable[]
            {
                touchInputController,
                walkController,
                jumpPainter,
                flightObserver
            };

            SceneController sceneController = new SceneController();

            Services.Instance.GameStateManager.SetControllers(playerCharacterController, sceneController);

        }

        #endregion

    }
}

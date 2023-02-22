
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
            JumpPainter jumpPainter = new JumpPainter(characterStateHolder, gamePlaySettings);
            JumpController jumpController = new JumpController(characterStateHolder, gamePlaySettings, jumpCalculator);
            FlightObserver flightObserver = new FlightObserver(characterStateHolder);

            TouchInputController touchInputController = new TouchInputController(characterStateHolder, 
                walkController, jumpController, jumpPainter);
            PlayerCharacterController playerCharacterController = new PlayerCharacterController(characterStateHolder, 
                touchInputController, walkController, jumpController, jumpPainter, flightObserver);

            _executeControllers = new IExecutable[]
            {
                touchInputController,
                walkController,
                jumpPainter,
                flightObserver
            };

            Services.Instance.GameStateManager.SetCharacterController(playerCharacterController);

        }

        #endregion

    }
}

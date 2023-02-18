
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

        public Controllers()
        {
            TouchInputController touchInputController = new TouchInputController();
            PlayerMovement playerMovement = new PlayerMovement();
            PlayerCharacterController playerCharacterController = new PlayerCharacterController(playerMovement, touchInputController);



            _executeControllers = new IExecutable[]
            {
                touchInputController,
                playerMovement
            };


            touchInputController.SetPlayerMovement(playerMovement);

            Services.Instance.GameStateManager.SetCharacterController(playerCharacterController);

        }

        #endregion

    }
}


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
            PlayerWalk playerWalk = new PlayerWalk();
            PlayerJump playerJump = new PlayerJump(playerWalk);
            PlayerCharacterController playerCharacterController = new PlayerCharacterController(playerWalk, playerJump, touchInputController);



            _executeControllers = new IExecutable[]
            {
                touchInputController,
                playerWalk
            };


            touchInputController.SetPlayerWalk(playerWalk);
            touchInputController.SetPlayerJump(playerJump);

            Services.Instance.GameStateManager.SetCharacterController(playerCharacterController);

        }

        #endregion

    }
}

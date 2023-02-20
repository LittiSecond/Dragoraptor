
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
            WalkController walkController = new WalkController();
            JumpPainter jumpPainter = new JumpPainter();
            JumpController jumpController = new JumpController(walkController, jumpPainter);

            PlayerCharacterController playerCharacterController = new PlayerCharacterController(walkController, 
                jumpController, jumpPainter, touchInputController);



            _executeControllers = new IExecutable[]
            {
                touchInputController,
                walkController,
                jumpPainter
            };


            touchInputController.SetPlayerWalk(walkController);
            touchInputController.SetPlayerJump(jumpController);

            Services.Instance.GameStateManager.SetCharacterController(playerCharacterController);

        }

        #endregion

    }
}

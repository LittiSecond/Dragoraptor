
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



            _executeControllers = new IExecutable[]
            {
                touchInputController
            };


            touchInputController.SetPlayerMovement(playerMovement);

            Services.Instance.GameStateManager.SetTouchInputController(touchInputController);

        }

        #endregion

    }
}

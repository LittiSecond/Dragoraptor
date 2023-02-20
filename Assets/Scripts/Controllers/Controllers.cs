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

        public Controllers()
        {
            CharacterStateHolder characterStateHolder = new CharacterStateHolder();
            WalkController walkController = new WalkController(characterStateHolder);
            JumpPainter jumpPainter = new JumpPainter();
            JumpController jumpController = new JumpController(characterStateHolder);

            TouchInputController touchInputController = new TouchInputController(characterStateHolder, 
                walkController, jumpController, jumpPainter);
            PlayerCharacterController playerCharacterController = new PlayerCharacterController(characterStateHolder, 
                walkController, jumpController, jumpPainter, touchInputController);



            _executeControllers = new IExecutable[]
            {
                touchInputController,
                walkController,
                jumpPainter
            };

            Services.Instance.GameStateManager.SetCharacterController(playerCharacterController);

        }

        #endregion

    }
}

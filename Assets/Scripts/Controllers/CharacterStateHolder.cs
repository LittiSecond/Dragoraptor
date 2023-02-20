using System;


namespace Dragoraptor
{
    public sealed class CharacterStateHolder
    {
        #region Fields

        public event Action<CharacterState> OnStateChanged;

        private CharacterState _state;

        #endregion


        #region Properties

        public CharacterState State { get => _state; }

        #endregion


        #region Methods

        public void SetState(CharacterState newState)
        {
            if (newState != _state)
            {
                _state = newState;
                OnStateChanged(_state);
            }
        }

        #endregion

    }
}

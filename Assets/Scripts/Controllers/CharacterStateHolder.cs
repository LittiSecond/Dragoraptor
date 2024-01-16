using System;


namespace Dragoraptor
{
    public sealed class CharacterStateHolder
    {

        public event Action<CharacterState> OnStateChanged;

        private CharacterState _state;


        public CharacterState State { get => _state; }


        public void SetState(CharacterState newState)
        {
            if (newState != _state)
            {
                _state = newState;
                OnStateChanged(_state);
            }
        }

    }
}

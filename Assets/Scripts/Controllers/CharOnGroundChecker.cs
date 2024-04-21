namespace Dragoraptor
{
    public class CharOnGroundChecker : ICharOnGroundChecker
    {
        
        private CharacterState _state;
        
        // ICharOnGroundChecker
        public bool IsCharacterOnGround
        {
            get
            {
                return (_state == CharacterState.Idle ||
                        _state == CharacterState.Walk ||
                        _state == CharacterState.PrepareJump ||
                        _state == CharacterState.Death);
            }
        }

        public CharOnGroundChecker(CharacterStateHolder csh)
        {
            csh.OnStateChanged += OnStateChanged;
        }
        
        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
        }
        
    }
}
using UnityEngine;


namespace Dragoraptor
{
    public sealed class AnimationController : IBodyUser
    {

        private Animator _bodyAnimator;

        private readonly int _stateParameter = Animator.StringToHash("CharacterState");

        private bool _haveAnimator;


        public AnimationController(CharacterStateHolder csh)
        {
            csh.OnStateChanged += OnStateChanged;
        }


        private void OnStateChanged(CharacterState newState)
        {
            if (_haveAnimator)
            {
                _bodyAnimator.SetInteger(_stateParameter, (int)newState);
            }
        }


        #region IBodyUser

        public void SetBody(PlayerBody pb)
        {
            _bodyAnimator = pb.GetBodyAnimator();
            _haveAnimator = true;
        }

        public void ClearBody()
        {
            _bodyAnimator = null;
            _haveAnimator = false;
        }

        #endregion
    }
}

using UnityEngine;


namespace Dragoraptor
{
    public sealed class AnimationController
    {
        #region Fields

        [SerializeField] private Animator _bodyAnimator;

        private readonly int _stateParametr = Animator.StringToHash("CharacterState");

        #endregion


        #region ClassLifeCycles

        public AnimationController(CharacterStateHolder csh)
        {
            csh.OnStateChanged += OnStateChanged;
        }


        #endregion


        #region Methods

        private void OnStateChanged(CharacterState newState)
        {
            _bodyAnimator.SetInteger(_stateParametr, (int)newState);
        }

        #endregion

    }
}

using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class Bird1Animation
    {
        #region Fields

        private const string ANIMATION_PROPERTI_NAME = "State";
        private const int ANIMATION_STATE_FLYING = 0;
        private const int ANIMATION_STATE_FALL = 1;
        private const int ANIMATION_STATE_GROUNDED = 2;


        private readonly Animator _animator;
        private readonly int _state;

        #endregion


        #region ClassLifeCycles

        public Bird1Animation(Animator animator)
        {
            _animator = animator;
            _state = Animator.StringToHash(ANIMATION_PROPERTI_NAME);
        }

        #endregion


        #region Methods

        public void SetFlying()
        {
            _animator.SetInteger(_state, ANIMATION_STATE_FLYING);
        }

        public void SetFall()
        {
            _animator.SetInteger(_state, ANIMATION_STATE_FALL);
        }

        public void SetGrounded()
        {
            _animator.SetInteger(_state, ANIMATION_STATE_GROUNDED);
        }

        #endregion
    }
}

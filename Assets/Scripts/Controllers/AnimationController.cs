﻿using UnityEngine;


namespace Dragoraptor
{
    public sealed class AnimationController : IBodyUser
    {
        #region Fields

        [SerializeField] private Animator _bodyAnimator;

        private readonly int _stateParametr = Animator.StringToHash("CharacterState");

        private bool _haveAnimator;

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
            if (_haveAnimator)
            {
                _bodyAnimator.SetInteger(_stateParametr, (int)newState);
            }
        }

        #endregion


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

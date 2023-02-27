using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {
        #region Fields

        private const string CHARACTER_PREFAB_ID = "PlayerCharacter";

        private readonly CharacterStateHolder _stateHolder;
        private readonly TouchInputController _touchInputController;
        private GameObject _playerGO;
        private PlayerBody _playerBody;

        private IBodyUser[] _bodyUsers;

        private bool _haveCharacterBody;

        #endregion


        #region ClassLifeCycles

        public PlayerCharacterController(CharacterStateHolder csh, TouchInputController tic, IBodyUser[] bu)
        {
            _stateHolder = csh;
            _touchInputController = tic;
            _bodyUsers = bu;
        }

        #endregion


        #region Methods

        public void CreateCharacter()
        {
            if (!_haveCharacterBody)
            {
                InstantiateCharacter();

                for (int i = 0; i < _bodyUsers.Length; i++)
                {
                    _bodyUsers[i].SetBody(_playerBody);
                }
                _haveCharacterBody = true;
            }
        }

        public void CharacterControllOn()
        {
            _touchInputController.On();
            _stateHolder.SetState(CharacterState.Idle);
        }

        public void CharacterControllOff()
        {
            _touchInputController.Off();
        }

        private void InstantiateCharacter()
        {
            GameObject prefab = PrefabLoader.GetPrefab(CHARACTER_PREFAB_ID);
            _playerGO = GameObject.Instantiate(prefab);
            _playerBody = _playerGO.GetComponent<PlayerBody>();
        }


        #endregion

    }
}

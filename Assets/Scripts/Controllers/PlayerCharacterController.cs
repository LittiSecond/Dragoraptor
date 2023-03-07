using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {
        #region Fields

        private const string CHARACTER_PREFAB_ID = "PlayerCharacter";

        private readonly CharacterStateHolder _stateHolder;
        private readonly TouchInputController _touchInputController;
        private readonly PlayerHealth _playerHealth;
        private GameObject _playerGO;
        private PlayerBody _playerBody;

        private IBodyUser[] _bodyUsers;

        private Vector2 _spawnPosition;

        private bool _haveCharacterBody;
        private bool _isCharacterControllEnabled;
        private bool _isUiConnected;

        #endregion


        #region ClassLifeCycles

        public PlayerCharacterController( CharacterStateHolder csh, GamePlaySettings gps, TouchInputController tic, 
            PlayerHealth ph, IBodyUser[] bu)
        {
            _stateHolder = csh;
            _spawnPosition = gps.CharacterSpawnPosition;
            _touchInputController = tic;
            _playerHealth = ph;
            _playerHealth.OnHealthEnd += OnHealthEnd;
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

            _playerGO.transform.position = _spawnPosition;
            _playerGO.SetActive(true);
            _playerHealth.ResetHealth();
            _stateHolder.SetState(CharacterState.Idle);
            Services.Instance.CharacterIntermediary.SetPlayerCharacterTransform(_playerGO.transform);
        }

        public void CharacterControllOn()
        {
            _touchInputController.On();
            _isCharacterControllEnabled = true;
        }

        public void CharacterControllOff()
        {
            _touchInputController.Off();
            _isCharacterControllEnabled = false;
        }

        public void RemoveCharacter()
        {
            if (_isCharacterControllEnabled)
            {
                CharacterControllOff();
            }
            Services.Instance.CharacterIntermediary.SetPlayerCharacterTransform(null);
            _playerGO.SetActive(false);
        }

        public void OnHealthEnd()
        {
            Services.Instance.GameStateManager.CharacterKilled();
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

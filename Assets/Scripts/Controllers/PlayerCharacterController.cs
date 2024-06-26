﻿using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerCharacterController
    {

        private const string CHARACTER_PREFAB_ID = "PlayerCharacter";

        private readonly CharacterStateHolder _stateHolder;
        private readonly TouchInputController _touchInputController;
        private readonly PlayerHealth _playerHealth;
        private readonly PlayerSatiety _playerSatiety;
        private readonly EnergyController _energyController;
        private GameObject _playerGO;
        private PlayerBody _playerBody;

        private IBodyUser[] _bodyUsers;
        private ITimeRemaining _timer;

        private Vector2 _spawnPosition;
        private float _characterDeathDelay;


        private bool _haveCharacterBody;
        private bool _isCharacterControllEnabled;
        private bool _isTiming;


        public PlayerCharacterController( CharacterStateHolder csh, GamePlaySettings gps, TouchInputController tic, 
            PlayerHealth ph, PlayerSatiety ps, EnergyController ec, IBodyUser[] bu)
        {
            _stateHolder = csh;
            _spawnPosition = gps.CharacterSpawnPosition;
            _characterDeathDelay = gps.CharacterDeathDelay;
            _touchInputController = tic;
            _playerHealth = ph;
            _playerHealth.OnHealthEnd += OnHealthEnd;
            _playerSatiety = ps;
            _energyController = ec;
            _bodyUsers = bu;
        }


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
            _playerSatiety.ResetSatiety();
            _energyController.ResetEnergy();
            _energyController.On();
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
            _energyController.Off();
            Services.Instance.CharacterIntermediary.SetPlayerCharacterTransform(null);
            _playerGO.SetActive(false);

            if (_isTiming)
            {
                _timer.RemoveTimeRemaining();
                _isTiming = false;
            }
        }

        public void OnHealthEnd()
        {
            CharacterControllOff();
            _stateHolder.SetState(CharacterState.Death);
            if (!_isTiming)
            {
                _timer = new TimeRemaining(OnDeathTimer, _characterDeathDelay);
                _timer.AddTimeRemaining();
                _isTiming = true;
            }
            Services.Instance.CharacterIntermediary.SetPlayerCharacterTransform(null);
        }

        private void InstantiateCharacter()
        {
            GameObject prefab = PrefabLoader.GetPrefab(CHARACTER_PREFAB_ID);
            _playerGO = GameObject.Instantiate(prefab);
            _playerBody = _playerGO.GetComponent<PlayerBody>();
        }

        private void OnDeathTimer()
        {
            _isTiming = false;
            Services.Instance.GameStateManager.CharacterKilled();
        }

    }
}

using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class EnergyController : IExecutable, IObservableResource, IResouceStore
    {
        #region Fields

        private const float UPDATE_OBSERVERS_INTERVAL = 0.5f;

        private IMessage _noEnergyMessage;

        private float _maxEnergy;
        private float _energy;
        private float _idleRegeneration;
        private float _walkRegeneration;
        private float _regenerationDelay;
        private float _updateObserversTimer;
        private float _changeRegenerationTimer;

        private CharacterState _state;
        private CharacterState _regenerationMode;

        private bool _isEnabled;
        private bool _isRegeneration;
        private bool _isChangeTimer;

        #endregion


        #region IObservableResource

        public event Action<int> OnMaxValueChanged;

        public event Action<int> OnValueChanged;

        public int MaxValue { get =>  (int)_maxEnergy; }

        public int Value { get => (int)_energy; }

        #endregion


        #region ClassLifeCycles

        public EnergyController(GamePlaySettings gps, CharacterStateHolder characterState)
        {
            _maxEnergy = gps.Energy;
            _idleRegeneration = gps.EnergyRegeneration;
            _walkRegeneration = gps.WalkEnergyRegeneration;
            _regenerationDelay = gps.RegenerationDelay;
            characterState.OnStateChanged += OnStateChanged;
        }

        #endregion


        #region Methods

        public void On()
        {
            _isEnabled = true;
        }

        public void Off()
        {
            _isEnabled = false;
        }

        public void ResetEnergy()
        {
            _energy = _maxEnergy;
            _isRegeneration = false;
            _isChangeTimer = false;
            OnValueChanged?.Invoke((int)_energy);
            _updateObserversTimer = 0.0f;
        }

        private void OnStateChanged(CharacterState newState)
        {
            if (newState != _state)
            {
                if (newState == CharacterState.Idle || newState == CharacterState.PrepareJump)
                {
                    if (_regenerationMode == CharacterState.None || _regenerationMode == CharacterState.Walk)
                    {
                        _changeRegenerationTimer = 0.0f;
                        _isChangeTimer = true;
                    }
                }
                else if (newState == CharacterState.Walk)
                {
                    if (_regenerationMode == CharacterState.Idle)
                    {
                        _regenerationMode = CharacterState.Walk;
                        _isChangeTimer = false;
                    }
                    else if (_regenerationMode == CharacterState.None)
                    {
                        _changeRegenerationTimer = 0.0f;
                        _isChangeTimer = true;
                    }
                }
                else
                {
                    _regenerationMode = CharacterState.None;
                    _isChangeTimer = false;
                }

                _state = newState;
            }
        }

        private void RegenerationLogick(float deltaTime)
        {
            bool shouldRegen = false;
            float regen = 0.0f;
            if (_regenerationMode == CharacterState.Idle)
            {
                regen = _idleRegeneration;
                shouldRegen = true;
            }
            else if (_regenerationMode == CharacterState.Walk)
            {
                regen = _walkRegeneration;
                shouldRegen = true;
            }

            if (shouldRegen)
            {
                _energy += regen * deltaTime;
                if (_energy >= _maxEnergy)
                {
                    _energy = _maxEnergy;
                    _isRegeneration = false;
                }
            }
        }

        private void ChangeRegenerationLogick(float deltaTime)
        {

            _changeRegenerationTimer += deltaTime;
            if (_changeRegenerationTimer >= _regenerationDelay)
            {
                _changeRegenerationTimer = 0.0f;
                _isChangeTimer = false;
                if (_state == CharacterState.Idle || _state == CharacterState.PrepareJump)
                {
                    _regenerationMode = CharacterState.Idle;
                    _isRegeneration = true;
                }
                else if (_state == CharacterState.Walk)
                {
                    _regenerationMode = CharacterState.Walk;
                    _isRegeneration = true;
                }
                else
                {
                    _regenerationMode = CharacterState.None;
                    _isRegeneration = false;
                }
            }
            
        }

        private void UpdateObserversLogick(float deltaTime)
        {
            if (_updateObserversTimer < UPDATE_OBSERVERS_INTERVAL)
            {
                _updateObserversTimer += deltaTime;
            }
            else
            {
                OnValueChanged?.Invoke((int)_energy);
                _updateObserversTimer = 0.0f;
            }
        }

        public void SetNoEnergyMessage(IMessage message)
        {
            _noEnergyMessage = message;
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled)
            {
                float deltaTime = Time.deltaTime;
                if (_isRegeneration)
                {
                    RegenerationLogick(deltaTime);
                }
                
                UpdateObserversLogick(deltaTime);

                if (_isChangeTimer)
                {
                    ChangeRegenerationLogick(deltaTime);
                }
            }
        }

        #endregion


        #region IResouceStore

        public bool SpendResource(int amount)
        {
            bool isSpended = false;
            if (_isEnabled)
            {
                float amountF = amount;
                if (amountF <= _energy)
                {
                    _energy -= amountF;
                    OnValueChanged?.Invoke((int)_energy);
                    _updateObserversTimer = 0.0f;
                    isSpended = true;
                    _isRegeneration = true;
                }
                else
                {
                    _noEnergyMessage?.ShowMessage();
                }
            }
            return isSpended;
        }

        #endregion


    }
}
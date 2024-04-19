using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class AttackController: IBodyUser 
    {

        private const string HIT_VISUAL_EFFECT = "EffectBoom";
        private const string ATTACK_VISUAL_EFFECT = "ClawScratch";

        private readonly IResouceStore _energyStore;
        private Transform _bodyTransform;
        private AttackAreasPack _attackAreas;
        private ITimeRemaining _attackDelayTimer;
        private CharacterState _state;
        private Direction _direction;

        private float _energyCost;
        private float _attackInterval;

        private int _attackPower;
        private int _layerMaskToAttack = (1 << (int)SceneLayer.Npc);

        private bool _haveBody;
        private bool _isTiming;
        private bool _shouldAttack;


        public AttackController(CharacterStateHolder csh, GamePlaySettings gps, PlayerHorizontalDirection phd, IResouceStore energyStore)
        {
            csh.OnStateChanged += OnStateChanged;
            _attackAreas = gps.AttackAreas;
            _energyCost = gps.AttackEnergyCost;
            _attackInterval = gps.AttackInterval;
            _attackPower = gps.AttackPower;
            phd.OnDirectionChanged += OnDirectionChanged;
            _energyStore = energyStore;
            _attackDelayTimer = new TimeRemaining(OnAttackTimer, _attackInterval, false);
        }


        public void TouchBegin()
        {
            if (_haveBody)
            {
                if (CheckStateCanAttack())
                {
                    if (!_isTiming)
                    {
                        DoAttack();
                    }
                    else
                    {
                        _shouldAttack = true;
                    }
                }
            }
        }

        private bool CheckStateCanAttack()
        {
            return (_state == CharacterState.Idle || _state == CharacterState.Walk ||
                    _state == CharacterState.FliesUp || _state == CharacterState.FliesDown);
        }

        private void DoAttack()
        {
            _shouldAttack = false;
            if (_energyStore.SpendResource((int)_energyCost))
            {
                Rect damagedArea = CalculateDamagedArea();
                
                CreateVisualAttackEffect(damagedArea);
                
                Collider2D[] targets = Physics2D.OverlapAreaAll(damagedArea.min, damagedArea.max, _layerMaskToAttack);

                if (targets.Length > 0)
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        MakeDamage(targets[i]);
                    }
                    //CreateVisualHitEffect(damagedArea);
                }
                _attackDelayTimer.AddTimeRemaining();
                _isTiming = true;
            }
        }

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
        }

        private void OnDirectionChanged(Direction newDirection)
        {
            _direction = newDirection;
        }

        private Rect CalculateDamagedArea()
        {
            Rect rect;
            if (_direction == Direction.Rigth)
            {
                switch (_state)
                {
                    case CharacterState.FliesUp:
                        rect = _attackAreas.RightFliesUp;
                        break;
                    case CharacterState.FliesDown:
                        rect = _attackAreas.RightFliesDown;
                        break;
                    case CharacterState.Walk:
                        rect = _attackAreas.RightWalk;
                        break;
                    default:
                        rect = _attackAreas.RightIdle;
                        break;
                }
            }
            else
            {
                switch (_state)
                {
                    case CharacterState.FliesUp:
                        rect = _attackAreas.LeftFliesUp;
                        break;
                    case CharacterState.FliesDown:
                        rect = _attackAreas.LeftFliesDown;
                        break;
                    case CharacterState.Walk:
                        rect = _attackAreas.LeftWalk;
                        break;
                    default:
                        rect = _attackAreas.LeftIdle;
                        break;
                }
            }

            Vector2 position = _bodyTransform.position;
            rect.max += position;
            rect.min += position;
            return rect;
        }

        private void MakeDamage(Collider2D targetCollider)
        {
            ITakeDamage target = targetCollider.GetComponent<ITakeDamage>();
            if (target != null)
            {
                target.TakeDamage(_attackPower);
            }
        }

        private void CreateVisualHitEffect(Rect area)
        {
            CreateEffect(area, HIT_VISUAL_EFFECT);
        }

        private void CreateVisualAttackEffect(Rect area)
        {
            CreateEffect(area, ATTACK_VISUAL_EFFECT);
        }

        private void CreateEffect(Rect area, string prefabID)
        {
            PooledObject effect = Services.Instance.ObjectPool.GetObjectOfType(prefabID);
            if (effect)
            {
                effect.transform.position = (Vector3)area.center;
                IInitializable initializable = effect as IInitializable;
                initializable?.Initialize();
            }
        }

        private void OnAttackTimer()
        {
            _isTiming = false;
            if (_shouldAttack)
            {
                if (CheckStateCanAttack())
                {
                    DoAttack();
                }
                else
                {
                    _shouldAttack = false;
                }
            }
        }


        #region IBodyUser

        public void SetBody(PlayerBody pb)
        {
            _bodyTransform = pb.transform;
            _haveBody = true;
        }

        public void ClearBody()
        {
            _bodyTransform = null;
            _haveBody = false;
            _shouldAttack = false;
            if (_isTiming)
            {
                _attackDelayTimer.RemoveTimeRemaining();
                _isTiming = false;
            }
        }

        #endregion

    }
}

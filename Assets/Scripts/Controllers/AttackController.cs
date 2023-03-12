using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class AttackController: IBodyUser 
    {
        #region Fields

        private const string HIT_VISUAL_EFFECT = "EffectBoom";

        private Transform _bodyTransform;
        private AttackAreasPack _attackAreas;
        private CharacterState _state;
        private Direction _direction;

        private int _attackPower;
        private int _layerMaskToAttack = (1 << (int)SceneLayer.Npc);

        private bool _haveBody;

        #endregion


        #region ClassLifeCycles

        public AttackController(CharacterStateHolder csh, GamePlaySettings gps, PlayerHorizontalDirection phd)
        {
            csh.OnStateChanged += OnStateChanged;
            _attackAreas = gps.AttackAreas;
            _attackPower = gps.AttackPower;
            phd.OnDirectionChanged += OnDirectionChanged;
        }

        #endregion


        #region Methods

        public void TouchBegin()
        {
            if (_haveBody)
            {
                if (_state == CharacterState.Idle || _state == CharacterState.Walk ||
                    _state == CharacterState.FliesUp || _state == CharacterState.FliesDown)
                {
                    Rect damagedArea = CalculateDamagedArea();
                    Collider2D[] targets = Physics2D.OverlapAreaAll(damagedArea.min, damagedArea.max, _layerMaskToAttack);

                    if (targets.Length > 0)
                    {
                        for (int i = 0; i < targets.Length; i++)
                        {
                            MakeDamag(targets[i]);
                        }
                        CreateVisualHitEffect(damagedArea);
                    }

                }
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

        private void MakeDamag(Collider2D targetCollider)
        {
            ITakeDamag target = targetCollider.GetComponent<ITakeDamag>();
            if (target != null)
            {
                target.TakeDamage(_attackPower);
            }
        }

        private void CreateVisualHitEffect(Rect area)
        {
            PooledObject effect = Services.Instance.ObjectPool.GetObjectOfType(HIT_VISUAL_EFFECT);
            if (effect)
            {
                effect.transform.position = (Vector3)area.center;
                IInitializable initializable = effect as IInitializable;
                initializable?.Initialize();
            }
        }

        #endregion


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
        }

        #endregion

    }
}

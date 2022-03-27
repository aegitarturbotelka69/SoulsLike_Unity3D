using UnityEngine;

using SLGame.Gameplay;

namespace SLGame.Enemy
{
    public class EnemyControllingAttackState : EnemyControllingBaseState
    {
        [Header("References: ")]

        /// <summary>
        /// Containts reference to enemy ForwardFov script
        /// </summary>
        [SerializeField] private ForwardFOV _fov;

        [Header("Stats: ")]
        [SerializeField] private float _rotationSpeed = 20f;

        public EnemyControllingAttackState(Animator enemyAnimator, EnemyAI ai, ForwardFOV fov)
            : base(enemyAnimator, ai)
        {
            this._fov = fov;
        }
        public override void Execute()
        {
            Quaternion rotationTarget = Quaternion.LookRotation(_fov.PlayerTransform.position - _enemyAI.transform.position);
            _enemyAI.transform.rotation = Quaternion.RotateTowards(_enemyAI.transform.rotation, rotationTarget, _enemyAI.LightAttackRotationSpeed * Time.deltaTime);
            base.Execute();
        }
        public override void StartTransition()
        {
            _animator.SetBool(States.Attack.ToString(), true);
            _enemyAI.StaminaRemain -= _enemyAI.LightAttackStaminaConsumption;
        }

        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool(States.Attack.ToString(), false);


            if (endingManually)
            {
                if (_fov.TargetInterested && _fov.CanSeePlayer)
                {
                    _enemyAI.CurrentState = _enemyAI.EnemyControllingStates[States.Chasing];
                    _enemyAI.Animator.SetBool(States.Chasing.ToString(), true);
                    return;
                }
                else
                {
                    _enemyAI.ChangeControllingState(States.Patrolling);
                    return;
                }
            }
        }
    }
}
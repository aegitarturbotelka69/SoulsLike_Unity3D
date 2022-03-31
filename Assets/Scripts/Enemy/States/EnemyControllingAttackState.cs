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
            _enemyAI.SetLightAttackOnCooldown();

            if (!endingManually)
                return;

            if ((_enemyAI.StaminaRemain / _enemyAI.Stamina) * 100 < 40)
            {
                Debug.LogWarning("Executing dodge back jump");
                _enemyAI.Animator.SetBool(States.DodgeBackJump.ToString(), true);
                _enemyAI.CurrentState = _enemyAI.EnemyControllingStates[States.DodgeBackJump];
                return;
            }

            if (_fov.TargetInterested || _fov.CanSeePlayer)
            {
                _enemyAI.Animator.SetBool(States.Chasing.ToString(), true);
                _enemyAI.CurrentState = _enemyAI.EnemyControllingStates[States.Chasing];
                return;
            }
            else
            {
                _enemyAI.Animator.SetBool(States.Move.ToString(), true);
                _enemyAI.CurrentState = _enemyAI.EnemyControllingStates[States.Patrolling];
                return;
            }
        }
    }
}
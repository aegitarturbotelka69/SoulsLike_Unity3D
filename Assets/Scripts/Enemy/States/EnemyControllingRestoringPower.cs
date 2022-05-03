using UnityEngine;

using SLGame.Gameplay;

namespace SLGame.Enemy
{
    public class EnemyControllingRestoringPowerState : EnemyControllingBaseState
    {
        [Header("References: ")]

        /// <summary>
        /// Containts reference to enemy ForwardFov script
        /// </summary>
        [SerializeField] private ForwardFOV _fov;

        public EnemyControllingRestoringPowerState(Animator enemyAnimator, EnemyAI ai, ForwardFOV fov)
            : base(enemyAnimator, ai)
        {
            this._fov = fov;
        }

        private void WaitUntilStaminaRestored()
        {
            Quaternion rotationTarget = Quaternion.LookRotation(_fov.PlayerTransform.position - _enemyAI.transform.position);
            _enemyAI.transform.rotation = Quaternion.RotateTowards(_enemyAI.transform.rotation, rotationTarget, 400 * Time.deltaTime);

            if (_enemyAI.StaminaRemain > _enemyAI.LightAttackStaminaConsumption)
            {
                _enemyAI.ChangeControllingState(States.Chasing);
            }
        }
        public override void Execute()
        {
            WaitUntilStaminaRestored();
            base.Execute();
        }
        public override void StartTransition()
        {
            _animator.SetBool(States.RestoringPower.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool(States.RestoringPower.ToString(), false);
        }
    }
}
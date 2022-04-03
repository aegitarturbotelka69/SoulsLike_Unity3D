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

        [Header("Stats:")]
        [SerializeField] private float _leftTimeWalk = 0.5f;
        [SerializeField] private float _rightTimeWalk = 0.5f;

        [Header("In game")]
        [SerializeField] private float _leftTimeWalkRemain = 0.5f;
        [SerializeField] private float _rightTimeWalkRemain = 0.5f;

        public EnemyControllingRestoringPowerState(Animator enemyAnimator, EnemyAI ai)
            : base(enemyAnimator, ai) { }

        private void WaitUntilStaminaRestored()
        {
            if ((_enemyAI.StaminaRemain / _enemyAI.Stamina) * 100 > 60)
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
using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyControllingIdleState : EnemyControllingBaseState
    {
        [Header("References:")]
        [SerializeField] private ForwardFOV _fov;

        [Header("Stats:")]
        /// <summary>
        /// Seconds of staying in starting position of patrolling
        /// </summary>
        [SerializeField] private float _idlingTime = 20f;

        [Header("In game:")]
        [SerializeField] private float _idleTimeRemain = 0f;

        public EnemyControllingIdleState(Animator enemyAnimator, EnemyAI ai, ForwardFOV fov)
            : base(enemyAnimator, ai)
        {
            this._fov = fov;
        }

        private void AwaitPatrolState()
        {
            if (_idleTimeRemain > 0f)
            {
                _idleTimeRemain -= Time.deltaTime;
            }
            else
            {
                _idleTimeRemain = _idlingTime;
                _enemyAI.ChangeControllingState(States.Patrolling);
            }
        }

        private void Observe()
        {
            if (_fov.CanSeePlayer || _fov.TargetInterested)
            {
                _enemyAI.ChangeControllingState(States.Chasing);
            }
        }

        public override void Execute()
        {
            Observe();
            AwaitPatrolState();
            base.Execute();
        }
        public override void StartTransition()
        {
            _animator.SetBool(States.Idle.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool(States.Idle.ToString(), false);
        }
    }
}
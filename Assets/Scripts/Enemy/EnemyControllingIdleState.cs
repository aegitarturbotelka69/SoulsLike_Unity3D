using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyControllingIdleState : EnemyControllingBaseState
    {
        [SerializeField] private float timeToStartPatrolling = 5f;

        public EnemyControllingIdleState(Animator enemyAnimator, EnemyAI ai)
            : base(enemyAnimator, ai) { }

        private void AwaitPatrolState()
        {
            if (timeToStartPatrolling > 0f)
            {
                timeToStartPatrolling -= Time.deltaTime;
            }
            else
            {
                _enemyAI.ChangeControllingState(States.Patrolling);
            }
        }
        public override void Execute()
        {
            base.Execute();
            AwaitPatrolState();
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
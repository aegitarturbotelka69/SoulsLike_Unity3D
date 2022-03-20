using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyControllingIdleState : EnemyControllingBaseState
    {
        [Header("Stats:")]
        [SerializeField] private float _idlingTime = 5f;

        [Header("In game:")]
        [SerializeField] private float _idleTimeRemain = 5f;

        public EnemyControllingIdleState(Animator enemyAnimator, EnemyAI ai)
            : base(enemyAnimator, ai) { }

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
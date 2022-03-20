using System.Collections.Generic;
using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyControllingPatrollingState : EnemyControllingBaseState
    {

        [Header("In game:")]
        [SerializeField] private uint _currentPatrollingPoint;
        public EnemyControllingPatrollingState(Animator enemyAnimator, EnemyAI ai)
        : base(enemyAnimator, ai)
        {
            _currentPatrollingPoint = 0;
        }

        private void PatrolToPoint()
        {
            _enemyAI.NavMeshAgent.SetDestination(_enemyAI.PatrolPoints[(int)_currentPatrollingPoint]);

            if (Vector3.Distance(_enemyAI.gameObject.transform.position, _enemyAI.PatrolPoints[(int)_currentPatrollingPoint]) < _enemyAI.PatrolOffset)
            {
                if (_enemyAI.PatrolPoints.Count - 1 == _currentPatrollingPoint)
                {
                    _currentPatrollingPoint = 0;
                    _enemyAI.ChangeControllingState(States.Idle);
                    return;
                }
                else
                {
                    _currentPatrollingPoint++;
                }
            }
        }
        public override void Execute()
        {
            PatrolToPoint();

            base.Execute();
        }
        public override void StartTransition()
        {
            _animator.SetBool("Move", true);
        }
        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool("Move", false);
        }
    }
}
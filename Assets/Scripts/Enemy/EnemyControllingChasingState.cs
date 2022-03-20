using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyControllingChasingState : EnemyControllingBaseState
    {
        [Header("References:")]
        [SerializeField] private ForwardFOV _forwardFOV;

        public EnemyControllingChasingState(Animator animator, EnemyAI ai, ForwardFOV forwardFOV)
            : base(animator, ai)
        {
            _forwardFOV = forwardFOV;
        }

        private void ChasePlayer()
        {

            if (_forwardFOV.TargetInterested)
                _enemyAI.NavMeshAgent.SetDestination(_forwardFOV.PlayerTransform.position);
            else
                _enemyAI.ChangeControllingState(States.Patrolling);
        }


        public override void Execute()
        {
            ChasePlayer();

            base.Execute();
        }
        public override void StartTransition()
        {
            _animator.SetBool(States.Chasing.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool(States.Chasing.ToString(), false);
        }
    }
}
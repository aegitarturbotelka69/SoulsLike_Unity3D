using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyControllingChasingState : EnemyControllingBaseState
    {
        [Header("References:")]
        [SerializeField] private ForwardFOV _forwardFOV;
        [SerializeField] private float _attackOffset = 2.5f;

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

        private void CheckForAttackAvaliablity()
        {
            if (Vector3.Distance(_enemyAI.gameObject.transform.position, _forwardFOV.PlayerTransform.position) < _attackOffset
                && _enemyAI.StaminaRemain > _enemyAI.LightAttackStaminaConsumption
                && !_enemyAI.LightAttackOnCooldown)
            {
                _enemyAI.ChangeControllingState(States.Attack);
            }
        }


        public override void Execute()
        {
            ChasePlayer();
            CheckForAttackAvaliablity();

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
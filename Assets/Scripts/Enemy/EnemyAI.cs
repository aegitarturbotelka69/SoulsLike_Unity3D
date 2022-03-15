using System.Collections;
using System.Collections.Generic;
using SLGame.Gameplay;
using UnityEngine;
using UnityEngine.AI;

namespace SLGame.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] public NavMeshAgent NavMeshAgent;
        [SerializeField] public Animator Animator;
        [Header("Stats:")]
        [SerializeField] public Dictionary<States, EnemyControllingBaseState> EnemyControllingStates = new Dictionary<States, EnemyControllingBaseState>();
        [SerializeField] public List<Vector3> PatrolPoints;
        [SerializeField] public float PatrolOffset;

        [Header("In game:")]
        [SerializeField] public EnemyControllingBaseState _currentState;
        private void Start()
        {
            this.Animator = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();

            EnemyControllingStates.Add(States.Idle, new EnemyControllingIdleState(Animator, this));
            EnemyControllingStates.Add(States.Patrolling, new EnemyControllingPatrollingState(Animator, this));

            _currentState = EnemyControllingStates[States.Idle];
        }

        private void Update()
        {
            _currentState.Execute();
        }

        public void ChangeControllingState(States newState, bool endingManually = false)
        {
            _currentState.EndTransition(endingManually);
            _currentState = EnemyControllingStates[newState];
            _currentState.StartTransition();
        }
    }
}
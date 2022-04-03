using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Enemy
{
    public class EnemyAnimationLeftSideWalk : StateMachineBehaviour
    {
        [Header("References:")]
        [SerializeField] private EnemyAI _ai;

        [Header("Stats")]
        [SerializeField] private float _leftSideWalkSpeedMultiplier = 4.5f;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_ai == null)
            {
                _ai = animator.GetComponentInParent<EnemyAI>();
            }

            if (_ai == null)
            {
                Debug.LogError("AI doesnt found");
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _ai.Controller.Move(
                        (-_ai.gameObject.transform.right)
                        * (_ai.NavMeshAgent.speed / 2)
                        * _leftSideWalkSpeedMultiplier
                        * Time.deltaTime);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
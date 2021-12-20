using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterAnimMoveState : CharacterAnimBaseState
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (VirtualInputManager.Instance.Roll)
            {
                animator.SetBool("Roll", true);
            }

            if (PlayerMovement.Direction.magnitude < 0.3f)
                animator.SetBool("Idle", true);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Move", false);
        }
    }
}
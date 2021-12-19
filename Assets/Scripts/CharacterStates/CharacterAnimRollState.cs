using UnityEngine;


namespace SLGame.Gameplay
{
    public class CharacterAnimRollState : CharacterAnimBaseState
    {
        [Header("Stats:")]
        [SerializeField] private float _speedMultiplier;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);

            PlayerMovement.MoveSpeed = PlayerMovement.MoveSpeed * _speedMultiplier;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (PlayerMovement.Direction.magnitude > 0.3f)
            {
                animator.SetBool("Move", true);
                return;
            }

            animator.SetBool("Idle", true);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Roll", false);
            PlayerMovement.MoveSpeed = PlayerMovement.MoveSpeed / _speedMultiplier;
        }
    }
}
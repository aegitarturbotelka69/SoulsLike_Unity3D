using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterRollState : CharacterBaseState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);
            PlayerMovement.MoveSpeed = PlayerMovement.MoveSpeed * 2f;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                animator.SetBool(States.Move.ToString(), true);
                return;
            }
            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                animator.SetBool(States.Move.ToString(), true);
                return;
            }

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                animator.SetBool(States.Move.ToString(), true);
                return;
            }

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool(States.Move.ToString(), true);
                return;
            }

            animator.SetBool(States.Idle.ToString(), true);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerMovement.MoveSpeed = PlayerMovement.MoveSpeed / 2f;
            animator.SetBool(States.Roll.ToString(), false);
        }
    }
}
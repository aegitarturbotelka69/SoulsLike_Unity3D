using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterWalkState : CharacterBaseState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
                PlayerMovement.zAxis = 1f;
            else if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
                PlayerMovement.zAxis = -1f;
            else
                PlayerMovement.zAxis = 0f;

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
                PlayerMovement.xAxis = 1f;
            else if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
                PlayerMovement.xAxis = -1f;
            else
                PlayerMovement.xAxis = 0f;

            if (PlayerMovement.zAxis == 0f && PlayerMovement.xAxis == 0f)
            {
                animator.SetBool(States.Idle.ToString(), true);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetBool(States.Move.ToString(), false);
        }
    }
}

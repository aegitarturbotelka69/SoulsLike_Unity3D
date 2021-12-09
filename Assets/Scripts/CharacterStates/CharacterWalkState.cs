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
            Apply_Z_AxisWalk(ref animator);
            Apply_X_AxisWalk(ref animator);

            if (PlayerMovement.zAxis == 0f && PlayerMovement.xAxis == 0f)
                animator.SetBool(States.Idle.ToString(), true);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetBool(States.Move.ToString(), false);
        }



        private void Apply_Z_AxisWalk(ref Animator animator)
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
            {
                PlayerMovement.zAxis = 0f;
                return;
            }
            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                PlayerMovement.zAxis = 1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                PlayerMovement.zAxis = -1f;
                return;
            }

            PlayerMovement.zAxis = 0f;
        }

        private void Apply_X_AxisWalk(ref Animator animator)
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
            {
                PlayerMovement.xAxis = 0f;
                return;
            }

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                PlayerMovement.xAxis = -1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                PlayerMovement.xAxis = 1f;
                return;
            }

            PlayerMovement.xAxis = 0f;
        }
    }
}

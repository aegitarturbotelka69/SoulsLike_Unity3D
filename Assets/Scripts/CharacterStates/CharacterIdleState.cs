using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterIdleState : CharacterBaseState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);
        }
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {

            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveBack || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight)
            {
                animator.SetBool(States.Move.ToString(), true);
            }

        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetBool(States.Idle.ToString(), false);
        }
    }
}

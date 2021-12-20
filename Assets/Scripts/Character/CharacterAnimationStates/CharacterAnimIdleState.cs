using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class CharacterAnimIdleState : CharacterAnimBaseState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);
            Debug.Log("Entering idle state");
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (PlayerMovement.Direction.magnitude > 0.3f)
            {
                animator.SetBool("Move", true);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetBool("Idle", false);
        }
    }
}

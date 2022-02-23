using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class CharacterAnimHardLandState : CharacterAnimBaseState
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            PlayerMovement playerMovement = animator.GetComponent<PlayerMovement>();
            playerMovement.CurrentCharacterControllingState = playerMovement.CharacterControllingStates[States.Idle];
        }
    }
}
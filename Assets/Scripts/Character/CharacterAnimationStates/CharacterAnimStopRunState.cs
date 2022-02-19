using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace SLGame.Gameplay
{
    public class CharacterAnimStopRunState : CharacterAnimBaseState
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            PlayerMovement playerMovement = animator.GetComponent<PlayerMovement>();
            playerMovement.CurrentCharacterControllingState = playerMovement.CharacterControllingStates[States.Idle];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SLGame.Gameplay
{
    public class CharacterBaseState : StateMachineBehaviour
    {
        public PlayerMovement PlayerMovement { get; private set; }

        public PlayerMovement GetPlayerMovement(ref Animator animator)
        {
            if (PlayerMovement == null)
            {
                PlayerMovement = animator.gameObject.GetComponent<PlayerMovement>();
            }

            return PlayerMovement;
        }
    }
}
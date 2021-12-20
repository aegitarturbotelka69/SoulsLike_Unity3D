using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SLGame.Gameplay
{
    public class CharacterAnimBaseState : StateMachineBehaviour
    {
        [SerializeField] public PlayerMovement PlayerMovement;

        public void GetPlayerMovement(ref Animator animator)
        {
            if (PlayerMovement == null)
            {
                PlayerMovement = animator.gameObject.GetComponent<PlayerMovement>();
                return;
            }

            if (PlayerMovement == null)
                Debug.LogError("Doesnt found PlayerMovement script");
        }
    }
}

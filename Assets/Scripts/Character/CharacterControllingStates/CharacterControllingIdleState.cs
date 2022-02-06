using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingIdleState : CharacterControllingBaseState
    {
        public CharacterControllingIdleState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }


        public override void Execute()
        {
            if (VirtualInputManager.Instance.MoveFront
                || VirtualInputManager.Instance.MoveBack
                || VirtualInputManager.Instance.MoveLeft
                || VirtualInputManager.Instance.MoveRight)
            {
                playerMovement.ChangeControllingState(States.Move);
            }
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
        }

        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), false);
        }
    }
}
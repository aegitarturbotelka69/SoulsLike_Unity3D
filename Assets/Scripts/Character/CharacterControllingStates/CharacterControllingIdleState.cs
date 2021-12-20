using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingIdleState : CharacterControllingBaseState
    {
        public CharacterControllingIdleState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
            Debug.Log("idle state");
        }

        public override void EndTransition()
        {
            base.EndTransition();
        }

        public override void StartTransition(CharacterControllingBaseState newControllingState)
        {
            base.StartTransition(newControllingState);
        }

        public override void GetInput()
        {
            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveBack || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight)
            {
                playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
                StartTransition(new CharacterControllingMoveState(ref playerMovement));
            }
        }
    }
}
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
        public override void GetInput()
        {
            Debug.Log("Idle state");
            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveBack || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight)
            {
                playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
                playerMovement._currentCharacterControllingState = playerMovement.CharacterControllingStates[States.Move];
            }
        }
    }
}
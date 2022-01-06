using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingRunState : CharacterControllingBaseState
    {
        public CharacterControllingRunState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }
        private void GetVerticalInput()
        {

        }
        private void GetHorizontalInput()
        {

        }

        public override void Execute()
        {
            GetHorizontalInput();
            GetVerticalInput();
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
        }

        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), false);
        }
    }
}
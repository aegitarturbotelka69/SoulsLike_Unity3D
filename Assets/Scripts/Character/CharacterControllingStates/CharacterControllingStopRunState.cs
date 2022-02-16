using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingStopRunState : CharacterControllingBaseState
    {
        public CharacterControllingStopRunState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference) { }

        private void GetAbilitiesInput()
        {

        }

        public override void Execute()
        {
            base.Execute();
            throw new System.Exception("Now implemented");
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), true);
        }

        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), false);
        }
    }
}
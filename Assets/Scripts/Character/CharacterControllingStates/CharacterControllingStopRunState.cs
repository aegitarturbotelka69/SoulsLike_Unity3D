using UnityEngine;
using SLGame.Input;
using System.Threading;

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
            //throw new System.Exception("Now implemented");
            //Thread.Sleep(2090);
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), true);
        }

        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), false);
            playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
        }
    }
}
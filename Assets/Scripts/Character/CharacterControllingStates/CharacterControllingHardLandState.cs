using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingHardLandState : CharacterControllingBaseState
    {
        public CharacterControllingHardLandState(ref PlayerMovement playerMovementReference, ref CharacterController controller) : base(ref playerMovementReference, ref controller) { }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StartTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(States.HardLand.ToString(), true);
        }

        public override void EndTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(States.HardLand.ToString(), false);
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
        }
    }
}
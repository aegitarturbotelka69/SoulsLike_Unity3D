using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingHardLandState : CharacterControllingBaseState
    {
        public CharacterControllingHardLandState(States enumState, PlayerMovement playerMovementReference, ref CharacterController controller)
        : base(enumState, playerMovementReference, ref controller) { }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StartTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(this.EnumState.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(this.EnumState.ToString(), false);

            if (!endingManually)
                return;

            _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Idle];
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
            return;
        }
    }
}
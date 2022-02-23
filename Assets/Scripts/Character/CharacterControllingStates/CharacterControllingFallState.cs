using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingFallState : CharacterControllingBaseState
    {
        public CharacterControllingFallState(ref PlayerMovement playerMovementReference, ref CharacterController controller) : base(ref playerMovementReference, ref controller)
        {
            this._playerMovement = playerMovementReference;
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StartTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(States.Falling.ToString(), true);
        }

        public override void EndTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(States.Falling.ToString(), false);
        }
    }
}
using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingAttackState : CharacterControllingBaseState
    {
        public CharacterControllingAttackState(States enumState, PlayerMovement playerMovementReference, ref CharacterController controller)
            : base(enumState, playerMovementReference, ref controller) { }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StartTransition()
        {
            //base.StartTransition();
            //_playerMovement.CharacterAnimator.SetBool(States.Attack.ToString(), false);
        }

        public override void EndTransition(bool endingManually)
        {
            //base.EndTransition(endingManually);
            _playerMovement.CharacterAnimator.SetBool(States.Attack.ToString(), false);
        }
    }
}
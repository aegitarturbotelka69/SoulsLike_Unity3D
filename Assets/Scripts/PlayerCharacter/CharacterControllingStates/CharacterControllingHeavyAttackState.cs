using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingHeavyAttackState : CharacterControllingBaseState
    {
        public CharacterControllingHeavyAttackState(States enumState, PlayerMovement playerMovementReference, CharacterController controller)
            : base(enumState, playerMovementReference, controller) { }

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
            _playerMovement.CharacterAnimator.SetBool(States.HeavyAttack.ToString(), false);

            if (!endingManually)
                return;

            _playerMovement.ChangeControllingState(States.Idle);
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
            //base.EndTransition(endingManually);
        }
    }
}
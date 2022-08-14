using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingIdleState : CharacterControllingBaseState
    {
        public CharacterControllingIdleState(States enumState, PlayerMovement playerMovementReference, CharacterController controller)
        : base(enumState, playerMovementReference, controller) { }

        private void GetAbilitiesInput()
        {
            if (VirtualInputManager.Instance.MoveFront
                            || VirtualInputManager.Instance.MoveBack
                            || VirtualInputManager.Instance.MoveLeft
                            || VirtualInputManager.Instance.MoveRight)
            {
                _playerMovement.ChangeControllingState(States.Move, false);
            }
        }

        public override void Execute()
        {
            base.Execute();
            GetAbilitiesInput();
        }

        public override void StartTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), false);
        }
    }
}
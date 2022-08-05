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
            _playerMovement.CharacterAnimator.SetBool(States.HardLand.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(States.HardLand.ToString(), false);

            if (!endingManually)
                return;

            if (VirtualInputManager.Instance.MoveBack
                || VirtualInputManager.Instance.MoveFront
                || VirtualInputManager.Instance.MoveLeft
                || VirtualInputManager.Instance.MoveRight)
            {
                _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Move];
                _playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
                return;
            }

            if (VirtualInputManager.Instance.Roll)
            {
                _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Roll];
                _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
                return;
            }

            _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Idle];
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
            return;
        }
    }
}
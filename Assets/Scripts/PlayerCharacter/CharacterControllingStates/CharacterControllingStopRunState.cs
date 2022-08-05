using UnityEngine;
using SLGame.Input;
using System.Threading;

namespace SLGame.Gameplay
{
    public class CharacterControllingStopRunState : CharacterControllingBaseState
    {
        public CharacterControllingStopRunState(States enumState, PlayerMovement playerMovementReference, ref CharacterController controller)
            : base(enumState, playerMovementReference, ref controller) { }

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
            _playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), false);

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

            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
            _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Idle];
            return;
        }
    }
}
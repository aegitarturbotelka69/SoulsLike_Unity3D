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

        private void CheckInput()
        {
            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight || VirtualInputManager.Instance.MoveBack)
            {
                _playerMovement.ChangeControllingState(States.Move);
                return;
            }
            else
            {
                _playerMovement.ChangeControllingState(States.Idle);
                return;
            }
        }

        public override void Execute()
        {
            base.Execute();

            if (PlayerGravityCheck.PLAYER_IS_GROUNDED)
            {
                CheckInput();
            }
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
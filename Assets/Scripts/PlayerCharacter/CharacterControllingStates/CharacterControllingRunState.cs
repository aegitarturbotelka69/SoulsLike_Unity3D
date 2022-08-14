using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingRunState : CharacterControllingBaseState
    {
        public CharacterControllingRunState(States enumState, PlayerMovement playerMovementReference, CharacterController controller)
        : base(enumState, playerMovementReference, controller) { }

        private void GetAbilitiesInput()
        {
            if (!VirtualInputManager.Instance.MoveFront
                && !VirtualInputManager.Instance.MoveBack
                && !VirtualInputManager.Instance.MoveLeft
                && !VirtualInputManager.Instance.MoveRight)
            {
                _playerMovement.ChangeControllingState(States.StopRun, false);
            }

            if (VirtualInputManager.Instance.Run)
            {
                _playerMovement.ChangeControllingState(States.Move, false);
                return;
            }
        }

        private void GetVerticalInput()
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                _playerMovement.xAxis = 1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                _playerMovement.xAxis = -1f;
                return;
            }

            _playerMovement.xAxis = 0f;
        }
        private void GetHorizontalInput()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                _playerMovement.zAxis = 1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                _playerMovement.zAxis = -1f;
                return;
            }

            _playerMovement.zAxis = 0f;
        }

        private void Move()
        {
            _playerMovement.Direction = new Vector3(_playerMovement.xAxis, 0f, _playerMovement.zAxis);

            if (_playerMovement.Direction.magnitude > 0.3f)
            {
                float lookAngle = Mathf.Atan2(_playerMovement.Direction.x, _playerMovement.Direction.z) * Mathf.Rad2Deg + _playerMovement._cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(_playerMovement.transform.eulerAngles.y, lookAngle, ref _playerMovement.MoveVelocity, _playerMovement.rotationSpeed);
                _playerMovement.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;

                _characterController.Move(moveDirection.normalized * _playerMovement.MoveSpeed * _playerMovement.RunSpeedMultiplier * Time.deltaTime);
            }
        }

        public override void Execute()
        {
            base.Execute();
            GetHorizontalInput();
            GetVerticalInput();
            GetAbilitiesInput();
            Move();
        }

        public override void StartTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(States.Run.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(States.Run.ToString(), false);
        }
    }
}
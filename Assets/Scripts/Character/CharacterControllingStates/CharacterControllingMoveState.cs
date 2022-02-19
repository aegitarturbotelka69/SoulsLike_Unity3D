using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingMoveState : CharacterControllingBaseState
    {
        public CharacterControllingMoveState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }

        private void GetAbilitiesInput()
        {
            if (!VirtualInputManager.Instance.MoveFront
                && !VirtualInputManager.Instance.MoveBack
                && !VirtualInputManager.Instance.MoveLeft
                && !VirtualInputManager.Instance.MoveRight)
            {
                playerMovement.ChangeControllingState(States.Idle);
            }

            if (VirtualInputManager.Instance.Roll && !playerMovement.RollOnCooldown)
            {
                playerMovement.ChangeControllingState(States.Roll);
            }

            if (VirtualInputManager.Instance.Run)
            {
                playerMovement.ChangeControllingState(States.Run);
            }
        }
        private void GetVerticalInput()
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                playerMovement.xAxis = 1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                playerMovement.xAxis = -1f;
                return;
            }

            playerMovement.xAxis = 0f;
        }
        private void GetHorizontalInput()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                playerMovement.zAxis = 1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                playerMovement.zAxis = -1f;
                return;
            }

            playerMovement.zAxis = 0f;
        }

        private void Move()
        {
            playerMovement.Direction = new Vector3(playerMovement.xAxis, 0f, playerMovement.zAxis);

            if (playerMovement.Direction.magnitude > 0.3f)
            {
                float lookAngle = Mathf.Atan2(playerMovement.Direction.x, playerMovement.Direction.z) * Mathf.Rad2Deg + playerMovement._cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(playerMovement.transform.eulerAngles.y, lookAngle, ref playerMovement.velocity, playerMovement.rotationSpeed);
                playerMovement.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;

                playerMovement._characterController.Move(moveDirection.normalized * playerMovement.MoveSpeed * Time.deltaTime);
            }
        }

        public override void Execute()
        {
            base.Execute();
            GetAbilitiesInput();
            GetHorizontalInput();
            GetVerticalInput();
            Move();
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
        }

        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), false);
        }
    }
}
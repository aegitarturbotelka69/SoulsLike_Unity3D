using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingRollState : CharacterControllingBaseState
    {
        [SerializeField] private float _rollSpeedBoost = 22.35f;
        public CharacterControllingRollState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }

        public override void GetInput()
        {
            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveBack || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight)
            {
                playerMovement.ChangeControllingState(States.Move);
            }
            else
            {
                playerMovement.ChangeControllingState(States.Idle);
            }
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
        }
        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), false);
        }

        public override void Move()
        {
            Debug.LogWarning("Roll state");
            playerMovement.Direction = new Vector3(playerMovement.xAxis, 0f, playerMovement.zAxis);


            float lookAngle = Mathf.Atan2(playerMovement.Direction.x, playerMovement.Direction.z) * Mathf.Rad2Deg + playerMovement._cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(playerMovement.transform.eulerAngles.y, lookAngle, ref playerMovement.velocity, playerMovement.rotationSpeed);
            playerMovement.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;

            playerMovement._characterController.Move(moveDirection.normalized * playerMovement.MoveSpeed * Time.deltaTime * _rollSpeedBoost);
            playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
        }

        public override void Rotate()
        {
            base.Rotate();

        }
    }
}
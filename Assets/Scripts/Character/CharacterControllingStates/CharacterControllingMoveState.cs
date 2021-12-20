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

        public override void GetInput()
        {

        }

        public override void StartTransition(States newState)
        {
            base.StartTransition(newState);
        }


        public override void Move()
        {
            Debug.Log("Move state");
            playerMovement.Direction = new Vector3(playerMovement.xAxis, 0f, playerMovement.zAxis);

            if (playerMovement.Direction.magnitude > 0.3f)
            {
                float lookAngle = Mathf.Atan2(playerMovement.Direction.x, playerMovement.Direction.z) * Mathf.Rad2Deg + playerMovement._cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(playerMovement.transform.eulerAngles.y, lookAngle, ref playerMovement.velocity, playerMovement.rotationSpeed);
                playerMovement.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;

                playerMovement._characterController.Move(moveDirection.normalized * playerMovement.MoveSpeed * Time.deltaTime);
            }
            else
            {
                StartTransition(States.Idle);
            }
        }
    }
}
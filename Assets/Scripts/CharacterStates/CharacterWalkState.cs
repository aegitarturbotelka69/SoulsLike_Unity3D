using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterWalkState : CharacterBaseState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GetPlayerMovement(ref animator);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (VirtualInputManager.Instance.Roll)
            {
                animator.SetBool(States.Roll.ToString(), true);
                return;
            }

            Get_Z_AxisWalk(ref animator);
            Get_X_AxisWalk(ref animator);

            Rotate();
            Move();

            if (PlayerMovement.zAxis == 0f && PlayerMovement.xAxis == 0f)
                animator.SetBool(States.Idle.ToString(), true);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetBool(States.Move.ToString(), false);
        }



        private void Get_Z_AxisWalk(ref Animator animator)
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
            {
                PlayerMovement.zAxis = 0f;
                return;
            }
            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                PlayerMovement.zAxis = 1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                PlayerMovement.zAxis = -1f;
                return;
            }

            PlayerMovement.zAxis = 0f;
        }

        private void Get_X_AxisWalk(ref Animator animator)
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
            {
                PlayerMovement.xAxis = 0f;
                return;
            }

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                PlayerMovement.xAxis = -1f;
                return;
            }

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                PlayerMovement.xAxis = 1f;
                return;
            }

            PlayerMovement.xAxis = 0f;
        }
        private void Rotate()
        {
            Vector3 targetDir = Vector3.zero;

            targetDir = PlayerMovement._cameraObject.forward * PlayerMovement.zAxis;
            targetDir += PlayerMovement._cameraObject.right * PlayerMovement.xAxis;

            PlayerMovement.TargetDirection = targetDir;
            targetDir.Normalize();
            targetDir.y = 0;
            PlayerMovement.TargetDirectionNormalized = targetDir;

            if (targetDir == Vector3.zero)
                targetDir = PlayerMovement.transform.forward;

            Quaternion rotation = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(PlayerMovement.transform.rotation, rotation, PlayerMovement._rotationSpeed * Time.deltaTime);

            PlayerMovement.transform.rotation = targetRotation;
        }

        private void Move()
        {
            Vector3 cameraPosition = PlayerMovement._cameraObject.forward;
            cameraPosition.y = 0;

            PlayerMovement._moveDirection = cameraPosition * PlayerMovement.zAxis;

            cameraPosition = PlayerMovement._cameraObject.right;
            cameraPosition.y = 0;

            PlayerMovement._moveDirection += cameraPosition * PlayerMovement.xAxis;
            PlayerMovement._moveDirection.Normalize();

            PlayerMovement._moveDirection *= PlayerMovement.MoveSpeed;

            PlayerMovement._rigidbody.MovePosition(PlayerMovement.transform.position + PlayerMovement._moveDirection);
        }
    }
}

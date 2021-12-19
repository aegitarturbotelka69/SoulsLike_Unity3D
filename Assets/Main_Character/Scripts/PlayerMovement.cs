using System;
using UnityEngine;
using SLGame.Input;
using SLGame.Modules;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;


        [Header("Stats:")]
        [SerializeField] public float MoveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 0.1f;


        [Header("Info:")]
        [SerializeField] private float zAxis;
        [SerializeField] private float xAxis;

        [SerializeField] private float velocity;
        [SerializeField] public Vector3 Direction;


        public Vector3 norm;
        public Vector3 notNorm;
        private void Update()
        {
            GetHorizontalInput();
            GetVerticalInput();
            Move();
        }



        private void GetHorizontalInput()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                zAxis = 1f;
                return;
            }
            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                zAxis = -1f;
                return;
            }

            zAxis = 0f;

        }
        private void GetVerticalInput()
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                xAxis = -1f;
                return;
            }
            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                xAxis = 1f;
                return;
            }

            xAxis = 0f;
        }

        private void Move()
        {
            Direction = new Vector3(xAxis, 0f, zAxis);

            if (Direction.magnitude > 0.3f)
            {
                float lookAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookAngle, ref velocity, rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;

                _characterController.Move(moveDirection.normalized * MoveSpeed * Time.deltaTime);
            }
        }
    }
}
using System;
using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Transform _cameraObject;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Stats:")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        [Header("Info:")]
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private Quaternion _moveRotation;


        [SerializeField] private Vector3 normalVector;

        [Space(10)]

        [SerializeField] private float zAxis;
        [SerializeField] private float xAxis;



        private void Start()
        {
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetInput();
            Rotate();
            Move();
        }

        private void GetInput()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
                zAxis = 1f;
            else if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
                zAxis = -1f;
            else
                zAxis = 0f;

            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
                xAxis = 1f;
            else if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
                xAxis = -1f;
            else
                xAxis = 0f;
        }

        private void Rotate()
        {
            Vector3 targetDirection = new Vector3(xAxis, 0f, zAxis);


            Quaternion rotationAngle = Quaternion.LookRotation(targetDirection);
            Debug.Log(rotationAngle);
            if (rotationAngle.x == 0 && rotationAngle.y == 0 && rotationAngle.z == 0 && !VirtualInputManager.Instance.MoveFront)
                return;

            Quaternion targetRotation = Quaternion.Slerp(this.gameObject.transform.rotation, rotationAngle, _rotationSpeed * Time.deltaTime);

            this.transform.rotation = targetRotation;
        }

        private void Move()
        {
            _moveDirection = _cameraObject.forward * zAxis;
            _moveDirection += _cameraObject.right * xAxis;
            _moveDirection.Normalize();

            _moveDirection *= _moveSpeed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection, normalVector);

            _rigidbody.MovePosition(this.transform.position + projectedVelocity);
        }
    }
}
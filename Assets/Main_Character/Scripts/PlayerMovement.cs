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


        [Header("Info:")]
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private float _moveSpeed;


        Vector3 normalVector;

        [Space(10)]

        [SerializeField] private float zAxis;
        [SerializeField] private float xAxis;
        private void Start()
        {
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
        }

        private void Rotate()
        {

        }

        private void Move()
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

            _moveDirection = new Vector3(xAxis, 0f, zAxis) * _moveSpeed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection, normalVector);
            _rigidbody.MovePosition(this.transform.position + projectedVelocity);
        }
    }
}
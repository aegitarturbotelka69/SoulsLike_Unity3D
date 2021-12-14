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
        [SerializeField] public float MoveSpeed;
        [SerializeField] private float _rotationSpeed;

        [Header("Info:")]
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private Quaternion _moveRotation;


        [SerializeField] private Vector3 normalVector;

        [Space(10)]

        [SerializeField] public float zAxis;
        [SerializeField] public float xAxis;



        private void Start()
        {
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Rotate();
            Move();
        }

        private void Rotate()
        {
            Vector3 targetDir = Vector3.zero;

            targetDir = _cameraObject.forward * zAxis;
            targetDir += _cameraObject.right * xAxis;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = this.transform.forward;

            Quaternion rotation = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(this.transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

            this.transform.rotation = targetRotation;
        }

        private void Move()
        {
            Vector3 cameraPosition = _cameraObject.forward;
            cameraPosition.y = 0;

            _moveDirection = cameraPosition * zAxis;

            cameraPosition = _cameraObject.right;
            cameraPosition.y = 0;

            _moveDirection += cameraPosition * xAxis;
            _moveDirection.Normalize();

            _moveDirection *= MoveSpeed;

            _rigidbody.MovePosition(this.transform.position + _moveDirection);
        }
    }
}
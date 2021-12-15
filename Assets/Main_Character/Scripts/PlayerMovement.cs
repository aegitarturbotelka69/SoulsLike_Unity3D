using System;
using UnityEngine;
using SLGame.Input;
using SLGame.Modules;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] public Transform _cameraObject;
        [SerializeField] public Rigidbody _rigidbody;

        [Header("Stats:")]
        [SerializeField] public float MoveSpeed = 0.048f;
        [SerializeField] public float _rotationSpeed = 5f;

        [Header("Info:")]
        [SerializeField] public Vector3 _moveDirection;
        [SerializeField] public Quaternion _moveRotation;

        [Space(10)]

        [SerializeField] public float zAxis;
        [SerializeField] public float xAxis;

        [Space(10)]

        [SerializeField] public Vector3 CameraForward;
        [SerializeField] public Vector3 CameraRight;
        [SerializeField] public Vector3 TargetDirection;
        [SerializeField] public Vector3 TargetDirectionNormalized;



        private void Awake()
        {
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            CameraForward = _cameraObject.forward;
            CameraRight = _cameraObject.right;
        }


    }
}
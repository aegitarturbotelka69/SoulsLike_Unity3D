using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class MainCamera : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;

        [Space(10)]

        [SerializeField] private Transform _highestPivotPointCameraPosition;
        [SerializeField] private Transform _centerPivotPointCameraPosition;
        [SerializeField] private Transform _lowestPivotPointCameraPosition;

        [SerializeField] private Transform _cameraPivotPoint;


        [Header("Stats:")]


        [SerializeField] private float _followSpeed;
        [SerializeField] private float _xRotationSpeed;
        [SerializeField] private float _yRotationSpeed;

        [Space(10)]
        [SerializeField] private float _maxPivotAngle;
        [SerializeField] private float _minPivotAngle;

        [Header("Info")]
        [SerializeField] private float _lookAngle;
        [SerializeField] private float _pivotAngle;

        [SerializeField] private Vector3 _realPivotAngle;

        private void Update()
        {
            Rotation();
        }
        private void FixedUpdate()
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _targetTransform.transform.position, _followSpeed);
        }

        private void Rotation()
        {
            _lookAngle += UnityEngine.Input.GetAxis("Mouse X") * _xRotationSpeed;
            _pivotAngle -= UnityEngine.Input.GetAxis("Mouse Y") * _yRotationSpeed;
            _pivotAngle = Mathf.Clamp(_pivotAngle, _minPivotAngle, _maxPivotAngle);

            if (-0.9f > (_pivotAngle / Mathf.Abs(_minPivotAngle)))
            {
                Debug.Log("Lowest");
                _realPivotAngle = Vector3.Slerp(_centerPivotPointCameraPosition.position, _lowestPivotPointCameraPosition.position, (_pivotAngle / _minPivotAngle));
            }
            else if (0.9f < (_pivotAngle / Mathf.Abs(_maxPivotAngle)))
            {
                Debug.Log("Highest");
                _realPivotAngle = Vector3.Slerp(_highestPivotPointCameraPosition.position, _centerPivotPointCameraPosition.position, (_pivotAngle / _minPivotAngle));
            }
            else
            {
                Debug.Log("Center");
                _realPivotAngle = _centerPivotPointCameraPosition.position;
            }
            _cameraPivotPoint.position = Vector3.Slerp(_cameraPivotPoint.position, _realPivotAngle, (_pivotAngle / _minPivotAngle));

            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngle;

            Quaternion targetRotation = Quaternion.Euler(rotation);
            this.transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = _pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            _cameraPivotPoint.localRotation = targetRotation;
        }
    }
}
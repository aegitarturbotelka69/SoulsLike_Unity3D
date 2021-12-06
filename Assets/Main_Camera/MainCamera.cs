using UnityEngine;

namespace SLGame.Gameplay
{
    public class MainCamera : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _cameraPositionWithOffset;


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

        private void Awake()
        {

        }

        private void Update()
        {
            FollowTarget();
            Rotation();
        }

        private void FollowTarget()
        {
            Vector3 followPosition = Vector3.Lerp(this.transform.position, _targetTransform.position, _followSpeed * Time.deltaTime);

            this.transform.position = followPosition;
        }

        private void Rotation()
        {
            _lookAngle += UnityEngine.Input.GetAxis("Mouse X") * _xRotationSpeed;
            _pivotAngle -= UnityEngine.Input.GetAxis("Mouse Y") * _yRotationSpeed;
            _pivotAngle = Mathf.Clamp(_pivotAngle, _minPivotAngle, _maxPivotAngle);

            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngle;

            Quaternion targetRotation = Quaternion.Euler(rotation);
            this.transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = _pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            _cameraPositionWithOffset.localRotation = targetRotation;
        }
    }
}
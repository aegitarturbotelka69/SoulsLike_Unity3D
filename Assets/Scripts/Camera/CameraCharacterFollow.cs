using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay.Camera
{
    public class CameraCharacterFollow : MonoBehaviour
    {
        [SerializeField] Transform _targetTransform;
        [SerializeField] Transform _cameraTransform;

        [SerializeField] private float _distanceOffset;
        [SerializeField, Range(0f, 1f)] private float _smoothnessTime = 0.125f;

        [SerializeField] private Vector3 velocity = Vector3.zero;
        private void LateUpdate()
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, _targetTransform.position, ref velocity, _smoothnessTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay.Camera
{
    public class CameraOrbitRotation : MonoBehaviour
    {
        [Header("Stats:")]

        [SerializeField] private bool mouseInversion = false;
        [SerializeField] private float mouse_X_Sensitivity;
        [SerializeField] private float mouse_Y_Sensitivity;

        [Space(10)]
        [SerializeField] private float mouse_X_maxAngle;
        [SerializeField] private float mouse_X_minAngle;

        [Header("Info:")]
        [SerializeField] private float mouse_X_Input;
        [SerializeField] private float mouse_Y_Input;

        [SerializeField] private float rotationX = 0f;
        [SerializeField] private float rotationY = 0f;

        [SerializeField] private bool _rotatePossibility = true;

        private void Awake()
        {
            SLGame.UI.UI.OnMenuOpened += ChangeRotatePossibilityStatus;
        }
        private void Update()
        {
            GetMouseInput();
            Rotate();
        }

        private void GetMouseInput()
        {
            if (mouseInversion == false)
                mouse_Y_Input = UnityEngine.Input.GetAxis("Mouse Y") * mouse_X_Sensitivity * -1f;
            else
                mouse_Y_Input = UnityEngine.Input.GetAxis("Mouse Y") * mouse_X_Sensitivity;

            mouse_X_Input = UnityEngine.Input.GetAxis("Mouse X") * mouse_X_Sensitivity;
        }

        private void ChangeRotatePossibilityStatus(bool status)
        {
            // ! Here "!" is important, cause if UI shown status will be true but rotatePossibility must be false
            _rotatePossibility = !status;
        }
        private void Rotate()
        {
            if (_rotatePossibility == false)
                return;

            rotationY += mouse_X_Input;
            rotationX += mouse_Y_Input;

            rotationX = Mathf.Clamp(rotationX, mouse_X_minAngle, mouse_X_maxAngle);

            if (rotationY >= 360 || rotationY <= -360)
                rotationY = 0f;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
    }
}
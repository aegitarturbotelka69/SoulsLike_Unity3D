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
        private void Update()
        {
            Rotate();
        }

        private void GetMouseInput()
        {
            if (mouseInversion == false)
                mouse_Y_Input = UnityEngine.Input.GetAxis("Mouse Y") * mouse_X_Sensitivity * -1f;
            else
                mouse_Y_Input = UnityEngine.Input.GetAxis("Mouse Y") * mouse_X_Sensitivity;
            //mouse_X_Input = UnityEngine.Input.GetAxis("Mouse X") * mouse_Y_Sensitivity;   
        }
        private void Rotate()
        {
            this.transform.localEulerAngles += new Vector3(mouse_Y_Input, mouse_X_Input, 0f);


        }
    }
}
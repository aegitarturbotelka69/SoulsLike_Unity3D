// Interpolates rotation between the rotations "from" and "to"
// (Choose from and to not to be the same as
// the object you attach this script to)

using UnityEngine;
using System.Collections;

public class example : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Vector3 rotationAngle;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    void Start()
    {
        this.transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        float mouseX = UnityEngine.Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = UnityEngine.Input.GetAxis("Mouse Y") * mouseSensitivity * -1f;


        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, -40f, 40f);

        if (rotationY >= 360 || rotationY <= -360)
            rotationY = 0;

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}
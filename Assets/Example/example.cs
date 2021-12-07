// Interpolates rotation between the rotations "from" and "to"
// (Choose from and to not to be the same as
// the object you attach this script to)

using UnityEngine;
using System.Collections;

public class example : MonoBehaviour
{
    public Transform from;
    public Transform to;

    public float speed;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, speed);
    }
}
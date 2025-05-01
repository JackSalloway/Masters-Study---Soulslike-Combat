using System;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
    public float angleRange = 45f; // Max range for axe to swing
    public float speed = 1f; // Speed the axe should swing at
    public Vector3 rotationAxis = Vector3.forward; // Axis of rotation (X axis)

    private Quaternion startRotation;
    private Quaternion rotationA;
    private Quaternion rotationB;

    void Start()
    {
        startRotation = transform.localRotation;

        // Define two target rotations from the start
        rotationA = startRotation * Quaternion.AngleAxis(-angleRange / 2f, rotationAxis);
        rotationB = startRotation * Quaternion.AngleAxis(angleRange / 2f, rotationAxis);
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // Oscillates between 0 and 1
        transform.localRotation = Quaternion.Slerp(rotationA, rotationB, t);
    }
}

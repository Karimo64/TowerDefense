using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float floatHeight = 0.5f; // How high it floats up and down
    public float floatSpeed = 2f; // Speed of the floating (how fast it goes up and down)
    public float rotationSpeed = 90f; // Speed of the rotation (degrees per second)

    private Vector3 startPos;

    void Start()
    {
        // Store the initial position of the object
        startPos = transform.position;
    }

    void Update()
    {
        // Create floating motion using sine wave for smooth up and down
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Apply the floating effect to the object's position
        transform.position = startPos + new Vector3(0, yOffset, 0);

        // Rotate the object continuously
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Rotation around the Y-axis (spinning like a coin)
    }
}

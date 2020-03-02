using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 10.0f;
    //public float rotationSpeed = 100.0f;

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float verticalTransform = Input.GetAxis("Vertical") * speed;
        float horizontalTransform = Input.GetAxis("Horizontal") * speed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        verticalTransform *= Time.deltaTime;
        horizontalTransform *= Time.deltaTime;

        // Move translation along the object's z and x axis
        transform.Translate(horizontalTransform, 0, verticalTransform);

        // Rotate around our y-axis
        //transform.Rotate(0, rotation, 0);
    }


}

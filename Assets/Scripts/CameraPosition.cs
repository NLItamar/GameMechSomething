using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject Player;

    //this will be the difference in position relation from the player to the camera
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    public GameObject gameManager;
    private Rigidbody rgBody;

    private void Awake()
    {
        rgBody = this.gameObject.GetComponent<Rigidbody>();
        rgBody.sleepThreshold = 0.0f;
    }

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            //game over
            gameManager.GetComponent<EndLevelScript>().GameOver("GameOver");
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            //game over
            gameManager.GetComponent<EndLevelScript>().GameOver("GameOver");
        }
    }
}

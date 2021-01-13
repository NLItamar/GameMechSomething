using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionEnemySide : MonoBehaviour
{
    public GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
        {
            Debug.Log("hit player from enemy side, OnControllerColliderHit");
            //game over
            EndIt();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("hit player from enemy side, OnCollisionEnter");
            //game over
            EndIt();
        }
    }

    private void EndIt()
    {
        gameManager.GetComponent<EndLevelScript>().GameOver("GameOver");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    public GameObject gameManager;

    private bool collidingEnemy;

    private void Start()
    {
        collidingEnemy = false;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Enemy" && collidingEnemy != false)
        {
            collidingEnemy = true;

            gameManager.GetComponent<LivingBehaviour>().currentLives--;
            Debug.Log(gameManager.GetComponent<LivingBehaviour>().currentLives);
        }
    }
}

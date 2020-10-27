using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    public GameObject gameManager;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            //game over
            gameManager.GetComponent<EndLevelScript>().GameOver("GameOver");
        }
    }
}

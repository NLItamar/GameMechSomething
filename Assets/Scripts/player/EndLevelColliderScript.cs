using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelColliderScript : MonoBehaviour
{
    public Text endText;
    public GameObject gameManager;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //first check if you are eligable to end the game then check if you are at the location
        if(gameManager.GetComponent<EndLevelScript>().endGame)
        {
            if (hit.collider.name == "EndLevelCollider")
            {
                endText.gameObject.SetActive(true);
                Debug.Log("end it!");
            }
        }
    }
}

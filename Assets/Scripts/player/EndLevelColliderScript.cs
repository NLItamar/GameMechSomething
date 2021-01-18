using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelColliderScript : MonoBehaviour
{
    public Text endText;
    public GameObject gameManager;

    private JournalProgression journalProgression;
    private EndLevelScript endLevelScript;

    private void Start()
    {
        journalProgression = gameManager.GetComponent<JournalProgression>();
        endLevelScript = gameManager.GetComponent<EndLevelScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //first check if you are eligable to end the game then check if you are at the location
        if (endLevelScript.endGame)
        {
            if (other.CompareTag("EndLevelCollider"))
            {
                endText.gameObject.SetActive(true);

                //goes to the gameover scene
                journalProgression.JournalAddText("GameOver");
            }
        }
    }
}

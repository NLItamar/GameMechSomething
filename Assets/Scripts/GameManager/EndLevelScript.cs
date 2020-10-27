using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{ 
    public GameObject escapeThing;
    public GameObject endCollider;

    public bool endGame = false;

    public void EndLevel()
    {
        endGame = true;

        //opens the door
        escapeThing.SetActive(false);

        //enables the collider
        endCollider.SetActive(true);
    }

    public void GameOver(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

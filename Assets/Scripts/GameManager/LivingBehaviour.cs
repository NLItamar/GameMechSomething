using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingBehaviour : MonoBehaviour
{
    public int currentLives;
    private readonly int maxLives;

    private void Update()
    {
        if(currentLives == 0)
        {
            Debug.Log("you lost");
            //SceneManager.LoadScene("Level01");
        }
    }
}

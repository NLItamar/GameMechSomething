using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
    //notice this does not exit the game in editor mode
    public void ExitGame()
    {
        Application.Quit();
    }
}

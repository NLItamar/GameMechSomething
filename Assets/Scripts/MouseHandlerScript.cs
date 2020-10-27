using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandlerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /*
    private void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<FirstPersonController>() != null)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = true;
        }
    }
    */
}

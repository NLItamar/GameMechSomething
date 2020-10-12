using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JournalScript : MonoBehaviour
{
    private bool journalOpen;

    // Start is called before the first frame update
    void Start()
    {
        journalOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && journalOpen == false)
        {
            OpenJournal();
        }
        else if(Input.GetKeyDown(KeyCode.Q) && journalOpen)
        {

        }
    }

    void OpenJournal()
    {

    }
}

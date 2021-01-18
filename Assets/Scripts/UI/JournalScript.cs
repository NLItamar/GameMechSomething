using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;



public class JournalScript : MonoBehaviour
{
    private bool journalOpen;

    public GameObject spawnObject;
    public GameObject journalImage;

    public Image bookImage;

    private void Awake()
    {
        journalImage.SetActive(false);
    }

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
            CloseJournal();
        }
        //temp place for escape to main menu
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Going to main menu now!");
            SceneManager.LoadScene("MainMenu");
        }
    }

    void OpenJournal()
    {
        journalImage.SetActive(true);
        bookImage.color = Color.yellow;
        journalOpen = true;
    }

    void CloseJournal()
    {
        journalImage.SetActive(false);
        bookImage.color = Color.white;
        journalOpen = false;
    }
}

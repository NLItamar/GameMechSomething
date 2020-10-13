using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalProgression : MonoBehaviour
{
    public Text journalText;
    public Text endText;

    private string caseSwitchString;

    public string spawnText, messText, sleepQuarterText, officerQuarterText;

    public GameObject spawnObject;

    //change this to a list later on
    public GameObject SpawnInteractable;
    public GameObject MessObject;
    public GameObject SQObject;
    public GameObject OfficerQObject;

    //int caseSwitch;

    private void Start()
    {
        //caseSwitch = 0;
    }

    public void JournalAddText(string objectName)
    {
        //caseSwitch++;
        caseSwitchString = objectName;
        Debug.Log(caseSwitchString + " interacted with this object");

        //for later: change to string switch so every journal entry adds by a keyword
        switch (caseSwitchString)
        {
            case "SpawnObject":
                journalText.text = journalText.text + System.Environment.NewLine + spawnText;
                spawnObject.SetActive(false);
                //sets the next object active
                MessObject.SetActive(true);
                break;
            case "MessObject":
                journalText.text = journalText.text + System.Environment.NewLine + messText;
                SQObject.SetActive(true);
                break;
            case "SleepingQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + sleepQuarterText;
                OfficerQObject.SetActive(true);
                break;
            case "OfficerQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + officerQuarterText;
                EndLevel();
                break;
            default:
                journalText.text = "Something went wrong";
                break;
        }
    }

    public void EndLevel()
    {
        //ends the game in a sense
        //endText.gameObject.SetActive(true);
        GetComponent<EndLevelScript>().EndLevel();
    }
}

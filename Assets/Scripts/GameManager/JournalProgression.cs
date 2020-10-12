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
                break;
            case "MessObject":
                journalText.text = journalText.text + System.Environment.NewLine + messText;
                break;
            case "SleepingQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + sleepQuarterText;
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

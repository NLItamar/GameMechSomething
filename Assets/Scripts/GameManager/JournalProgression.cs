using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalProgression : MonoBehaviour
{
    public Text journalText;
    public Text endText;

    private string caseSwitchString;

    public string spawnText, messText, sleepQuarterText, officerQuarterText, FlashLightText, firstEnemyText;

    public GameObject spawnObject;

    //change this to a list later on
    public GameObject SpawnInteractable;
    public GameObject MessObject;
    public GameObject SQObject;
    public GameObject OfficerQObject;
    public GameObject MainFlashlight;

    public GameObject SQSpawnPoints;
    public GameObject OFSpawnPoints;

    public int numberOfSQPoints;
    public int numberOfOQPoints;

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
                journalText.text = journalText.text + System.Environment.NewLine + spawnText + System.Environment.NewLine;
                spawnObject.SetActive(false);
                //sets the next object active
                MessObject.SetActive(true);
                break;
            case "MessObject":
                journalText.text = journalText.text + System.Environment.NewLine + messText + System.Environment.NewLine;
                SQObject.SetActive(true);
                this.gameObject.GetComponent<StartLevelOneScript>().SpawnIntelLocationsRandom(Random.Range(0, numberOfSQPoints + 1), SQSpawnPoints, SQObject);
                break;
            case "SleepingQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + sleepQuarterText + System.Environment.NewLine;
                OfficerQObject.SetActive(true);
                break;
            case "OfficerQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + officerQuarterText + System.Environment.NewLine;
                EndLevel();
                break;
            case "MainFlashlight":
                journalText.text = journalText.text + System.Environment.NewLine + FlashLightText + System.Environment.NewLine;
                MainFlashlight.SetActive(false);
                break;
            case "FirstEnemyEncounter":
                journalText.text = journalText.text + System.Environment.NewLine + firstEnemyText + System.Environment.NewLine;
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

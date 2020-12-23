using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalProgression : MonoBehaviour
{
    //journal progression does more than just the journal, it also progresses the game itself
    //this script is quite full and needs to be cleansed at some point

    public Text journalText;
    public Text endText;

    private string caseSwitchString;

    public string spawnText, messText, sleepQuarterText, officerQuarterText, FlashLightText, firstEnemyText;

    //blockades
    public GameObject spawnObject;
    public GameObject[] messBlockades;
    public GameObject[] sqBlockades;
    public GameObject[] ofblockades;

    //change this to a list later on?
    public GameObject SpawnInteractable;
    public GameObject MessObject;
    public GameObject SQObject;
    public GameObject OfficerQObject;
    public GameObject MainFlashlight;

    public GameObject SQSpawnPoints;
    public GameObject OFSpawnPoints;

    //lists of enemies
    public GameObject[] startEnemies;
    public GameObject[] afterSpawnEnemies;
    public GameObject[] afterMessEnemies;
    public GameObject[] afterSQEnemies;
    public GameObject[] afterOFEnemies;

    //overarching enemy for hierarchy
    public GameObject startHierarchy;
    public GameObject messHierarchy;
    public GameObject sQHierarchy;
    public GameObject oFHierarchy;


    //SQ0 and OF0 doesnt exist so the the max is 'not' exclusive in int random range
    public int numberOfSQPoints;
    public int numberOfOQPoints;

    private StartLevelOneScript startLevelOneScript;

    public bool firstEncounter;

    private void Start()
    {
        firstEncounter = false;
        startLevelOneScript = this.GetComponent<StartLevelOneScript>();

        //add gameobject to the appropriate lists
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
                //enable some enemies
                //
                //sets the next object active
                MessObject.SetActive(true);
                break;
            case "MessObject":
                journalText.text = journalText.text + System.Environment.NewLine + messText + System.Environment.NewLine;
                //removes the blockades
                foreach(GameObject blockade in messBlockades)
                {
                    blockade.SetActive(false);
                }
                //enable more enemies, cleanup some shitssszzzz
                //
                //sets the next intel piece active
                SQObject.SetActive(true);
                startLevelOneScript.SpawnToRandomFromList(Random.Range(0, numberOfSQPoints), SQSpawnPoints, SQObject);
                break;
            case "SleepingQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + sleepQuarterText + System.Environment.NewLine;
                //removes the blockades
                foreach(GameObject blockades in sqBlockades)
                {
                    blockades.SetActive(false);
                }
                //enable some more enemies, cleaup some shitssszzzzz
                //
                //sets the next intel active
                OfficerQObject.SetActive(true);
                break;
            case "OfficerQuarterObject":
                journalText.text = journalText.text + System.Environment.NewLine + officerQuarterText + System.Environment.NewLine;
                //removes the blockades
                foreach(GameObject blockades in ofblockades)
                {
                    blockades.SetActive(false);
                }
                //new enemies!!! cleanup some enemies
                //
                //opens up the end level doorway
                EndLevel();
                break;
            case "MainFlashlight":
                journalText.text = journalText.text + System.Environment.NewLine + FlashLightText + System.Environment.NewLine;
                MainFlashlight.SetActive(false);
                break;
            case "FirstEnemyEncounter":
                FirstEncounter();
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

    private void FirstEncounter()
    {
        if(!firstEncounter)
        {
            journalText.text = journalText.text + System.Environment.NewLine + firstEnemyText + System.Environment.NewLine;
            firstEncounter = true;
        }
    }

    //enable enemies, can be reused for other purposes as well
    public void EnableEnemies(GameObject[] enemies)
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
}

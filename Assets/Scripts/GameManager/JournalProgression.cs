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

    //more lists, for more dynamic change of enemies
    public GameObject[] dynamicAfterSQEnemies;

    //overarching enemy for hierarchy
    public GameObject startHierarchy;
    public GameObject messHierarchy;
    public GameObject sQHierarchy;
    public GameObject oFHierarchy;

    //will get used later on
    //amount of enemies in each list
    public int startEnemyCount;
    public int afterSpawnEnemyCount;
    public int afterMessEnemyCount;
    public int afterSQEnemyCount;
    public int afterOFEnemyCount;
    //amount of enemies in each dynamic list
    public int dynamicAfterSQEnemyCount;

    //SQ0 and OF0 doesnt exist so the the max is 'not' exclusive in int random range
    public int numberOfSQPoints;
    public int numberOfOQPoints;

    private StartLevelOneScript startLevelOneScript;

    public bool firstEncounter;

    private void Start()
    {
        firstEncounter = false;
        startLevelOneScript = this.GetComponent<StartLevelOneScript>();

        //make new lists with the appriopriate count
        //startEnemies = new GameObject[startEnemyCount];
        //might continue this in code, for now in editor

        //add gameobject to the appropriate lists

    }

    public void JournalAddText(string objectName)
    {
        //caseSwitch++;
        caseSwitchString = objectName;
        Debug.Log(caseSwitchString + " interacted with this object");

        //the big ol' string case, with every object picked up something new happens and that is activated here.
        //! notice ! the case is what happens AFTER IT'S PICKED UP
        //the intel objects themselves get deactivated in another script
        //perhaps decouple most from the cases in easy reusable methods?
        switch (caseSwitchString)
        {
            case "SpawnObject":
                //add the text in the journal
                journalText.text = journalText.text + System.Environment.NewLine + spawnText + System.Environment.NewLine;
                //deactivates the spawn object
                spawnObject.SetActive(false);
                //enable some enemies
                EnableThings(afterSpawnEnemies);
                //sets the next object active
                MessObject.SetActive(true);
                break;

            case "MessObject":
                //add text to journal
                journalText.text = journalText.text + System.Environment.NewLine + messText + System.Environment.NewLine;
                //removes the blockades
                DisableFromList(messBlockades);
                //enable more enemies
                EnableThings(afterMessEnemies);
                //disable some enemies
                DisableFromList(afterSpawnEnemies);
                //enable poltergeist enemy
                //
                //sets the next intel piece active AND spawns it randomly over a few static locations
                SQObject.SetActive(true);
                startLevelOneScript.SpawnToRandomFromList(Random.Range(0, numberOfSQPoints), SQSpawnPoints, SQObject);
                break;

            case "SleepingQuarterObject":
                //add text to journal
                journalText.text = journalText.text + System.Environment.NewLine + sleepQuarterText + System.Environment.NewLine;
                //removes the blockades
                DisableFromList(sqBlockades);
                //enable some more enemies
                EnableThings(afterSQEnemies);
                //disable some enemies??
                DisableFromList(dynamicAfterSQEnemies);
                //sets the next intel active
                OfficerQObject.SetActive(true);
                break;

            case "OfficerQuarterObject":
                //add text to journal
                journalText.text = journalText.text + System.Environment.NewLine + officerQuarterText + System.Environment.NewLine;
                //removes the blockades
                DisableFromList(ofblockades);
                //new enemies
                EnableThings(afterOFEnemies);
                //disable some enemies??
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
                //incase a wrong case is called upon
                journalText.text = "Something went wrong";
                Debug.LogError("WRONG SWITCH CASE");
                break;

        }
    }

    public void EndLevel()
    {
        //ends the game in a sense
        //endText.gameObject.SetActive(true);
        GetComponent<EndLevelScript>().EndLevel();
    }

    //first enemy encounter
    private void FirstEncounter()
    {
        if(!firstEncounter)
        {
            journalText.text = journalText.text + System.Environment.NewLine + firstEnemyText + System.Environment.NewLine;
            firstEncounter = true;
        }
    }

    //enables from gameobject list, can be reused for other purposes as well
    public void EnableThings(GameObject[] things)
    {
        foreach(GameObject thing in things)
        {
            thing.SetActive(true);
        }
    }

    //disables things, can be reused 
    public void DisableFromList(GameObject[] things)
    {
        foreach(GameObject thing in things)
        {
            thing.SetActive(false);
        }
    }
}

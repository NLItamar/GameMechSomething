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

    public Image JournalImage;

    private string caseSwitchString;

    public string spawnText, messText, sleepQuarterText, officerQuarterText, FlashLightText, firstEnemyText;

    //blockades
    public GameObject[] spawnObjects;
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

    //lists and arrays of enemies
    public GameObject[] startEnemies;
    public GameObject[] afterSpawnEnemies;
    public GameObject[] afterMessEnemies;
    public GameObject[] afterSQEnemies;
    public GameObject[] afterOFEnemies;
    public List<GameObject> activeEnemies;

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
    private EndLevelScript endLevelScript;

    public bool firstEncounter;

    private void Start()
    {
        firstEncounter = false;
        startLevelOneScript = this.GetComponent<StartLevelOneScript>();
        endLevelScript = this.GetComponent<EndLevelScript>();

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
                DisableFromArray(spawnObjects);
                //enable some enemies
                EnableThingsFromArray(afterSpawnEnemies);
                //sets the next object active
                //random spawn points done in startLevelScript
                MessObject.SetActive(true);
                break;

            case "MessObject":
                //add text to journal
                journalText.text = journalText.text + System.Environment.NewLine + messText + System.Environment.NewLine;
                //removes the blockades
                DisableFromArray(messBlockades);
                //enable more enemies
                EnableThingsFromArray(afterMessEnemies);
                //disable some enemies
                DisableFromArray(afterSpawnEnemies);
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
                DisableFromArray(sqBlockades);
                //enable some more enemies
                EnableThingsFromArray(afterSQEnemies);
                //disable some enemies??
                DisableFromArray(dynamicAfterSQEnemies);
                //sets the next intel active
                OfficerQObject.SetActive(true);
                break;

            case "OfficerQuarterObject":
                //add text to journal
                journalText.text = journalText.text + System.Environment.NewLine + officerQuarterText + System.Environment.NewLine;
                //removes the blockades
                DisableFromArray(ofblockades);
                //new enemies
                EnableThingsFromArray(afterOFEnemies);
                //disable some enemies??
                //
                //opens up the end level doorway
                EndLevel();
                break;

            case "MainFlashlight":
                //add text to journal
                journalText.text = journalText.text + System.Environment.NewLine + FlashLightText + System.Environment.NewLine;
                //disables the pickup
                MainFlashlight.SetActive(false);
                break;

            case "FirstEnemyEncounter":
                FirstEncounter();
                break;

            case "GameOver":
                //add text to journal
                //purgatory code??
                Debug.Log("end it!");
                //Disable all them active enemies
                DisableFromList(activeEnemies);
                //start the countdown to the gameover scene
                StartCoroutine(GameOverRoutine());
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
            JournalImage.color = Color.green;
            firstEncounter = true;
        }
        else
        {
            Debug.Log("Already had first encounter, but what about second encounter?");
        }
    }

    //enables from gameobject array, can be reused for other purposes as well
    public void EnableThingsFromArray(GameObject[] things)
    {
        foreach(GameObject thing in things)
        {
            thing.SetActive(true);
            //add to active enemy list
            activeEnemies.Add(thing);
        }
    }

    //disables things, can be reused 
    public void DisableFromArray(GameObject[] things)
    {
        foreach(GameObject thing in things)
        {
            thing.SetActive(false);
            //remove from active enemy list
            activeEnemies.Remove(thing);
        }
    }

    public void DisableFromList(List<GameObject> things)
    {
        foreach (GameObject thing in things)
        {
            thing.SetActive(false);
        }
    }
    public void EnableThingsFromList(List<GameObject> things)
    {
        foreach (GameObject thing in things)
        {
            thing.SetActive(true);
        }
    }

    IEnumerator GameOverRoutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        //switches to the gameover screen, change this to create a purgatory!! so NOT a scene rest BUT a rearranging of it.
        activeEnemies.Clear();
        endLevelScript.GameOver("GameOver");
    }
}

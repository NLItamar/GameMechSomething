using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepDetection : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Image CreepMeter;
    [SerializeField] private GameObject gameManager;

    [HideInInspector] public float creepingDistanceMeasure;

    private bool shouldICheck;
    private bool firstEncounter;
    private bool isEnemyActive;
    private CreepManager creepManager;
    private EnemyValuesScript enemyValues;
    private ActiveBehaviourScript behaviourScript;
    private JournalProgression journalProgression;

    private void Start()
    {
        //refs
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
        creepManager = gameManager.GetComponent<CreepManager>();
        enemyValues = gameManager.GetComponent<EnemyValuesScript>();
        behaviourScript = this.gameObject.GetComponent<ActiveBehaviourScript>();
        journalProgression = gameManager.GetComponent<JournalProgression>();

        //should I check if the player is in range
        shouldICheck = true;
        firstEncounter = journalProgression.firstEncounter;

        isEnemyActive = this.gameObject.GetComponent<ActiveBehaviourScript>().isJumpyParticleEnabled;

        //makes sure the creeping distance is followed from the game manager for each different kind of enemy
        if(behaviourScript.isJumpyEnemy)
        {
            creepingDistanceMeasure = enemyValues.creepJumpyDistance;
        }
        if(behaviourScript.isNormalPatrolEnemy)
        {
            creepingDistanceMeasure = enemyValues.creepNormalDistance;
        }
    }

    void Update()
    {
        //check if the first encounter has been done, else it'll give the journal add multiple times
        firstEncounter = journalProgression.firstEncounter;

        //is the enemy active and doing it's shizzles, we know this if it has it's particles enabled and running
        //because the enemies disable this at certain points YET DO NOT disable the game object itself since they are still there
        isEnemyActive = this.gameObject.GetComponent<ActiveBehaviourScript>().isJumpyParticleEnabled;

        //distance between the player and this enemy
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);

        //clusterfuck that is the creeping of the screen with enemies closeby(gonna do this different later on!!! and moving it to CreepManager!!!)
        //puts the ui image to reddddddd so you cant see shit
        if (dist <= creepingDistanceMeasure && creepManager.creepOn == false && shouldICheck)
        {
            //does the colour thingy
            creepManager.Creeping(this.transform, creepingDistanceMeasure, isEnemyActive);

            //depending if you are in range of the first enemy, you'll get notified in the journal for it. BUT it only happens once
            if(firstEncounter == false)
            {
                creepManager.FirstEncounter();
                firstEncounter = true;
            }
            //so it wont check again
            this.shouldICheck = false;
        }

        //puts the ui image back to nothing, see through
        else if(dist > creepingDistanceMeasure && creepManager.creepOn && shouldICheck == false)
        {
            creepManager.creepOn = false;
            //simple debug to show that the creeping is off now
            Debug.Log("creep is off");

            //fully see through no filter
            CreepMeter.color = new Color(0, 0, 0, 0);

            //gotta check again ehh
            shouldICheck = true;
        }
    }
}

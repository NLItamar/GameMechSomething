using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepDetection : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject gameManager;

    [HideInInspector] public float creepingDistanceMeasure;

    [HideInInspector] public bool shouldICheck;
    private bool firstEncounter;
    private bool isEnemyActive;
    private CreepManager creepManager;
    private EnemyValuesScript enemyValues;
    private ActiveBehaviourScript behaviourScript;
    private JournalProgression journalProgression;

    private float lerpModifier;

    private void Start()
    {
        //refs
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
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
        lerpModifier = enemyValues.normalLerpModifier;
    }

    void Update()
    {
        //is the enemy active and doing it's shizzles, we know this if it has it's particles enabled and running
        //because the enemies disable this at certain points YET DO NOT disable the game object itself since they are still there
        isEnemyActive = behaviourScript.isJumpyParticleEnabled;

        //distance between the player and this enemy
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);

        //clusterfuck that is the creeping of the screen with enemies closeby(gonna do this different later on!!! and moving it to CreepManager!!!)
        //puts the ui image to reddddddd so you cant see shit
        if (dist <= creepingDistanceMeasure && creepManager.creepOn == false && shouldICheck)
        {
            //does the colour thingy
            creepManager.Creeping(this.transform, creepingDistanceMeasure, isEnemyActive, lerpModifier);

            //depending if you are in range of the first enemy, you'll get notified in the journal for it. BUT it only happens once
            if(!firstEncounter)
            {
                creepManager.FirstEncounter();
                firstEncounter = journalProgression.firstEncounter;
            }
            //so it wont check again
            this.shouldICheck = false;
        }

        //puts the ui image back to nothing, see through
        else if(dist > creepingDistanceMeasure && !shouldICheck)
        {
            creepManager.TurnCreepOff();

            //gotta check again ehh
            shouldICheck = true;
            behaviourScript.enteredRaycast = false;
        }
    }
}

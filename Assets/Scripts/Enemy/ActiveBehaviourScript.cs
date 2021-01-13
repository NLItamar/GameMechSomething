using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActiveBehaviourScript : MonoBehaviour
{
    //basically making sure that the enemy is not doing fancy things like audio and heavy particles while the player is far away from it
    //OR when the enemy can only be activated when the player sees it and is close. like a jumpscare kinda thing
    //to do for later, might increase performance especially for lower end pc's
    public bool isActived;

    private bool isFollowingPlayer;
    private bool goingToLastKnowLocation;

    //is this a jump scare enemy? or normal?
    public bool isJumpyEnemy;
    public bool isNormalPatrolEnemy;

    //are the particles enabled and running?
    public bool isJumpyParticleEnabled;

    private bool isInSight;

    private GameObject Player;
    private ParticleSystem leParticles;
    private AudioSource leAudio;

    private float distanceMeasureToDisableParticles;
    private float followDistance;

    private GameObject gameManager;
    private CreepManager creepManager;
    private CreepDetection creepDetection;
    private StartLevelOneScript startLevelOneScript;
    private EnemyValuesScript enemyValues;

    RaycastHit hit;

    private bool enteredRaycast;

    private NavMeshAgent agent;

    private Transform playerLastKnownLocation;

    private bool isCoroutineExecuting;

    private float distanceToPlayer;

    //links for the spawn to new location
    public int spawnNumber;
    public GameObject spawnPoints;

    private float lerpModifierRay;
    private float lerpModifyNormal;

    // Start is called before the first frame update
    void Start()
    {
        //points to the thingies
        Player = GameObject.FindGameObjectWithTag("Player");
        leParticles = this.gameObject.GetComponentInChildren<ParticleSystem>();
        leAudio = this.gameObject.GetComponent<AudioSource>();

        //is the player insight of the enemy? well not at start
        isInSight = false;

        //enemy is not following the player at game start or at instantiating
        isFollowingPlayer = false;
        
        //refs
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        creepManager = gameManager.GetComponent<CreepManager>();
        creepDetection = this.gameObject.GetComponent<CreepDetection>();
        startLevelOneScript = gameManager.GetComponent<StartLevelOneScript>();
        enemyValues = gameManager.GetComponent<EnemyValuesScript>();

        //just incase, because player is not in raycast at startup and the couroutine is not executing at startup
        enteredRaycast = false;
        isCoroutineExecuting = false;
        
        //check to see if the enemy is on route to the player's last known location
        goingToLastKnowLocation = false;

        agent = GetComponent<NavMeshAgent>();

        //sets the bools active if the enemy is actually enabled
        if (this.gameObject.activeSelf)
        {
            isActived = true;
            isJumpyParticleEnabled = true;
        }

        if(isJumpyEnemy)
        {
            //disable particles just incase it is active on startup
            DisableParticleStuffs();
        }

        //enemy values
        if(isJumpyEnemy)
        {
            distanceMeasureToDisableParticles = enemyValues.enemyJumpyParticlesDistance;
            followDistance = enemyValues.followJumpyDistance;
        }
        if(isNormalPatrolEnemy)
        {
            distanceMeasureToDisableParticles = enemyValues.enemyNormalParticlesDistance;
            followDistance = enemyValues.followNormalDistance;
        }

        lerpModifyNormal = enemyValues.normalLerpModifier;
        lerpModifierRay = enemyValues.rayLerpModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //make the enemy always face the player at all time, not needed tho
        //this.transform.LookAt(Player.transform);

        //distance player and this enemy, keeping it out of the if statement
        distanceToPlayer = Vector3.Distance(Player.transform.position, this.transform.position);

        //check if the enemy is a jumpscare one, distance from player and if it's emitting particles and is the player NOT insight
        if (isJumpyEnemy && distanceToPlayer > distanceMeasureToDisableParticles && isJumpyParticleEnabled && !isInSight)
        {
            DisableParticleStuffs();
        }

        //follow distance in the raycast is in this case the length of the ray in which we will check for things
        Vector3 raycastDirection = (Player.transform.position - this.transform.position).normalized;
        if(isActived && Physics.Raycast(this.transform.position, raycastDirection, out hit, followDistance))
        {
            Debug.DrawRay(this.transform.position, raycastDirection, Color.green);
            if(hit.collider.CompareTag("Player"))
            {
                creepDetection.shouldICheck = false;
                //enable the shizzles again
                if(!isJumpyParticleEnabled && isJumpyEnemy)
                {
                    EnableParticleStuffs();
                    //does the red screen thingy
                    creepManager.Creeping(this.gameObject.transform, creepDetection.creepingDistanceMeasure, isJumpyEnemy, lerpModifierRay);
                }

                //if the player is in sight and for the first time in sight it'll set the values true for following the player and a lil sauron ref
                if(!enteredRaycast)
                {
                    //follow player code
                    isFollowingPlayer = true;
                    Debug.Log("I SEEEEE YOUUUUU");
                    enteredRaycast = true;
                    goingToLastKnowLocation = false;
                }
            }
            else
            {
                enteredRaycast = false;
            }
        }

        if(isFollowingPlayer && enteredRaycast)
        {
            //as long as the enemy has the player in it's raycast it'll know it's last location until the player is out of it's raycast
            playerLastKnownLocation = Player.transform;
            FollowPlayer(playerLastKnownLocation);
        }

        if(isFollowingPlayer && !enteredRaycast)
        {
            //goes to the last know location and...
            GoToLastKnownPlayerLocation(playerLastKnownLocation);
        }

        //if the enemy is on route to the player's last known location, check if it arrives(nearly there) and set a countdown to respawn
        if(isJumpyEnemy && agent.remainingDistance <= 0.5f && goingToLastKnowLocation)
        {
            StartCoroutine(ExecuteAfterTime(5));
        }

        if(!goingToLastKnowLocation && isFollowingPlayer && isCoroutineExecuting)
        {
            StopCoroutine("ExecuteAfterTime");
        }

        else
        {
            isFollowingPlayer = false;
            enteredRaycast = false;
            goingToLastKnowLocation = false;
        }
    }

    void DisableParticleStuffs()
    {
        Debug.Log("disables " + this.gameObject);
        //stops the new particles from spawning, yet does not disable the system
        leParticles.Stop();
        //clears the current particles or else it gets funky wonky
        leParticles.Clear();
        //disables the audio, whilst not disabling the component
        leAudio.enabled = false;
        //so this method isn't called again for no reason
        isJumpyParticleEnabled = false;
    }

    void EnableParticleStuffs()
    {
        Debug.Log("enables " + this.gameObject);
        //starts the spawning of particles
        leParticles.Play();
        //plays the audio
        leAudio.enabled = true;
        //sets the is this jumpy boii active now and ready to cause some mayhem
        isJumpyParticleEnabled = true;
    }

    //follow player methods
    void FollowPlayer(Transform lastKnowLocation)
    {
        //Debug.Log("Go to player");
        agent.destination = lastKnowLocation.position;

        if(agent.autoBraking)
        {
            agent.autoBraking = false;
        }
    }

    void GoToLastKnownPlayerLocation(Transform lastKnowLocation)
    {
        isFollowingPlayer = false;

        Debug.Log("Go to last known player location");
        agent.destination = lastKnowLocation.position;

        agent.autoBraking = true;

        if (isJumpyEnemy)
        {
            goingToLastKnowLocation = true;
        }
    }

    //spawns to the other spot, uses refs that NEED TO BE SET IN THE EDITOR. or afterwards somewhere in code.
    void SpawnOtherSpot()
    {
        isCoroutineExecuting = true;
        agent.enabled = false;
        startLevelOneScript.SpawnToRandomFromList
                (Random.Range(0, spawnNumber), spawnPoints, this.gameObject);
        agent.enabled = true;
        goingToLastKnowLocation = false;
        DisableParticleStuffs();
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }
        //so this wont be displayed in console over and over
        Debug.Log("starting countdown!");
        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        //execution in the thingy after the delay
        SpawnOtherSpot();

        isCoroutineExecuting = false;
    }

    //hopefully clears the creeping when an enemy is close and disabled
    private void OnDisable()
    {
        creepManager.TurnCreepOff();
    }
}

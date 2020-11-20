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

    //is this a jump scare enemy?
    public bool isJumpyEnemy;
    public bool isJumpyParticleEnabled;
    private bool isInSight;

    [SerializeField] private GameObject Player;
    private ParticleSystem leParticles;
    private AudioSource leAudio;

    public float distanceMeasure;
    public float followDistance;

    private GameObject gameManager;
    private CreepManager creepManager;
    private CreepDetection creepDetection;
    private StartLevelOneScript startLevelOneScript;

    RaycastHit hit;

    private bool enteredRaycast;

    private NavMeshAgent agent;

    private Transform playerLastKnowLocation;

    private bool isCoroutineExecuting;

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
        
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        creepManager = gameManager.GetComponent<CreepManager>();
        creepDetection = this.gameObject.GetComponent<CreepDetection>();
        startLevelOneScript = gameManager.GetComponent<StartLevelOneScript>();

        enteredRaycast = false;
        isCoroutineExecuting = false;

        agent = GetComponent<NavMeshAgent>();

        //sets the bools active if the enemy is actually enabled
        if (this.gameObject.activeSelf)
        {
            isActived = true;
            isJumpyParticleEnabled = true;
        }

        if(isJumpyEnemy)
        {
            DisableParticleStuffs();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //make the enemy always face the player at all time
        //this.transform.LookAt(Player.transform);

        //check if the enemy is a jumpscare one, distance from player and if it's emitting particles and is the player NOT insight
        if (isJumpyEnemy && Vector3.Distance(Player.transform.position, this.transform.position) > distanceMeasure && isJumpyParticleEnabled && !isInSight)
        {
            DisableParticleStuffs();
        }

        Vector3 raycastDirection = (Player.transform.position - this.transform.position).normalized;
        if(isActived && Physics.Raycast(this.transform.position, raycastDirection, out hit, followDistance))
        {
            Debug.DrawRay(this.transform.position, raycastDirection, Color.green);
            if(hit.collider.tag == "Player")
            {
                //enable the shizzles again
                if(!isJumpyParticleEnabled && isJumpyEnemy)
                {
                    EnableParticleStuffs();
                    //does the red screen thingy
                    creepManager.Creeping(this.gameObject.transform, creepDetection.distanceMeasure, isJumpyEnemy);
                }

                if(!enteredRaycast)
                {
                    //follow player code
                    isFollowingPlayer = true;
                    Debug.Log("I SEEEEE YOUUUUU");
                    enteredRaycast = true;
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
            playerLastKnowLocation = Player.transform;
            FollowPlayer(playerLastKnowLocation);
        }
        else if(isFollowingPlayer && !enteredRaycast)
        {
            //goes to the last know location and...
            GoToLastKnowPlayerLocation(playerLastKnowLocation);
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

        agent.autoBraking = false;
    }

    void GoToLastKnowPlayerLocation(Transform lastKnowLocation)
    {
        Debug.Log("Go to last know player location");
        agent.destination = lastKnowLocation.position;

        agent.autoBraking = true;

        if (isJumpyEnemy && isFollowingPlayer && agent.remainingDistance <= 0.5f)
        {
            Debug.Log("starting countdown!");

            StartCoroutine(ExecuteAfterTime(5));
        }
    }

    //gotta make this reusable for other jumpyboiis
    void SpawnOtherSpot()
    {
        isCoroutineExecuting = true;
        agent.enabled = false;
        startLevelOneScript.SpawnToRandomFromList
                (Random.Range(0, startLevelOneScript.SQRightSpawnNumber), startLevelOneScript.SQRightSpawnPoints, startLevelOneScript.SQRightRandomEnemy);
        agent.enabled = true;
        isFollowingPlayer = false;
        DisableParticleStuffs();
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        //execution in the thingy after the delay
        SpawnOtherSpot();

        isCoroutineExecuting = false;
    }
}

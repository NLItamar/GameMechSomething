using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBehaviourScript : MonoBehaviour
{
    //basically making sure that the enemy is not doing fancy things like audio and heavy particles while the player is far away from it
    //OR when the enemy can only be activated when the player sees it and is close. like a jumpscare kinda thing
    //to do for later, might increase performance especially for lower end pc's
    public bool isActived;

    //is this a jump scare enemy?
    public bool isJumpyEnemy;
    private bool isJumpyParticleEnabled;

    [SerializeField] private GameObject Player;
    private ParticleSystem leParticles;
    private AudioSource leAudio;

    // Start is called before the first frame update
    void Start()
    {
        //points to the thingies
        Player = GameObject.FindGameObjectWithTag("Player");
        leParticles = this.gameObject.GetComponentInChildren<ParticleSystem>();
        leAudio = this.gameObject.GetComponent<AudioSource>();


        //sets the bools active if the enemy is actually enabled
        if(this.gameObject.activeSelf)
        {
            isActived = true;
            isJumpyParticleEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //make the enemy always face the player at all time
        this.transform.LookAt(Player.transform);

        //check if the enemy is a jumpscare one, distance from player and if it's emitting particles
        if (isJumpyEnemy && Vector3.Distance(Player.transform.position, this.transform.position) > 20f && isJumpyParticleEnabled)
        {
            DisableParticleStuffs();
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
}

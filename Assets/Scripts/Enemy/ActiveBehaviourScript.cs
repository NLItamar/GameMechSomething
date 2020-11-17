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

    [SerializeField] private GameObject Player;
    private ParticleSystem leParticles;
    private AudioSource leAudio;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        leParticles = Player.GetComponentInChildren<ParticleSystem>();
        leAudio = Player.GetComponent<AudioSource>();

        if(this.gameObject.activeSelf)
        {
            isActived = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //make the enemy always face the player
        this.transform.LookAt(Player.transform);

        if (isJumpyEnemy && Vector3.Distance(Player.transform.position, this.transform.position) > 20f)
        {
            leParticles.gameObject.SetActive(false);
            leAudio.enabled = false;
        }
    }
}

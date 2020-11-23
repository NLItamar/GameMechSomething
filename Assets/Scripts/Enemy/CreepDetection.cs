using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepDetection : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Image CreepMeter;

    [SerializeField] private GameObject gameManager;

    public float distanceMeasure;

    private bool shouldICheck;

    private bool firstEncounter;

    private bool isEnemyActive;

    private CreepManager creepManager;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
        creepManager = gameManager.GetComponent<CreepManager>();

        shouldICheck = true;
        firstEncounter = gameManager.GetComponent<JournalProgression>().firstEncounter;
        isEnemyActive = this.gameObject.GetComponent<ActiveBehaviourScript>().isJumpyParticleEnabled;
    }

    void Update()
    {
        isEnemyActive = this.gameObject.GetComponent<ActiveBehaviourScript>().isJumpyParticleEnabled;

        float dist = Vector3.Distance(Player.transform.position, this.transform.position);

        //clusterfuck that is the creeping of the screen with enemies closeby(gonna do this different later on!!! and moving it to CreepManager!!!)
        //puts the ui image to reddddddd so you cant see shit
        if (dist <= distanceMeasure && gameManager.GetComponent<CreepManager>().creepOn == false && shouldICheck)
        {
            //does the colour thingy
            creepManager.Creeping(this.transform, distanceMeasure, isEnemyActive);
            if(firstEncounter == false)
            {
                gameManager.GetComponent<CreepManager>().FirstEncounter();
                firstEncounter = true;
            }
            this.shouldICheck = false;
        }

        //puts the ui image back to nothing, see through
        else if(dist > distanceMeasure && gameManager.GetComponent<CreepManager>().creepOn && shouldICheck == false)
        {
            gameManager.GetComponent<CreepManager>().creepOn = false;
            Debug.Log("creep is off");

            CreepMeter.color = new Color(0, 0, 0, 0);

            shouldICheck = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepDetection : MonoBehaviour
{
    public GameObject Player;
    public Image CreepMeter;

    private GameObject gameManager;

    public float distanceMeasure;

    private bool shouldICheck;

    private bool firstEncounter;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();

        shouldICheck = true;
        firstEncounter = false;
    }

    void Update()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);

        //clusterfuck that is the creeping of the screen with enemies closeby(gonna do this different later on!!! and moving it to CreepManager!!!)
        //puts the ui image to reddddddd so you cant see shit
        if (dist <= distanceMeasure && gameManager.GetComponent<CreepManager>().creepOn == false && shouldICheck)
        {
            //does the colour thingy
            gameManager.GetComponent<CreepManager>().Creeping(this.transform, distanceMeasure);
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

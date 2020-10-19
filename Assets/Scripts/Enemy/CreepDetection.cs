using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepDetection : MonoBehaviour
{
    public GameObject Player;
    public Image CreepMeter;

    private GameObject gameManager;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
    }

    void Update()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);

        //clusterfuck that is the creeping of the screen with enemies closeby(gonna do this different later on!!! and moving it to CreepManager!!!)
        if(dist <= 30f && gameManager.GetComponent<CreepManager>().creepOn == false)
        {
            gameManager.GetComponent<CreepManager>().Creeping();
        }

        else if(dist > 30f && gameManager.GetComponent<CreepManager>().creepOn)
        {
            gameManager.GetComponent<CreepManager>().creepOn = false;
        }
    }
}

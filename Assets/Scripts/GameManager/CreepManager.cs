using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepManager : MonoBehaviour
{
    public bool creepOn;

    public GameObject Player;
    public Image CreepMeter;
    public Image JournalImage;

    private Transform enemy;
    private float distanceMeasure;

    private JournalProgression journalProgression;

    // Start is called before the first frame update
    void Start()
    {
        creepOn = false;
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
        journalProgression = this.GetComponent<JournalProgression>();
    }

    public void Creeping(Transform enemy, float distanceMeasure, bool isEnemyOnline)
    {
        Debug.Log(isEnemyOnline);

        if(isEnemyOnline)
        {
            creepOn = true;
            Debug.Log("creep is on");

            this.enemy = enemy;
            this.distanceMeasure = distanceMeasure;
        }
    }

    private void Update()
    {
        if(creepOn)
        {
            CreepMeter.color = Color.Lerp(Color.red, Color.clear, Vector3.Distance(Player.transform.position, enemy.position) / distanceMeasure);
        }
    }

    public void FirstEncounter()
    {
        //double check
        if(!journalProgression.firstEncounter)
        {
            this.GetComponent<JournalProgression>().JournalAddText("FirstEnemyEncounter");
            JournalImage.color = Color.green;
        }
        else
        {
            Debug.Log("Already had first encounter");
        }
    }

    public void TurnCreepOff()
    {
        creepOn = false;
        CreepMeter.color = Color.clear;
    }
}

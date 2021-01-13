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

    private float lerpMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        creepOn = false;
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
        journalProgression = this.GetComponent<JournalProgression>();
    }

    public void Creeping(Transform enemy, float distanceMeasure, bool isEnemyOnline, float lerpModify)
    {
        Debug.Log(isEnemyOnline);

        lerpMultiplier = lerpModify;

        if(isEnemyOnline)
        {
            this.enemy = enemy;
            this.distanceMeasure = distanceMeasure / lerpModify;
            Debug.Log("distanceMeasure: " + distanceMeasure + " " +
                "lerpModifier: " + lerpModify +
                "creep starts at distance: " + this.distanceMeasure
                );

            creepOn = true;
            Debug.Log("creep is on");
        }
    }

    private void Update()
    {
        if(creepOn)
        {
            CreepMeter.color = Color.Lerp(Color.red, Color.clear, Vector3.Distance(Player.transform.position, enemy.position) / distanceMeasure);
            //Debug.Log(Vector3.Distance(Player.transform.position, enemy.position));
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
        if(CreepMeter != null)
        {
            Debug.Log("Creep is now off");
            creepOn = false;
            CreepMeter.color = Color.clear;
        }
        else
        {
            Debug.LogWarning("creepMeter image is NULL, can ignore for now");
        }
    }
}

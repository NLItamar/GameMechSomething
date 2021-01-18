using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepManager : MonoBehaviour
{
    public bool creepOn;

    public GameObject Player;
    public Image CreepMeter;

    private Transform enemy;
    private float distanceMeasure;

    private JournalProgression journalProgression;

    private float lerpMultiplier;

    private void Awake()
    {
        journalProgression = this.GetComponent<JournalProgression>();
        Player = GameObject.FindGameObjectWithTag("Player");
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        creepOn = false;
    }

    public void Creeping(Transform enemy, float distanceMeasure, bool isEnemyOnline, float lerpModify)
    {
        lerpMultiplier = lerpModify;

        if(isEnemyOnline)
        {
            this.enemy = enemy;
            this.distanceMeasure = distanceMeasure / lerpModify;
            Debug.Log("distanceMeasure: " + distanceMeasure + " " +
                "lerpModifier: " + lerpModify + " " +
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
        journalProgression.JournalAddText("FirstEnemyEncounter");
    }

    public void TurnCreepOff(string thisObject)
    {
        Debug.Log("TurnCreepOff is called by: " + thisObject);

        if(CreepMeter != null && creepOn)
        {
            creepOn = false;
            CreepMeter.color = Color.clear;
            Debug.Log("Creep is now off");
        }

        if(CreepMeter == null)
        {
            //something about closing the game whilst in editor or switching scenes
            Debug.LogWarning("creepMeter image is NULL, can ignore for now");
            //CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
        }

        else
        {
            Debug.Log("Creep off is called but the if's are not passed");
        }
    }
}

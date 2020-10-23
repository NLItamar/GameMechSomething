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


    // Start is called before the first frame update
    void Start()
    {
        creepOn = false;
        CreepMeter = GameObject.FindGameObjectWithTag("CreepDetection").GetComponent<Image>();
    }

    public void Creeping(Transform enemy, float distanceMeasure)
    {
        creepOn = true;
        Debug.Log("creep is on");

        this.enemy = enemy;
        this.distanceMeasure = distanceMeasure;
    }

    private void Update()
    {
        CreepMeter.color = Color.Lerp(Color.red, Color.clear, Vector3.Distance(Player.transform.position, enemy.position) / distanceMeasure);
    }
}

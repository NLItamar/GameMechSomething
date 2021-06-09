using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivationScript : MonoBehaviour
{
    private GameObject player;
    private GameObject lights;
    public float jumpyDistance;

    public bool isJumpyLight;
    public bool isOff;
    public bool isAlwaysOn;

    public float minIntensityOne;
    public float maxIntensityOne;
    public float normalIntensityOne;
    public int maxCheckOne;

    public float minIntensityTwo;
    public float maxIntensityTwo;
    public float normalIntensityTwo;
    public int maxCheckTwo;

    //bubble distance
    public float bubbleDistance;

    //intensity when the player approaches first
    public float jumpyLightIntensity;
    [HideInInspector] public bool firstEncounter;

    //private LightBehaviourStaticScript lightBehaviourStaticScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lights = transform.Find("Lights").gameObject;

        //lightBehaviourStaticScript = this.gameObject.GetComponentInChildren<LightBehaviourStaticScript>();
        firstEncounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        BubbleChecker();

        if(isJumpyLight)
        {
            CheckJumpyLightDistance();
        }
    }

    private void CheckJumpyLightDistance()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= jumpyDistance && lights.activeInHierarchy == false)
        {
            lights.SetActive(true);
            firstEncounter = true;
        }

        else if (Vector3.Distance(this.transform.position, player.transform.position) > jumpyDistance && lights.activeInHierarchy == true)
        {
            lights.SetActive(false);
            firstEncounter = false;
        }
    }

    private void BubbleChecker()
    {
        //bubble distance to turn off every light source, to save performance
        if(Vector3.Distance(this.transform.position, player.transform.position) >= bubbleDistance && lights.activeInHierarchy == true)
        {
            lights.SetActive(false);
        }
        //turn it on if its in the bubble
        else if(Vector3.Distance(this.transform.position, player.transform.position) < bubbleDistance && lights.activeInHierarchy == false)
        {
            lights.SetActive(true);
        }
    }
}

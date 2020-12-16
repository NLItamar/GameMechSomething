using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivationScript : MonoBehaviour
{
    private GameObject player;
    private GameObject lights;
    public float distance;

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lights = transform.Find("Lights").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isJumpyLight)
        {
            CheckJumpyLightDistance();
        }
    }

    private void CheckJumpyLightDistance()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= distance && lights.activeInHierarchy == false)
        {
            lights.SetActive(true);
        }

        else if (Vector3.Distance(this.transform.position, player.transform.position) > distance && lights.activeInHierarchy == true)
        {
            lights.SetActive(false);
        }
    }
}

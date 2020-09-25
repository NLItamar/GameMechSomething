using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviourStaticScript : MonoBehaviour
{
    Light myLight;

    Material m_Material;

    public float minIntensity;
    public float maxIntensity;
    private float setIntensity;

    public float maxCheck;
    private float randomTime;
    private float randomTimeChecker;

    private bool isOn;

    void Start()
    {
        myLight = GetComponent<Light>();
        randomTime = Random.Range(1f, maxCheck);
        randomTimeChecker = maxCheck - 1f;
        Debug.Log(randomTime);
        Debug.Log(randomTimeChecker);

        isOn = true;

        setIntensity = maxIntensity;

        m_Material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if(isOn)
        {

        }
        FlickerLight();
    }

    public void FlickerLight()
    {
        //turns the light 'off'
        if (randomTime >= randomTimeChecker)
        {
            myLight.intensity = minIntensity;
            m_Material.DisableKeyword("_EMISSION");
        }

        //turns the light 'on'
        else if (randomTime <= randomTimeChecker)
        {
            setIntensity = Random.Range(minIntensity, maxIntensity);
            Invoke("LightOn", 0.5f);
        }
    }

    private void LightOn()
    {
        myLight.intensity = maxIntensity;
        m_Material.EnableKeyword("_EMISSION");

        randomTime = Random.Range(1f, maxCheck);
    }

}

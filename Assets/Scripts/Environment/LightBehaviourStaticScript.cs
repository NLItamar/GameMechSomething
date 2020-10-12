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
    public float normalIntensity;

    public float maxCheck;
    private float randomTime;
    private float randomTimeChecker;

    private bool isOn;

    void Start()
    {
        myLight = GetComponent<Light>();
        randomTime = Random.Range(1f, maxCheck);
        randomTimeChecker = maxCheck - 1f;

        isOn = true;

        setIntensity = maxIntensity;

        m_Material = GetComponent<Renderer>().material;

        isOn = true;
    }

    void Update()
    {
        //gets a new random for the next 'loop'
        randomTime = Random.Range(1f, maxCheck);

        if(isOn && randomTime >= randomTimeChecker)
        {
            FlickerTheLight();
        }
        //FlickerLight();
    }

    public void FlickerTheLight()
    {
        myLight.intensity = minIntensity;
        m_Material.DisableKeyword("_EMISSION");
        isOn = false;

        //turns the light back on after x amount of time
        Invoke("FlickerBackOn", 0.2f);
    }

    public void FlickerBackOn()
    {
        //gets a new random intensity to shine bright for a bit
        setIntensity = Random.Range(normalIntensity, maxIntensity);
        myLight.intensity = setIntensity;

        //turns the emission on the material BACK ON
        m_Material.EnableKeyword("_EMISSION");
        m_Material.SetColor("_EmmisionColor", Color.white * 1f);

        //puts the light back to normal
        Invoke("LightNormal", 0.2f);
    }

    public void LightNormal()
    {
        myLight.intensity = normalIntensity;
        m_Material.SetColor("_EmmisionColor", Color.white * 0.5f);

        isOn = true;
    }

    /*
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
    */
}

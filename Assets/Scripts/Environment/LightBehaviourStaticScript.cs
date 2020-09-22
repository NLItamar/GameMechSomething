using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviourStaticScript : MonoBehaviour
{
    Light myLight;

    Material m_Material;

    public float minIntensity = 0.3f;
    public float maxIntensity = 2f;

    public float maxCheck;
    private float randomTime;
    private float randomTimeChecker;

    void Start()
    {
        myLight = GetComponent<Light>();
        randomTime = Random.Range(1f, maxCheck);
        randomTimeChecker = maxCheck - 1f;
        Debug.Log(randomTime);
        Debug.Log(randomTimeChecker);

        //InvokeRepeating("FlickerLight", 0f, 0.1f);

        m_Material = GetComponent<Renderer>().material;

    }

    void Update()
    {
        //turns the light off
        if (randomTime >= randomTimeChecker)
        {
            myLight.intensity = minIntensity;
            m_Material.DisableKeyword("_EMISSION");
        }

        //turns the light on
        if (randomTime <= randomTimeChecker)
        {
            myLight.intensity = maxIntensity;
            m_Material.EnableKeyword("_EMISSION");
        }

        randomTime = Random.Range(1f, maxCheck);
    }

    private void FlickerLight()
    {

    }

}

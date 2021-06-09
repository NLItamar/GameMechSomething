using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviourStaticScript : MonoBehaviour
{
    Light myLight;

    Material m_Material;

    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    private float setIntensity;
    [SerializeField] private float normalIntensity;

    [SerializeField] private float maxCheck;
    [SerializeField] private float firstEncounterIntensity;
    [HideInInspector] public bool firstEncounter;
    private float randomTime;
    private float randomTimeChecker;

    private bool isOn;
    private bool isOff;
    private bool isAlwaysOn;

    private LightActivationScript lightActivationScript;

    private bool givenWarning;
    private bool isJumpyLight;

    private void Awake()
    {
        myLight = GetComponent<Light>();
        randomTime = Random.Range(1f, maxCheck);
        randomTimeChecker = maxCheck - 1f;
        lightActivationScript = this.GetComponentInParent<LightActivationScript>();

        setIntensity = maxIntensity;

        m_Material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        //should this light be turned on? also a checker further on in the code
        isOn = true;

        isOff = lightActivationScript.isOff;
        isAlwaysOn = lightActivationScript.isAlwaysOn;

        //values from the parent depending on what light it is through a tag
        if(gameObject.CompareTag("LightOne"))
        {
            minIntensity = lightActivationScript.minIntensityOne;
            maxIntensity = lightActivationScript.maxIntensityOne;
            normalIntensity = lightActivationScript.normalIntensityOne;
            maxCheck = lightActivationScript.maxCheckOne;
        }
        else if(gameObject.CompareTag("LightTwo"))
        {
            minIntensity = lightActivationScript.minIntensityTwo;
            maxIntensity = lightActivationScript.maxIntensityTwo;
            normalIntensity = lightActivationScript.normalIntensityTwo;
            maxCheck = lightActivationScript.maxCheckTwo;
        }
        firstEncounterIntensity = lightActivationScript.jumpyLightIntensity;
        firstEncounter = lightActivationScript.firstEncounter;

        isJumpyLight = lightActivationScript.isJumpyLight;

        givenWarning = false;
    }

    void Update()
    {
        //gets a new random for the next 'loop'
        randomTime = Random.Range(1f, maxCheck);

        //long if else ifffff
        //lots of checks!!
        //perhaps change it to a switch case, buttt it works now.
        //also a check if a warning is given, just incase a light is both checked as off and always on

        //check if the light should be on, if the randomtime is higher or the same as the checker, if it's not an always off light and if it's not an always on light. then flickers it
        if(isOn && randomTime >= randomTimeChecker && !isOff && !isAlwaysOn)
        {
            FlickerTheLight();
            givenWarning = false;
        }
        //check if the light should be off and check if it is not already turned off
        else if(isOff && myLight.intensity > 0f && !isAlwaysOn)
        {
            LightOff();
            givenWarning = false;
        }
        //checks if the light is an always on light, if its not already on and if its not an always off light
        else if(isAlwaysOn && !isOn && !isOff)
        {
            LightNormal();
            givenWarning = false;
        }
        //checks for double bools, gives warning if so
        else if(isAlwaysOn && isOff && !givenWarning)
        {
            isOn = false;
            Debug.LogWarning("Warning: isAlways on and isOff cannot be true at the same time!! at object: " + this.gameObject);
            givenWarning = true;
        }
        //keep checking this value so it can be changed during runtime
        isOff = lightActivationScript.isOff;
        isAlwaysOn = lightActivationScript.isAlwaysOn;
    }

    public void FlickerTheLight()
    {
        myLight.intensity = minIntensity;
        m_Material.DisableKeyword("_EMISSION");
        isOn = false;

        //turns the light back on after x amount of time
        Invoke(nameof(FlickerBackOn), 0.2f);
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
        Invoke(nameof(LightNormal), 0.2f);
    }

    public void LightNormal()
    {
        isOn = false;

        myLight.intensity = normalIntensity;
        m_Material.SetColor("_EmmisionColor", Color.white * 0.5f);

        isOn = true;
    }

    public void LightOff()
    {
        myLight.intensity = 0f;
        m_Material.DisableKeyword("_EMISSION");
    }

    //cheesy way of doing this
    private void OnEnable()
    {
        if(!firstEncounter && isJumpyLight && lightActivationScript.activatedNow)
        {
            Debug.Log("light in " + this.transform.parent.parent.parent.name + " just went heckin nuts!!");
            myLight.intensity = firstEncounterIntensity;
            firstEncounter = true;

            //turns the emission on the material BACK ON
            m_Material.EnableKeyword("_EMISSION");
            m_Material.SetColor("_EmmisionColor", Color.white * 1f);

            //puts the light back to normal, after a really quick moment
            Invoke(nameof(LightNormal), 0.1f);
        }
    }

    private void OnDisable()
    {
        firstEncounter = false;
    }
}

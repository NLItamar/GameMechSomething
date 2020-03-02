using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelOneScript : MonoBehaviour
{
    public GameObject RoofObject;
    public Text StartText;

    // Start is called before the first frame update
    void Start()
    {
        StartText.GetComponent<TextFadeOut>().FadeOut();

        //set active method
        SetObjectsActive();
    }

    void SetObjectsActive()
    {
        //set the roof active
        RoofObject.SetActive(true);

        //set the start text active
        StartText.gameObject.SetActive(true);
    }
}

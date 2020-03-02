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
        //setting the roof active
        RoofObject.SetActive(true);

        StartText.GetComponent<TextFadeOut>().FadeOut();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelOneScript : MonoBehaviour
{
    public GameObject RoofObject;

    // Start is called before the first frame update
    void Start()
    {
        RoofObject.SetActive(true);
    }
}

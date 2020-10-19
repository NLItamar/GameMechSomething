using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepManager : MonoBehaviour
{
    public bool creepOn;

    // Start is called before the first frame update
    void Start()
    {
        creepOn = false;
    }

    public void Creeping()
    {
        creepOn = true;

    }
}

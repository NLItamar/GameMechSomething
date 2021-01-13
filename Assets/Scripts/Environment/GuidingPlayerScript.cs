using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingPlayerScript : MonoBehaviour
{
    public GameObject spawnToMessBlockade;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player goes from  main hall to mess/audit, enabling " + other.tag);
            spawnToMessBlockade.SetActive(true);
            //so it wont enable this door again
            this.gameObject.SetActive(false);
        }
    }
}

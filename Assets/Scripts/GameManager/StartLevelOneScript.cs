using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelOneScript : MonoBehaviour
{
    public GameObject RoofObject;
    public Text StartText;

    public GameObject MessObject;

    public GameObject MessSpawnPoints;

    private List<GameObject> childrenList;

    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        StartText.GetComponent<TextFadeOut>().FadeOut();

        //set active method
        SetObjectsActive();

        //spawns the intels randomly
        randomNumber = Random.Range(0, 5);
        SpawnIntelLocationsRandom(randomNumber, MessSpawnPoints, MessObject);
    }

    void SetObjectsActive()
    {
        //set the roof active
        RoofObject.SetActive(true);

        //set the start text active
        StartText.gameObject.SetActive(true);
    }


    //reusable method that moves an interactable object to another location
    //needs a random number as much as the locations there are, which are the transform positions of the empty gameobjects
    //needs the parent of those empty gameobjects and the interactable object itself
    public void SpawnIntelLocationsRandom(int randomSeed, GameObject randomSpawnPoints, GameObject toMoveObject)
    {
        childrenList = new List<GameObject>();

        foreach(Transform child in randomSpawnPoints.transform)
        {
            childrenList.Add(child.gameObject);
        }
        //gives the location of the spot it'll move it to
        Debug.Log(childrenList[randomSeed].transform.position);

        //moves the interact object
        toMoveObject.transform.position = childrenList[randomSeed].transform.position;

        //clears the list for next object
        childrenList.Clear();
    }
}

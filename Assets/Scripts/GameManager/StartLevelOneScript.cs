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

    public int SQRightSpawnNumber;

    public GameObject SQRightRandomEnemy;
    public GameObject SQRightSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartText.GetComponent<TextFadeOut>().FadeOut();

        //set active method for roof and start text
        SetObjectsActive();

        //spawns the intels randomly, only the mess object since that one spawns in one room and not dynamic over a lot of points
        randomNumber = Random.Range(0, 5);
        SpawnToRandomFromList(randomNumber, MessSpawnPoints, MessObject);

        //spawns the random spawn enemies that will activate when the player is near and in sight, like a jumpscare enemy
        if (SQRightRandomEnemy.activeSelf == false)
        {
            //reusing the spawn intel locations method because it does the same.
            SpawnToRandomFromList(Random.Range(0, SQRightSpawnNumber), SQRightSpawnPoints, SQRightRandomEnemy);
        }
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
    public void SpawnToRandomFromList(int randomSeed, GameObject randomSpawnPoints, GameObject toMoveObject)
    {
        childrenList = new List<GameObject>();

        foreach(Transform child in randomSpawnPoints.transform)
        {
            childrenList.Add(child.gameObject);
        }
        //gives the location of the spot it'll move it to
        Debug.Log(childrenList[randomSeed].transform.position);

        //moves the object
        toMoveObject.transform.position = childrenList[randomSeed].transform.position;
        //rotates it to the appropriate rotation
        Vector3 eulerRotation = new Vector3(
            childrenList[randomSeed].transform.eulerAngles.x, 
            childrenList[randomSeed].transform.eulerAngles.y, 
            childrenList[randomSeed].transform.eulerAngles.z);

        toMoveObject.transform.rotation = Quaternion.Euler(eulerRotation);

        //clears the list for next object
        childrenList.Clear();

        //sets the gameobject active if it's not already
        if (toMoveObject.activeSelf == false)
        {
            toMoveObject.SetActive(true);
        }

        Debug.Log("Spawned " + toMoveObject.name + " at: " + toMoveObject.transform.position + ", go figure where that is");
    }
}

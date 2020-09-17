using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepDetection : MonoBehaviour
{
    public Image CreepMeter;

    private GameObject raycastedObject;

    public GameObject Player;

    public GameObject gameManager;

    //distance of interacting
    [SerializeField] private readonly int rayLength = 10;

    [SerializeField] private LayerMask layerMaskInteract;

    public bool raycastHit;

    private void Start()
    {
        //CreepMeter = 
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractableObject"))
            {
                
            }
        }
    }
}

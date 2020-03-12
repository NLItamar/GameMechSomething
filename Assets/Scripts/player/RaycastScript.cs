using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastScript : MonoBehaviour
{
    private GameObject raycastedObject;

    [SerializeField] private int rayLength = 4;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image UICrosshair;

    //private bool raycastHit = false;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("InteractableObject"))
            {
                raycastedObject = hit.collider.gameObject;
                //change the crosshair
                CrosshairActive();

                if(Input.GetKeyDown("e"))
                {
                    Debug.Log("I have interacted");
                    raycastedObject.SetActive(false);
                }
            }
        }
        else
        {
            //crosshair to normal
            CrosshairNormal();
        }
    }

    void CrosshairActive()
    {
        UICrosshair.color = Color.green;
    }

    void CrosshairNormal()
    {
        UICrosshair.color = Color.white;
    }
}

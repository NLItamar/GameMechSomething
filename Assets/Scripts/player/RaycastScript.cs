using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastScript : MonoBehaviour
{
    private GameObject raycastedObject;

    public GameObject gameManager;

    //distance of interacting
    [SerializeField] private readonly int rayLength = 4;

    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image UICrosshair;

    private Text tooltipText;
    private bool tooltipTextActive;

    public bool raycastHit;

    private void Start()
    {
        tooltipTextActive = false;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("InteractableObject"))
            {
                raycastedObject = hit.collider.gameObject;
                //text object on the interactable object
                tooltipText = raycastedObject.gameObject.GetComponentInChildren<Text>(true);
                SetToolTipOn();

                raycastHit = true;

                //change the crosshair
                CrosshairActive();

                //interacting with the object
                if(Input.GetKeyDown("e"))
                {
                    Debug.Log("I have interacted");
                    raycastedObject.SetActive(false);
                    gameManager.GetComponent<JournalProgression>().JournalAddText(raycastedObject.name);
                }
            }
        }
        else
        {
            //crosshair to normal when the ray hits nothaaaaaanggggg
            raycastHit = false;
            CrosshairNormal();
            SetToolTipOff();
        }
    }

    //might want to use a sprite for this
    void CrosshairActive()
    {
        UICrosshair.color = Color.green;
    }

    void CrosshairNormal()
    {
        UICrosshair.color = Color.white;
        if(tooltipTextActive)
        {
            tooltipText.gameObject.SetActive(false);
        }
    }

    void SetToolTipOn()
    {
        if(tooltipTextActive == false)
        {
            tooltipText.gameObject.SetActive(true);
            tooltipTextActive = true;
        }
    }

    void SetToolTipOff()
    {
        if (tooltipTextActive)
        {
            tooltipText.gameObject.SetActive(false);
            tooltipTextActive = false;
        }
    }
}

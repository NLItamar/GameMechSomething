using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public bool IsTorchOn;
    public Light Torch;

    void Start()
    {
        IsTorchOn = false;
        Torch.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TorchUse();
        }
    }

    void TorchUse()
    {
        if(IsTorchOn == false && Torch.gameObject.activeSelf == false)
        {
            IsTorchOn = true;
            Torch.gameObject.SetActive(true);
        }
        else if(IsTorchOn == true && Torch.gameObject.activeSelf == true)
        {
            IsTorchOn = false;
            Torch.gameObject.SetActive(false);
        }
    }
}

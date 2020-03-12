using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchController : MonoBehaviour
{
    public bool IsTorchOn;
    public Light Torch;
    public Image FlashLightImage;

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
        //turn it on
        if(IsTorchOn == false && Torch.gameObject.activeSelf == false)
        {
            IsTorchOn = true;
            //colour in the HUD
            FlashLightImage.color = Color.yellow;
            Torch.gameObject.SetActive(true);
        }
        //turn it off
        else if(IsTorchOn == true && Torch.gameObject.activeSelf == true)
        {
            IsTorchOn = false;
            //colour off in the HUD
            FlashLightImage.color = Color.white;
            Torch.gameObject.SetActive(false);
        }
    }
}

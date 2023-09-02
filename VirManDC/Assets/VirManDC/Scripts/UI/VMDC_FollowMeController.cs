using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMDC_FollowMeController : MonoBehaviour
{
    public KeepRelativePositionToCamera keepRelativePositionToCameraScript;
    public KeepPositionAndRotationController keepPositionAndRotationController;


    void Start()
    {
        //keepRelativePositionToCameraScript.SwapState();
        //positionControllerGameObject.SetActive(true);
    }

    /// <summary>
    /// To be called from button
    /// </summary>
    public void SwapPositionController(){
        keepRelativePositionToCameraScript.SwapState();
        keepPositionAndRotationController.SwapState();
    }
}

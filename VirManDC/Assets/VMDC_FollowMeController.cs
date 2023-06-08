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

    public void SwapPositionController(){
        keepRelativePositionToCameraScript.SwapState();
        //positionControllerGameObject.SetActive(!positionControllerGameObject.activeSelf);
        keepPositionAndRotationController.SwapState();
    }
}

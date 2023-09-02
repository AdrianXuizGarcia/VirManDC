using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private Animator selectRackGraphicAnimator;
    [SerializeField]
    private Animator selectRackUnitsAnimator;
    [SerializeField]
    private GameObject openDoorButton;
    [SerializeField]
    private GameObject closeDoorButton;

    public void OnOpenDoor()
    {
        // Trigger animation
        doorAnimator.SetBool("OpenDoor",true);
        selectRackGraphicAnimator.SetBool("RackIsSelected",true);
        selectRackUnitsAnimator.SetBool("RackIsSelected",true);
        openDoorButton.SetActive(false);
        closeDoorButton.SetActive(true);
    }

    public void OnCloseDoor(){
        doorAnimator.SetBool("OpenDoor",false);
        selectRackGraphicAnimator.SetBool("RackIsSelected",false);
        selectRackUnitsAnimator.SetBool("RackIsSelected",false);
        openDoorButton.SetActive(true);
        closeDoorButton.SetActive(false);
    }


}

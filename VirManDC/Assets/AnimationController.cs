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

    public void OnOpenDoor()
    {
        // Trigger animation
        doorAnimator.SetTrigger("OpenDoor");
        selectRackGraphicAnimator.SetTrigger("RackIsSelected");
        selectRackUnitsAnimator.SetTrigger("RackIsSelected");
    }


}

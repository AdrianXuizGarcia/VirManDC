using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class FrontSlotController : MonoBehaviour
{
    [SerializeField]
    private SlotControl referenceSlotControl;

    [SerializeField]
    private WarningController warningController;
    [SerializeField]
    private MainDataController mainDataController;
    [SerializeField]
    private ObjectManipulator objectManipulator;
    public GameObject resetPositionButton;
    public ServerMenuBehaviourController serverMenuController;
    public AudioSource audioSource;

    void Start(){
        resetPositionButton.SetActive(false);
    }
    /// <summary>
    /// To be called from Front Button Controller
    /// </summary>
    public void InteractWithSlot(){
        if (!referenceSlotControl.slotIsDeactivated)
        {
            if (!StaticDataHolder.architectureMode)
            {
                warningController.NewPetitionForWarningData();
                mainDataController.NewPetitionForMainData();
            }
            serverMenuController.SwapStateMenu();
            audioSource.PlayOneShot(audioSource.clip);
        }
        objectManipulator.enabled = true;
        resetPositionButton.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class FrontSlotController : MonoBehaviour
{
    [SerializeField]
    private GameObject basicMenuGUI;
    [SerializeField]
    private SlotData slotData;

    [SerializeField]
    private WarningController warningController;
    [SerializeField]
    private MainDataController mainDataController;
    [SerializeField]
    private ObjectManipulator objectManipulator;
    public GameObject resetPositionButton;

    //TODO
    private ListOfComponentsGUI listOfComponentsGUI;

    void Start(){
        basicMenuGUI.SetActive(false);
        listOfComponentsGUI = basicMenuGUI.GetComponentInChildren<ListOfComponentsGUI>();
        listOfComponentsGUI.changeSlotName(slotData.slotName);
        resetPositionButton.SetActive(false);
    }

    public void OpenSlot(){
        if(!StaticDataHolder.architectureMode){
            warningController.NewPetitionForWarningData();
            mainDataController.NewPetitionForMainData();
        }
        basicMenuGUI.SetActive(true);
        objectManipulator.enabled = true;
        resetPositionButton.SetActive(true);
    }

    public void ChangeMenuState(bool state){
        // TODO: Fix interaction with close menu button
        //basicMenuGUI.SetActive(!basicMenuGUI.activeSelf);
        basicMenuGUI.SetActive(state);
        //listOfComponentsGUI.changeSlotName(slotData.slotName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSlotController : MonoBehaviour
{
    [SerializeField]
    private GameObject basicMenuGUI;
    [SerializeField]
    private SlotData slotData;

    private ListOfComponentsGUI listOfComponentsGUI;

    void Start(){
        basicMenuGUI.SetActive(false);
        listOfComponentsGUI = basicMenuGUI.GetComponentInChildren<ListOfComponentsGUI>();
    }

    public void ChangeMenuState(){
        // TODO: Fix interaction with close menu button
        //basicMenuGUI.SetActive(!basicMenuGUI.activeSelf);
        basicMenuGUI.SetActive(true);
        listOfComponentsGUI.changeSlotName(slotData.slotName);
    }
}

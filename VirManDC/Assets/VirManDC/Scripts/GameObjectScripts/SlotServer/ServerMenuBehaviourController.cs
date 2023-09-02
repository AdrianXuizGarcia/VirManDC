using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMenuBehaviourController : MonoBehaviour
{
    public GameObject serverMenu;
    public DrawAuxiliarLinesController drawAuxiliarLinesController;

    [SerializeField]
    private SlotData referenceSlotData;
    private ListOfComponentsGUI listOfComponentsGUI;

    void Start(){
        if(serverMenu){
            serverMenu.SetActive(false);
            listOfComponentsGUI = serverMenu.GetComponentInChildren<ListOfComponentsGUI>();
            listOfComponentsGUI.changeSlotName(referenceSlotData.slotName);
        }      
    }

    public void SwapStateMenu(){
        serverMenu.SetActive(!serverMenu.activeSelf);
        //drawAuxiliarLinesController.ChangeState(false);
    }
}

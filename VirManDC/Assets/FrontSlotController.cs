using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSlotController : MonoBehaviour
{
    [SerializeField]
    private GameObject basicMenuGUI;
    [SerializeField]
    private ArchitectureSlotData architectureSlotData;

    private ListOfComponentsGUI listOfComponentsGUI;

    void Start(){
        basicMenuGUI.SetActive(false);
        listOfComponentsGUI = basicMenuGUI.GetComponentInChildren<ListOfComponentsGUI>();
    }

    public void ChangeMenuState(){
        basicMenuGUI.SetActive(!basicMenuGUI.activeSelf);
        listOfComponentsGUI.changeSlotName(architectureSlotData.slotName);
    }
}

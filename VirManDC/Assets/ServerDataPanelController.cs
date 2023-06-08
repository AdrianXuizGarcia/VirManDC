using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServerDataPanelController : MonoBehaviour
{
    private SlotData slotDataReference;
    public TextMeshProUGUI serverDataText;
    
    //public GameObject openPanelButton;
    //public GameObject openPanelButtonIcon;
    private bool panelIsOpen = false;

    public GameObject serverDataPanel;

    private void Start() {
        // Initialization
        //openPanelButton.SetActive(false);
        // To get the reference to the data of the slot
        AsignReferenceSlotData();
        //SetButton_ReadyToOpen();
        serverDataPanel.SetActive(panelIsOpen);
    }

    /// <summary>
    /// Executed first on BehaviourSlotController. This script should be executed only
    /// when all data is readed
    /// </summary>
    /// <param name="referenceSlotData"></param>
    public void AsignReferenceSlotData(){
        //TODO: Esperar a que la información esté totalmente leída, bloquear botón mientras tanto,
        //TODO: quizas desde un script externo controlar todos los botones.
        //slotData = referenceSlotData;
        //
        slotDataReference = this.GetComponentInParent<SlotComponentsReferences>().GetSlotDataReference();
    }

    public void UpdateServerDataPanel()
    {
        serverDataText.text = "Hostname: " + slotDataReference.hostname;
        serverDataText.text += "<br>IP: " + slotDataReference.ip;
        serverDataText.text += "<br>Host ID: " + slotDataReference.hostID;
        serverDataText.text += "<br>Host: " + slotDataReference.host;
        serverDataText.text += "<br>Description: " + slotDataReference.descriptionHost;
    }

    public void OpenServerDataPanel(){
        //SetButton_ReadyToClose();
        serverDataPanel.SetActive(true);
    }

    /*private void SetButton_ReadyToOpen(){
        //openPanelButtonIcon.transform.Rotate(0f, 0f, 0f);
        openPanelButtonIcon.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    private void SetButton_ReadyToClose(){
        //openPanelButtonIcon.transform.Rotate(0f, 0f, 180f);
        openPanelButtonIcon.transform.eulerAngles = new Vector3(0f, 0f, 180f);
    }*/

    public void SwapServerDataPanelState(){
        serverDataPanel.SetActive(!panelIsOpen);
        /*if (panelIsOpen)
            SetButton_ReadyToClose();
        else 
            SetButton_ReadyToOpen();*/
        panelIsOpen = !panelIsOpen;
    }
}

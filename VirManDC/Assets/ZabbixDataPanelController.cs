using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZabbixDataPanelController : MonoBehaviour
{
    private SlotData slotData;
    public TextMeshProUGUI warningText;

    public void AsignSlotData(SlotData referenceSlotData){
        slotData = referenceSlotData;
    }

    public void UpdateZabbixPanelData()
    {
        warningText.text = "Hostname: " + slotData.hostname;
        warningText.text += "<br>IP: " + slotData.ip;
        warningText.text += "<br>Host ID: " + slotData.hostID;
        warningText.text += "<br>Host: " + slotData.host;
        warningText.text += "<br>Description: " + slotData.descriptionHost;
    }
}

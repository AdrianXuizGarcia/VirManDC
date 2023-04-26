using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class BehaviourSlotController : MonoBehaviour
{
    private SlotDataFromAPI_Manager slotDFAM;
    public WarningController warningController;
    public ZabbixDataPanelController zabbixDataPanelController;
    public SlotData slotData;
    public SlotControl slotControl;

    //TODO: Do a nice call, this executes all at once at first
    public void Start(){
        if (!slotDFAM)
            slotDFAM = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>();
        zabbixDataPanelController.AsignSlotData(slotData);
    }

    public void UpdateWarningData(){
        slotDFAM.SetHostsDataToSlotData(slotData, slotControl);
        //Debug.Log("Updating for host " + slotData.hostID+"...");
        StartCoroutine(UpdateWarningDataCoroutine());
        
    }

    private IEnumerator UpdateWarningDataCoroutine(){
        List<WarningLastData> warningData = null;
        yield return slotDFAM.GetWarningsData(slotData.hostID, (List<WarningLastData> aux) => warningData = aux);
        if (warningData!=null){
            //Debug.Log("Warning found for host " + slotData.hostID);
            warningController.UpdateWarningData(warningData);
        }
    }
}

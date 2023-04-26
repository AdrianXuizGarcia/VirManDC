using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class SlotData : MonoBehaviour
{
    [Header("ARCHITECTURE DATA")]
    public string slotName;
    //[HideInInspector]
    public string slotID; // can be hostid or ip
    [Header("ZABBIX DATA")]
    public string hostID;
    public string ip;
    public string hostname;
    public string host;
    public string descriptionHost;



    public void SetArchitectureData(RackSlotDto dto){
        slotName = dto.name;
        slotID = dto.slotID;
    }
}

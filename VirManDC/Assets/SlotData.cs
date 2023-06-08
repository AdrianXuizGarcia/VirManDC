using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class SlotData : MonoBehaviour
{
    [Header("ARCHITECTURE DATA")]
    public string slotName;
    public string type;
    public string slotID; // can be hostid or ip

    [Header("ZABBIX DATA")]
    public string hostID;
    public string ip;
    public string hostname;
    public string host;
    public string descriptionHost;

    // For indicators and data
    public DataApiSchema dataApiSchema;
	public DataApiContainer dataApiContainer;
	public string hostGroupID; // for waht?

    public void SetArchitectureData(RackSlotDto dto){
        slotName = dto.name;
        slotID = dto.slotID;
        type = dto.type;
    }
}

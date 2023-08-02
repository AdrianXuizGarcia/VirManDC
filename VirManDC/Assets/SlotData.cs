using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class SlotData : MonoBehaviour
{
    [Header("ARCHITECTURE DATA")]
    public string slotName;
    public string type;
    public string slotID; // can be hostid or ip
    public bool isHypervisor;

    [Header("ZABBIX DATA")]
    public string hostID;
    public string ip;
    public string hostname;
    public string host;
    public string descriptionHost;

    // For indicators and data
    public DataApiSchema dataApiSchema;
	public DataApiContainer dataApiContainer;
	public string hostGroupID;
    public List<VMData> virtualMachinesList;

    /// <summary>
    /// Set the slot dto data to the data class
    /// </summary>
    /// <param name="dto">Slot data</param>
    public void SetArchitectureData(RackSlotDto dto){
        slotName = dto.name;
        slotID = dto.slotID;
        type = dto.type;
        isHypervisor = dto.isHypervisor;
    }
}

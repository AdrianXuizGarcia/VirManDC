using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class ArchitectureSlotData : MonoBehaviour
{
    public string slotName;

    public void SetArchitectureData(RackSlotDto dto){
        slotName = dto.name;
    }
}

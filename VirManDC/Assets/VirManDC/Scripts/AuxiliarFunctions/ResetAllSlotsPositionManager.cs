// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllSlotsPositionManager : MonoBehaviour
{
    public SlotListManager slotListManager;
    public void ResetAllSlotsPosition(){
        foreach(GameObject slot in slotListManager.slotReferencesList){
            VMDC_ResetPositionController controller = slot.GetComponentInChildren<VMDC_ResetPositionController>();
            controller.ResetPosition();
        }
    }
}

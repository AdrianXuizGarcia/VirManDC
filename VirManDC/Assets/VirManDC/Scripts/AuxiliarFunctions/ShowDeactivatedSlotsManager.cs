// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDeactivatedSlotsManager : MonoBehaviour
{
public SlotListManager slotListManager;
    public void SwapDeactivatedRedSlotsFront(){
        foreach(GameObject slot in slotListManager.slotReferencesList){
            DeactivatedSlotFrontController controller = slot.GetComponentInChildren<DeactivatedSlotFrontController>();
            controller.SwapDeactivatedRedSlotFront();
        }
    }
}

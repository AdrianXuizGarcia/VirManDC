using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotComponentsReferences : MonoBehaviour
{
    public SlotData slotDataReference;
    public BehaviourSlotController behaviourSlotController;

    public SlotData GetSlotDataReference(){
        return slotDataReference;
    }

    public void NewPetitionForWarningData(){
        behaviourSlotController.UpdateWarningData();
    }

    public void NewPetitionForMainData(){
        behaviourSlotController.UpdateMainData();
    }
    
}

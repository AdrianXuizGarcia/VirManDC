// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotComponentsReferences : MonoBehaviour
{
    public SlotData slotDataReference;
    public BehaviourSlotController behaviourSlotController;
    public TestDataPanelController dataPanelController; // TODO: CHange
    public ScriptListPanelController scriptListPanelController;

    public SlotData GetSlotDataReference(){
        return slotDataReference;
    }

    public void NewPetitionForWarningData(){
        behaviourSlotController.UpdateWarningData();
    }

    public void NewPetitionForMainData(){
        behaviourSlotController.UpdateMainData();
    }

    public void SwapDataPanelPage(int newPage){
        dataPanelController.SwapPage(newPage);
    }

    public IEnumerator NewPetitionForScriptExecution(string hostid, int scriptid){
        yield return StartCoroutine(scriptListPanelController.NewScriptExecution_Co(hostid,scriptid));
    }
    
}

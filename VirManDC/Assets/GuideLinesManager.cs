using UnityEngine;

public class GuideLinesManager : MonoBehaviour
{
    public SlotListManager slotListManager;
    public void ChangeStateAllGuideLines(){
        foreach(GameObject slot in slotListManager.slotReferencesList){
            DrawAuxiliarLinesController controller = slot.GetComponentInChildren<DrawAuxiliarLinesController>();
            controller.SwapState();
        }
    }
}

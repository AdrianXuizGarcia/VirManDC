using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatedSlotFrontController : MonoBehaviour
{
    private bool deactivatedFrontColorIsActive = false;
    [SerializeField]
    private SlotControl referenceSlotControl;
    private Color originalColor;
    public SpriteRenderer imageFront;

    void Start(){
        originalColor = imageFront.material.color;
    }

    public void SwapDeactivatedRedSlotFront(){
        if(referenceSlotControl.slotIsDeactivated){
            if(deactivatedFrontColorIsActive)
                ChangeImageColor(originalColor);
            else
                ChangeImageColor(Color.red);
            deactivatedFrontColorIsActive = !deactivatedFrontColorIsActive;
        }
    }

    private void ChangeImageColor(Color newColor){
		imageFront.material.color = newColor;
    }
}

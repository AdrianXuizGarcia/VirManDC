using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class RotateBaseWithSlider : MonoBehaviour
{
    private float prevRotation;
    public void OnSliderUpdatedRotation(SliderEventData eventData)
        {
            //transform.Rotate(0.0f, eventData.NewValue, 0.0f, Space.World);

            // How much we've changed
            float delta = eventData.NewValue - prevRotation;
            //Debug.Log(delta);
            this.transform.Rotate (Vector3.up * delta * 180);
    
            // Set our previous value for the next change
            this.prevRotation = eventData.NewValue;
        }
}

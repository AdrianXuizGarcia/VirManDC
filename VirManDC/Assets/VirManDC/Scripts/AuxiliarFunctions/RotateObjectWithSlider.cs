// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class RotateObjectWithSlider : MonoBehaviour
{
    private float prevRotation;
    [SerializeField]
    public GameObject objectToRotate;
    [SerializeField]
    private Transform rotationPivot;
    
    public void OnSliderUpdatedRotation(SliderEventData eventData)
        {
            //transform.Rotate(0.0f, eventData.NewValue, 0.0f, Space.World);

            // How much we've changed
            float delta = eventData.NewValue - prevRotation;
            //Debug.Log(delta);
            objectToRotate.transform.RotateAround (rotationPivot.position, Vector3.up, delta * 180);
    
            // Set our previous value for the next change
            this.prevRotation = eventData.NewValue;
        }
}

// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAuxiliarLinesController : MonoBehaviour
 {
    public GameObject objectOne;    // The first object you instantiate
    public GameObject objectTwo;    // The second object you instantiate
 
    public LineRenderer lineRend;   // The linerenderer component
    public bool isActive = false;

    void Start(){
        // Set the position count of the linerenderer to two
        lineRend.positionCount = 2;
        lineRend.enabled = false;
    }

    public void ChangeState(bool isActive){
        this.isActive = isActive;
        lineRend.enabled = isActive;
    }
    public void SwapState(){
        isActive = !isActive;
        lineRend.enabled = isActive;
    }

    void Update(){
        if (isActive)
            DrawLineBetweenObjects(objectOne.transform, objectTwo.transform);
    }

    void DrawLineBetweenObjects (Transform firstT, Transform secondT)
    {
        // Set the positions of the LineRenderer
        lineRend.SetPosition(0, firstT.position);
        lineRend.SetPosition(1, secondT.position);
    }
}
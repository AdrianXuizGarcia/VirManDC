using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAuxiliarLinesController : MonoBehaviour
 {
     public GameObject objectOne;    // The first object you instantiate
     public GameObject objectTwo;    // The second object you instantiate
 
     public LineRenderer lineRend;   // The linerenderer component, remember to assign this in the inspector!
 
     void Start()
     {
         // Set the position count of the linerenderer to two
         lineRend.positionCount = 2;
 
         // Get the transform of the two objects
         Transform first = objectOne.transform;
         Transform second = objectTwo.transform;
 
         
     }

     void Update(){
        DrawLineBetweenObjects(objectOne.transform, objectTwo.transform);
     }
 
     void DrawLineBetweenObjects (Transform firstT, Transform secondT)
     {
         // Set the positions of the LineRenderer
         lineRend.SetPosition(0, firstT.position);
         lineRend.SetPosition(1, secondT.position);
     }
 }
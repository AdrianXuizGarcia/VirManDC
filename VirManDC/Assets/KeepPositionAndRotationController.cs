using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPositionAndRotationController : MonoBehaviour
{

    public Transform mainTransform;
    public Transform pivotTransform;
    public bool active;

    void Start(){
        //cameraTransform = Camera.main.transform;
        //mainTransform.Rotate(initialRotation);
    }

    public void SwapState(){
        active=!active;
    }

    void Update()
    {
        if (active)
        {
            mainTransform.position = pivotTransform.position;
            //mainTransform.LookAt(Camera.main.transform);
            mainTransform.rotation = Quaternion.LookRotation(pivotTransform.forward);
        }
    }

    public void SetNewPositionOnce(){
        mainTransform.position = pivotTransform.position;
        mainTransform.rotation = Quaternion.LookRotation(pivotTransform.forward);
        
    }
}

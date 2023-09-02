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
        /*if (active){
            SetNewPositionOnce();
        }*/
    }

    /*void Update()
    {
        if (active)
        {
            //mainTransform.LookAt(Camera.main.transform);
            mainTransform.SetPositionAndRotation(pivotTransform.position, Quaternion.LookRotation(pivotTransform.forward));
        }
    }*/

    public void SetNewPositionOnce(){
        mainTransform.SetPositionAndRotation(pivotTransform.position, Quaternion.LookRotation(pivotTransform.forward));
        
    }
}

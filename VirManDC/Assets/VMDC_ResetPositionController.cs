using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMDC_ResetPositionController : MonoBehaviour
{
    public GameObject gameObjectToResetPosition;
    public GameObject pivotToSavePosition;
    private Transform positionToSave;

    void Start()
    {
        positionToSave = pivotToSavePosition.transform;
    }

    public void ResetPosition(){
        //Debug.Log("x of saved: " + positionToSave.position.x);
        //Debug.Log("x of new: " + gameObjectToResetPosition.position.x);
        gameObjectToResetPosition.transform.position = positionToSave.position;
        gameObjectToResetPosition.transform.rotation = positionToSave.rotation;
    }
}

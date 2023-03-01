using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeepRelativePositionToCamera : MonoBehaviour
{
    private Transform cameraTransform;
    public Transform mainTransform;
    public Vector3 positionToKeep;
    public Vector3 initialRotation;

    void Start(){
        cameraTransform = Camera.main.transform;
        mainTransform.Rotate(initialRotation);
    }

    void Update()
    {
        mainTransform.position =  cameraTransform.position + positionToKeep;
    }
}
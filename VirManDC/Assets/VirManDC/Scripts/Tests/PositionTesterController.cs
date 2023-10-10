// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PositionTesterController : MonoBehaviour
{
    private Transform cameraTransform;
    public Transform testTransform;
    [SerializeField]
    public GameObject display;
    private TextMeshPro displayText;

void Awake()
	{
        displayText = display.GetComponent<TextMeshPro>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        //var dist = Vector3.Distance(testTransform.position,cameraTransform.position);
        var dist = testTransform.position - cameraTransform.position;
        displayText.text = dist.ToString();
    }
}

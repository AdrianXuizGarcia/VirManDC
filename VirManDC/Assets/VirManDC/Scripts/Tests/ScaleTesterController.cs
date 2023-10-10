// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScaleTesterController : MonoBehaviour
{
    public Transform testTransform;
    [SerializeField]
    private GameObject display;
    private TextMeshPro displayText;

void Awake()
	{
        displayText = display.GetComponent<TextMeshPro>();
        //Debug.Log("DISPLAY: "+displayText.text);
    }
    void Update()
    {
        //var dist = Vector3.Distance(testTransform.position,cameraTransform.position);
        var dist = testTransform.localScale;
        displayText.text = dist.ToString();
    }
}

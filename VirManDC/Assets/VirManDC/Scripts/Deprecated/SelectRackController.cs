// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRackController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectWithRenderer;
    private Material originalMat;
    private bool original = true;

    void Start(){
        if (!gameObjectWithRenderer)
            gameObjectWithRenderer = gameObject;
        originalMat = gameObjectWithRenderer.GetComponent<Renderer>().material;
    }
    
    public void ChangeColor(Material newMat){
        if (original){
            //Material newMat = Resources.Load("Assets/VirManDC/Art/Materials/"+newColor, typeof(Material)) as Material;
            gameObjectWithRenderer.GetComponent<Renderer>().material = newMat;
        } else 
         gameObjectWithRenderer.GetComponent<Renderer>().material = originalMat;
        
        original = !original;
    }
}

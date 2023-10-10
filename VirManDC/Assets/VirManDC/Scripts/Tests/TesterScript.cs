// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TesterScript : MonoBehaviour
{
    public void ShowLog(string log){
        Debug.Log(log);
    }

    public void ActivateItem(GameObject gameObject){
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ChangeColor(string newColor, GameObject gameObject){
        Material newMat = Resources.Load(newColor, typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material = newMat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probando : MonoBehaviour
{
    public void ShowLog(string log){
        Debug.Log(log);
    }

    public void ActivateItem(GameObject gameObject){
        gameObject.SetActive(true);
    }
}

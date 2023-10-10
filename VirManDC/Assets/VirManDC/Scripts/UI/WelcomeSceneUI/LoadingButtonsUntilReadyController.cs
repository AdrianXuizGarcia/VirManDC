// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingButtonsUntilReadyController : MonoBehaviour
{
    public GameObject loadingIcon;
    public List<Button> buttonsReadyToLoad;

    //TODO: Deactive panel buttons via script, disable button and deactive icon

    void Start() {
        //loadingIcon.SetActive(true);
        ChangeStateButtons(false);
        //Debug.Log("Desactivaos");
    }

    private void ChangeStateButtons(bool activate){
        //loadingIcon.SetActive(!activate);
        foreach (Button button in buttonsReadyToLoad){
            button.interactable=activate;
        }
    }

public void OnStartNewDataPetition(){
     //Debug.Log("recibida nueva peticion warning");
     loadingIcon.SetActive(true);
}

public void OnDataRetrieved(bool isOk){
        //Debug.Log("recibidos nuevos datos warning");
        ChangeStateButtons(isOk);
        loadingIcon.SetActive(false);
    }
}

// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VMDC.Dtos;

public class RackNameController : MonoBehaviour
{
    public GameObject rackNameIcon;
    [SerializeField]
    public GameObject display;
    private TextMeshPro displayText;

    void Awake()
	{
        RackNameManager.OnChangeNameRacks += ChangeIconBehaviour;
        ArchitectureObjectsManager.OnRackInstantiationEnd += ChangeLabelName;
        displayText = display.GetComponent<TextMeshPro>();
        //Debug.Log("DISPLAY: "+displayText.text);
    }

	void OnDestroy() {
		RackNameManager.OnChangeNameRacks -= ChangeIconBehaviour;
        ArchitectureObjectsManager.OnRackInstantiationEnd -= ChangeLabelName;
	}

    private void ChangeIconBehaviour(bool change){
        rackNameIcon.SetActive(change);
    }
    
    private void ChangeLabelName(RackBasicData rackBasicData){
        //Debug.Log("Dentro name: "+rackBasicData.rackName);
        //Debug.Log(display.text);
        displayText.text=rackBasicData.rackName;
        ArchitectureObjectsManager.OnRackInstantiationEnd -= ChangeLabelName;
        //Debug.Log("hasta qui");
    }

}

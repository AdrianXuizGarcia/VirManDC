// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.AuxiliarConfiguration;
using TMPro;

public class LogInUIElementsController : MonoBehaviour
{
	public TextMeshPro serverIpText;
	public TextMeshPro zabbixAPiversionText;
	public GameObject buttonLogIn;
	public GameObject buttonCheckArchitecture;
	
	public void UpdateServerIPText(){
		//serverIpText.text = serverIpText.text + " " + ZabbixConfig.ipServer;
		serverIpText.text = "Server IP: " + ZabbixConfig.ipServer;
	}

	public void UpdateZabbixAPIText(string version){
		//serverIpText.text = serverIpText.text + " " + ZabbixConfig.ipServer;
		zabbixAPiversionText.text = "Zabbix API version: " + version;
	}
	
	public void StartLogIn(){
		//isLoading = true;
		ManageAllInputs(false);
	}
	
	public void EndLogIn(){
		//isLoading = false;
		ManageAllInputs(true);
	}
	
	private void ManageAllInputs(bool active){
		buttonLogIn.SetActive(active);
		buttonCheckArchitecture.SetActive(active);
	}
	
	public void DeactivateInitialButtons(){
		ManageAllInputs(false);
	}
	
	public void ActivateInitialButtons(){
		ManageAllInputs(true);
	}
	
	public void ActivateTryArchitectureButton(){
		buttonCheckArchitecture.SetActive(true);
	}

	public void SetErrorTexts(){
		serverIpText.text = "Error while loading app.\n Check error interface";
		zabbixAPiversionText.text = "";
	}
}

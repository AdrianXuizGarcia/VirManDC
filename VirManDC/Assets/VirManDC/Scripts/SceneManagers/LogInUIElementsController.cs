using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.AuxiliarConfiguration;

public class LogInUIElementsController : MonoBehaviour
{
	public Text serverIpText;
	public Button buttonLogIn;
	public GameObject loadingCircle;
	public InputField username;
	public InputField password;
	public Text errorText;
	
	private bool isLoading;
	
    void Start()
    {
      UpdateServerIPText();
    }
	
	void Update()
	{
		if (!isLoading)
			CheckInputFields();
	}
	
	public void UpdateServerIPText(){
		//serverIpText.text = serverIpText.text + " " + ZabbixConfig.ipServer;
		serverIpText.text = "Server IP: " + ZabbixConfig.ipServer;
	}
	
	public void StartLogIn(){
		isLoading = true;
		ManageAllInputs(false);
		loadingCircle.SetActive(true);
	}
	
	public void EndLogIn(){
		isLoading = false;
		ManageAllInputs(true);
		loadingCircle.SetActive(false);
	}
	
	private void CheckInputFields(){
		if((username.text == "")|| (password.text == ""))
			buttonLogIn.interactable = false;
		else
			buttonLogIn.interactable = true;
	}
	
	private void ManageAllInputs(bool active){
		username.interactable = active;
		password.interactable = active;
		buttonLogIn.interactable = active;
	}
	
	public void DeactivateInitialButtons(){
		ManageAllInputs(false);
	}
	
	public void ActivateInitialButtons(){
		ManageAllInputs(true);
	}
	
	public void RefreshErrorText(){
		errorText.text = "";
	}
}

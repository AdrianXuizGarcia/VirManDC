using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VMDC.AuxiliarConfiguration;

public class SettingsMenuController : MonoBehaviour
{
	public InputField url_IPServer;
    public InputField url_ZabbixAPI;
	
	public GameObject panelSettings;
	
	//?public Button buttonCheckArchitecture;
	
	public LogInUIElementsController logInUIElementsController;
	
	/// <summary>
    /// Called from Save button
    /// </summary>
    public void SaveConfigurationInFile(){
		ZabbixConfigFile.SaveAndWriteNewConfiguration(url_IPServer.text,url_ZabbixAPI.text,null);
		logInUIElementsController.UpdateServerIPText();
		panelSettings.SetActive(false);
	}

	/// <summary>
    /// Called from Options button
    /// </summary>
	public void EnterSettingsFromMenu(){
		SetActualValuesToPlaceHolder();
		panelSettings.SetActive(true);
	}
	
	public void ExitSettings(){
		panelSettings.SetActive(false);
	}
	
	private void SetActualValuesToPlaceHolder(){
		url_IPServer.text = ZabbixConfig.ipServer;
		url_ZabbixAPI.text = ZabbixConfig.urlZabbixAPI;
	}
	
    /// <summary>
    /// Called from Default Values button
    /// </summary>
	public void SetDefaultValuesToPlaceHolder(){
		url_IPServer.text = ZabbixConfigDefault.ipServer;
		url_ZabbixAPI.text = ZabbixConfigDefault.urlZabbixAPI;
	}
	
	public void AllReadyNow()
	/*
		Called when config files and xml are all ok,
		so we can enter CheckArchitecture */
	{
		//?buttonCheckArchitecture.interactable = true;
	}

}

// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using EncryptLibrary;
using UnityEngine;
using UnityEngine.UI;
using VMDC.AuxiliarConfiguration;
using VMDC.Constants;

public class SettingsMenuController : MonoBehaviour
{
	public InputField url_IPServer;
    public InputField url_ZabbixAPI;

	public InputField userText;
    public InputField passText;
	
	public GameObject panelSettings;

	//?public Button buttonCheckArchitecture;

	//public LogInUIElementsController logInUIElementsController;
	public StartController_WelcomeScene startController_WelcomeScene;
	
	/// <summary>
    /// Called from Save button
    /// </summary>
    public void SaveConfigurationInFile(){
		// TODO: Pass urlConfigurationFiles as value, not used atm
		ZabbixConfigFile.SaveAndWriteNewConfiguration(url_IPServer.text, url_ZabbixAPI.text,
						ZabbixConfig.urlConfigurationFiles,
						StringCipher.Encrypt(userText.text, VMDCEncrypt.PassPhrase),
						StringCipher.Encrypt(passText.text, VMDCEncrypt.PassPhrase)
						);
		//logInUIElementsController.UpdateServerIPText();
		// Make a new petition for log in 
		startController_WelcomeScene.MakeLogInPetition();
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
		userText.text = ZabbixConfig.encryptedUser!=string.Empty ? StringCipher.Decrypt(ZabbixConfig.encryptedUser, VMDCEncrypt.PassPhrase) : string.Empty;
		passText.text = ZabbixConfig.encryptedPass!=string.Empty ? StringCipher.Decrypt(ZabbixConfig.encryptedPass, VMDCEncrypt.PassPhrase) : string.Empty;
	}
	
    /// <summary>
    /// Called from Default Values button
    /// </summary>
	public void SetDefaultValuesToPlaceHolder(){
		url_IPServer.text = ZabbixConfigDefault.ipServer;
		url_ZabbixAPI.text = ZabbixConfigDefault.urlZabbixAPI;
	}

}

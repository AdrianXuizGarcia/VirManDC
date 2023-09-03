using System.Collections;
using System.Collections.Generic;
using AuxiliarMethods;
using EncryptLibrary;
using TMPro;
using UnityEngine;
using VMDC.AuxiliarConfiguration;
using VMDC.Constants;
using VMDC.Dtos;

public class StartController_WelcomeScene : MonoBehaviour
{
    public GameObject guiInfoPanel;
    public GameObject guiChangeArchitecturePanel;
    public GameObject guiOptions;
    public GameObject loadingBar;
    public float rotationGui = 24;

    public ZabbixServerStatusIconController zabbixServerStatusIconController;
    public ZabbixPetitions apiPetitions;
    public SlotDataFromAPI_Manager slotDFAM;

    public LogInUIElementsController logInUIElementsController;

    void Start()
    {
        guiInfoPanel.SetActive(false);
        guiChangeArchitecturePanel.SetActive(false);
        guiOptions.SetActive(false);
        loadingBar.SetActive(false);
        logInUIElementsController.StartLogIn();
        guiInfoPanel.transform.Rotate(0.0f, rotationGui, 0.0f);
        guiChangeArchitecturePanel.transform.Rotate(0.0f, -rotationGui, 0.0f);
        bool errorFound = false;

        // Check if there is a problem with the configuration files
        if (!Files_extraMethods.CheckOrCreateDirectoryAuxFiles()){
			ErrorManager.NewErrorMessage("Essential directory '"+VMDCPaths.extraFilesDirPath+"' cannot be created");
			logInUIElementsController.DeactivateInitialButtons();
            errorFound = true;
		}
		string missingFile = Files_extraMethods.CheckIfAuxFilesMissing();
		if (missingFile!=""){
			ErrorManager.NewErrorMessage("Essential file '"+missingFile+"' on directory '"+VMDCPaths.extraFilesDirPath+"' was not found. Add it and restart the app");
			logInUIElementsController.DeactivateInitialButtons();
            errorFound = true;
		}
        missingFile = Files_extraMethods.CheckIfConfigurationFileMissing();
        if (missingFile!=""){
			ErrorManager.NewErrorMessage("Essential file '"+missingFile+"' on directory '"+Application.persistentDataPath+"' was not found. Add it and restart the app");
			logInUIElementsController.DeactivateInitialButtons();
            errorFound = true;
		}

        if (!errorFound)
        {
            // If all ok, activate at least the try architecture button
            logInUIElementsController.ActivateTryArchitectureButton();

            // No conection needed
            ZabbixConfigFile.SetDefaultConfigFromDefaultFile();
            ZabbixConfigFile.SetConfig();

            // Need to conect
            MakeLogInPetition();
        } else {
            logInUIElementsController.SetErrorTexts();
            zabbixServerStatusIconController.SetStatus(false);
        }
    }

    public void MakeLogInPetition(){
        zabbixServerStatusIconController.ResetStatus();
        StartCoroutine(GetZabbixAPIVersion());
        logInUIElementsController.UpdateServerIPText();
        StartCoroutine(MakeLogInPetitionCo());
    }

    private IEnumerator MakeLogInPetitionCo(){
		string responseString = "";
        yield return StartCoroutine(apiPetitions.MakeLogInPetition(
            StringCipher.Decrypt(ZabbixConfig.encryptedUser,VMDCEncrypt.PassPhrase),
            StringCipher.Decrypt(ZabbixConfig.encryptedPass,VMDCEncrypt.PassPhrase),
            (string aux) => responseString=aux));
        if (responseString=="" || responseString==null){
            ConectionIsBad();
        } else {
            ConectionIsOk(responseString);
        }
	}

    private void ConectionIsOk(string authKey){
        ZabbixConfig.authKey = authKey;
        logInUIElementsController.EndLogIn();
        zabbixServerStatusIconController.SetStatus(true);
        Debug.Log("Conected to server.");
        //StartCoroutine(UpdateWarningsCoroutine());
    }

    private void ConectionIsBad(){
        zabbixServerStatusIconController.SetStatus(false);
        //ErrorManager.NewErrorMessage("Error conecting to server");
    }


    public IEnumerator GetZabbixAPIVersion()
	{
		string version = "";
		yield return StartCoroutine(slotDFAM.MakeApiVersionPetition((string aux)=>version=aux));
        logInUIElementsController.UpdateZabbixAPIText(version);

    }

}

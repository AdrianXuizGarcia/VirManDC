using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VMDC.AuxiliarConfiguration;
using VMDC.Dtos;

public class StartController_WelcomeScene : MonoBehaviour
{
    public GameObject guiInfoPanel;
    public GameObject guiChangeArchitecturePanel;
    public GameObject guiOptions;
    public GameObject loadingBar;
    public GameObject signInButton;
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
        signInButton.SetActive(false);
        guiInfoPanel.transform.Rotate(0.0f, rotationGui, 0.0f);
        guiChangeArchitecturePanel.transform.Rotate(0.0f, -rotationGui, 0.0f);

        // No conection needed
        ZabbixConfigFile.SetDefaultConfigFromDefaultFile();
		ZabbixConfigFile.setConfig();

        logInUIElementsController.UpdateServerIPText();
        StartCoroutine(GetZabbixAPIVersion());

        // Need to conect
        StartCoroutine(MakeLogInPetition());
    }

    private IEnumerator MakeLogInPetition(){
		//Debug.Log("Conecting to server...");
		string responseString = "default";
		yield return StartCoroutine(apiPetitions.MakeLogInPetition("Admin","d1@8#y2FFHl3",(string aux) => responseString=aux));
        if (responseString=="" || responseString==null){
            ConectionIsBad();
        } else {
            ConectionIsOk(responseString);
        }
	}

    private void ConectionIsOk(string authKey){
        ZabbixConfig.authKey = authKey;
        signInButton.SetActive(true);
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
        // 10566 es webcitic
		yield return StartCoroutine(slotDFAM.MakeApiVersionPetition((string aux)=>version=aux));
        logInUIElementsController.UpdateZabbixAPIText(version);

    }

    /*
    Prueba de warning
    public IEnumerator UpdateWarningsCoroutine()
	{
		List<WarningLastData> warningData = null;
        // 10566 es webcitic
		yield return StartCoroutine(slotDFAM.GetWarningsData("10566", (List<WarningLastData> aux)=>warningData=aux));
		//WarningLastData warningData = slotDFAM.GetWarningsData(slotDaC.hostID);
		if (warningData!=null)
			text.text=warningData[0].warningDescription;
		
	}*/

}

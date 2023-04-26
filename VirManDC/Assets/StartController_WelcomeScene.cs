using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VMDC.AuxiliarConfiguration;
using VMDC.Dtos;

public class StartController_WelcomeScene : MonoBehaviour
{
    public GameObject guiInfoPanel;
    public GameObject loadingBar;
    public GameObject signInButton;
    public float rotationGui = 24;
    public TextMeshPro serverIPText;

    public ZabbixServerStatusIconController zabbixServerStatusIconController;
    public ZabbixPetitions apiPetitions;
    public SlotDataFromAPI_Manager slotDFAM;

    //TODO delete
    public TextMeshProUGUI text;

    void Start()
    {
        guiInfoPanel.SetActive(false);
        loadingBar.SetActive(false);
        signInButton.SetActive(false);
        guiInfoPanel.transform.Rotate(0.0f, rotationGui, 0.0f);

        // No conection needed
        ZabbixConfigFile.SetDefaultConfigFromDefaultFile();
		ZabbixConfigFile.setConfig();

        serverIPText.text += ZabbixConfig.ipServer;

        // Need to conect
        StartCoroutine(MakeLogInPetition());
    }

    private IEnumerator MakeLogInPetition(){
		//Debug.Log("Conecting to server...");
		string responseString = "default";
		yield return StartCoroutine(apiPetitions.MakeLogInPetition("Admin","zabbix",(string aux) => responseString=aux));
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
        StartCoroutine(UpdateWarningsCoroutine());
    }

    private void ConectionIsBad(){
        zabbixServerStatusIconController.SetStatus(false);
        //ErrorManager.NewErrorMessage("Error conecting to server");
    }

    public IEnumerator UpdateWarningsCoroutine()
	{
		List<WarningLastData> warningData = null;
        // 10566 es webcitic
		yield return StartCoroutine(slotDFAM.GetWarningsData("10566", (List<WarningLastData> aux)=>warningData=aux));
		//WarningLastData warningData = slotDFAM.GetWarningsData(slotDaC.hostID);
		if (warningData!=null)
			text.text=warningData[0].warningDescription;
		
	}

}

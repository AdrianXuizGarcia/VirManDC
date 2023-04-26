using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using TMPro;
using VMDC.AuxiliarConfiguration;

public class CheckConnection : MonoBehaviour
{
	[SerializeField]
	private TMP_Text display;

    public ZabbixPetitions apiPetitions;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Check());
    }

    public IEnumerator Check()
    // Return "" if an error occurred
    {
        // First we set the url 
        var url = "http://" + "10.56.64.60" + "/" + "api_jsonrpc.php";
        string jsonParams = "HI";
        //Debug.Log(jsonParams);

        // Make a new POST request with the params we have set
        var www = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonParams);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json-rpc");

        // Now wait for the request to be sent
        yield return www.SendWebRequest();

        // We send back the result, as success or as error
        if (www.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log($"Success: {www.downloadHandler.text}");
            display.text = "YES!";
            yield return null;
        }
        else
        {
            display.text = ManageErrorFromWebRequest(www);
            yield return null;
        }

		ZabbixConfigFile.SetDefaultConfigFromDefaultFile();
		ZabbixConfigFile.setConfig();
        Debug.Log(ZabbixConfig.ipServer);
        StartCoroutine(MakeLogInPetition());
    }

	private  IEnumerator MakeLogInPetition(){
		Debug.Log("Conecting to server...");
		string responseString = "default";
		yield return StartCoroutine(apiPetitions.MakeLogInPetition("Admin","zabbix",(string aux) => responseString=aux));
		if (responseString=="") ErrorManager.NewErrorMessage("Error conecting to server");
		else Debug.Log("Conected to server.");
		ZabbixConfig.authKey = responseString;
	}
    		
    // Auxiliar method to manage the error messages from the UnityWebRequest
    private string ManageErrorFromWebRequest(UnityWebRequest www){
        Debug.Log("<color=red>ERROR</color>: Problem with API Zabbix");
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                display.text="Failed to communicate with the server";
                break;
            case UnityWebRequest.Result.ProtocolError:
                display.text="The server returned an error response";
                break;
            case UnityWebRequest.Result.DataProcessingError:
                display.text="Error processing data";
                break;
            default:
                display.text="Net error";
                break;
        }
        Debug.Log($"Detailed Error: {www.error}");			
        //callback(www.error);
        return "";
    }

    public void MakeWarningsPetition(){
        StartCoroutine(apiPetitions.AllWarningsPetition(null,null));
    }


}

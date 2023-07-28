using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Constants;
using TMPro;

public class ErrorManagerGUI : MonoBehaviour
{
	public GameObject errorCanvas;
    private TextMeshProUGUI displayText;
    public GameObject optionsErrorScroll;
	
	//private bool initialized = false;
	
	void Awake()
	{
        Initialize();
        ErrorManager.OnNewError += ErrorLog;
    }

	void OnDestroy() {
		ErrorManager.OnNewError -= ErrorLog;
	}

	private void ErrorLog(string error){
        //Debug.Log("also in gui: " + error);
        NewErrorMessage(error);
    }
	
	private void Initialize(){
		//errorText = generalErrorMessage.GetComponentInChildren<Text>();
		//logInErrorMessage = logInErrorScroll.GetComponentInChildren<Text>();
		displayText = optionsErrorScroll.GetComponent<TextMeshProUGUI>();
		errorCanvas.SetActive(false);
        ClearLog();
        //initialized = true;
    }
	
	public void NewErrorMessage(string s)
	{	
		if(!errorCanvas.activeSelf)
            errorCanvas.SetActive(true);
        //TextMeshProUGUI displayText = optionsErrorScroll.GetComponent<TextMeshProUGUI>();
        displayText.text = displayText.text+"\n"+s;
        //Debug.Log("SO: " + displayText.text);
    }

	public void ClearLog()
	{
		displayText.text = "";
	}
}

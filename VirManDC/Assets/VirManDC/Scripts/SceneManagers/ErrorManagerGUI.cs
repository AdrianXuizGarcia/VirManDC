// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

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
		displayText = optionsErrorScroll.GetComponent<TextMeshProUGUI>();
		errorCanvas.SetActive(false);
        ClearLog();
    }
	
	public void NewErrorMessage(string s)
	{	
		// If the interface was close, the previous error is 
		// suposed to be already known
		if(!errorCanvas.activeSelf)
			ClearLog();
        errorCanvas.SetActive(true);
        displayText.text = displayText.text+"\n"+s;
    }

	public void ClearLog()
	{
		displayText.text = "";
	}
}

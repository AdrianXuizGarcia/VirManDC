using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Constants;

public class ErrorManager : MonoBehaviour
{
	public GameObject logInErrorScroll;
	private Text logInErrorMessage;
	public GameObject optionsErrorScroll;
	private Text optionsErrorMessage;
	public GameObject generalErrorMessage;
	private Text errorText;
	public GameObject GUIlogin;
	public GameObject GUIoptions;
	//private LogManager logManager;
	
	private bool initialized = false;
	
	void Awake()
	{
		//logManager = GetComponent<LogManager>();
		Initialize();
		
	}
	
	private void Initialize(){
		errorText = generalErrorMessage.GetComponentInChildren<Text>();
		logInErrorMessage = logInErrorScroll.GetComponentInChildren<Text>();
		optionsErrorMessage = optionsErrorScroll.GetComponentInChildren<Text>();
		initialized = true;
	}
	
	/*public void NewErrorMessage(string s)
	// If the LogIn UI is still active, print the
	// error message there. If not, print it on the 
	// in-scene error display 
	{	
		// Check if its not been initialized
		if (!initialized)
			Initialize();
		if (!GUIlogin.activeSelf){
			if (!generalErrorMessage.activeSelf)
			{
				errorText.text = s;
				generalErrorMessage.SetActive(true);
			} else
				errorText.text = errorText.text+"\n"+s;
			//}
		} else {
			if (!logInErrorScroll.activeSelf){
				logInErrorScroll.SetActive(true);
				logInErrorMessage.text = s;
			} else 
				logInErrorMessage.text = logInErrorMessage.text+"\n"+s;
			//Debug.Log(ErrorInRed(s));
		}
		Debug.Log(s);
	}^*/
	
	public void NewErrorMessage(string s)
	// If the LogIn UI is still active, print
	// the error message there. 
	// If the options UI is still active, print
	// the error message there.
	// In other cases, print it on the 
	// in-scene error display 
	{	
		// Check if its not been initialized
		if (!initialized)
			Initialize();
		if (GUIoptions.activeSelf){
			if (!optionsErrorScroll.activeSelf){
				optionsErrorScroll.SetActive(true);
				optionsErrorMessage.text = s;
			} else 
				optionsErrorMessage.text = optionsErrorMessage.text+"\n"+s;
		} else if (GUIlogin.activeSelf){
			if (!logInErrorScroll.activeSelf){
				logInErrorScroll.SetActive(true);
				logInErrorMessage.text = s;
			} else 
				logInErrorMessage.text = logInErrorMessage.text+"\n"+s;
		} else {
			if (!generalErrorMessage.activeSelf)
			{
				errorText.text = s;
				generalErrorMessage.SetActive(true);
			} else
				errorText.text = errorText.text+"\n"+s;
		}
		Debug.Log(s);
	}

    private string ErrorInRed(string s)
	{
		int position = s.IndexOf(":");
		if (position < 0)
			return s;
		return "<color=red>"+s.Substring(0,position)+"</color>"+s.Substring(position+1);
	}
}

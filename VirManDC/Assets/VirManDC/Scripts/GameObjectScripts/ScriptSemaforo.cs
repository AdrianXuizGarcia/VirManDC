using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Dtos;

public class ScriptSemaforo : MonoBehaviour {

	//[Header("0 - General, 1 - CPU, 2 - Disks, 3 - Memory")]
    public int buttonID;
    private ScriptPanelInfo scriptPanel;
	
	private IndicatorPanelController indicatorPanelcontroller;
	
    public SlotDataAndControl slotDataAndControl;
	
	private GameObject errorIcon;
	//private ErrorManager errorManager;
	private string originalErrorText;
	
	private bool buttonDeactivated = false;
	
	private KeyData buttonKeyData;
	public string keySemaforo;
	
	private GameObject nameButton;
	private Text icon_text;
	private Image icon_image;
	private Behaviour icon_halo;
	private Color originalColor;
	
	private bool initialized = false;
	
	public bool isVMButton;

	// Use this for initialization
	void OnEnable () {
		if (!initialized)
			Inicialize();
	}
	
	private void Inicialize()
	{
		indicatorPanelcontroller = gameObject.GetComponentInParent<ControllersList>().indicatorPanelcontroller;
		if (indicatorPanelcontroller==null)
			Debug.Log("LOL");
		scriptPanel = indicatorPanelcontroller.GetScriptPanelInfo();
		
		//errorManager = GameObject.FindWithTag("ErrorManager").GetComponent<ErrorManager>();
		errorIcon = transform.GetChild(3).gameObject;
		
		slotDataAndControl = GetComponentInParent<SlotDataAndControl>();
		//if (slotDataAndControl==null)
		//	Debug.Log("es nulo al principio");
		
		icon_image = transform.GetChild(0).gameObject.GetComponent<Image>();
		icon_halo = (Behaviour)transform.GetChild(0).gameObject.GetComponent("Halo");
		GameObject aux = transform.GetChild(1).gameObject;
		icon_text = aux.GetComponent<Text>();
		// No need to show a % if its the VM button
		if (isVMButton)
			aux.SetActive(false);
		nameButton = transform.GetChild(2).gameObject;

		originalColor = GetComponent<Image>().color;
		
		//Debug.Log("Inicialized");
		initialized=true;
	}

    void OnMouseEnter()
    {
		Entering();
    }
	
	public void Entering()
	{
        nameButton.SetActive(true);
	}

    private void OnMouseExit()
    {
        nameButton.SetActive(false);
    }

    private void OnMouseDown()
	{      
	   Clicking();
	}
	
	/*public void Clicking()
	{
		if (!buttonDeactivated) {
			if (scriptPanel.buttonActive == buttonID)
				HideInfo();
			else
			{
				if (scriptPanel.buttonActive == -1)
				{
					panelInicial.SetActive(false);
					listaElementos.SetActive(true);
				}
			scriptPanel.buttonActive = buttonID;
			StartCoroutine("UpdateDataButton");
		   }
	   }
	}*/
	
	public void Clicking()
	{
		if (!buttonDeactivated) {
			if (indicatorPanelcontroller.ButtonWithIDHasBeenPressed(buttonID))
				StartCoroutine("UpdateDataButtonAndPanel");
		}
	}

	public IEnumerator UpdateDataButton()
	/*
		This methods sets the data located in the SlotDataAndControl in the parent object
		into the info panel. Its called the first time the slot is open, and when an
		update is called.
		*/
    {
		if (!initialized)
			Inicialize();

		indicatorPanelcontroller.RestoreErrorText();
		if (buttonDeactivated)
			ActivateButton();
		
		if (!isVMButton){
			//StartCoroutine(AddDataToPanel(dataSlot.appDataList[buttonID]));
			DataApiContainer dataSlot = slotDataAndControl.dataApiContainer;
			buttonKeyData=dataSlot.keyDataList[buttonID];
			yield return StartCoroutine(UpdateIconData());
		}
		
    }
	
	public IEnumerator UpdateDataButtonAndPanel(){
		//StopCoroutine("UpdateDataButtonAndPanel");
		yield return StartCoroutine("UpdateDataButton");
		
		DataApiContainer dataSlot = slotDataAndControl.dataApiContainer;
		scriptPanel.ClearList();
		// Depending of the type of the semaforo, we add a button or not
		if (isVMButton){
			//Debug.Log("Inside:");
			//foreach(VMData vm in slotDataAndControl.virtualMachinesList)
			//	Debug.Log(vm.hostname);
			//if (!vmDone)
			StartCoroutine(AddVMToPanel(slotDataAndControl.virtualMachinesList));
			yield return null;
		} else {
			StartCoroutine(AddDataToPanel(dataSlot.appDataList[buttonID]));
			buttonKeyData=dataSlot.keyDataList[buttonID];
			yield return StartCoroutine(UpdateIconData());
		}
		
	}
	
	private IEnumerator AddVMToPanel(List<VMData> vmlist){
		foreach (VMData vm in vmlist) {
			scriptPanel.AddVirtualServerButton(vm.hostname, vm.hostID, vm.isVmActive);
			yield return null;
		}
		//vmDone = true;
	}
	
    private IEnumerator AddDataToPanel(List<InfoApi> dataList)
    {
		if (dataList.Count==0) {
			DeactivateButton();
			indicatorPanelcontroller.SetErrorText("\nError: no values returned from API");
		} else {
			//scriptPanel.ClearList();
			foreach (InfoApi data in dataList) {
				//scriptPanel.AddElement(data.key_, data.description, data.lastValue);

				string valueParsed = data.lastValue;
				//Debug.Log(valueParsed);
				if (float.TryParse(valueParsed, out _))
					valueParsed = Math.Round(float.Parse(data.lastValue, CultureInfo.InvariantCulture),2).ToString();
				
				scriptPanel.AddElement(data.name, data.description, valueParsed);
				yield return null;
			}
		}
    }
	
	private IEnumerator UpdateIconData()
	{
		if (buttonKeyData.keyValue!=null) {
			float value = (float)Math.Round(float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture),2);
			float perCent = (value * 100)/buttonKeyData.keyModel.topLimit;
			icon_text.text = perCent.ToString();
			if (buttonKeyData.keyModel.topLimit==100)
				icon_text.text = icon_text.text + "%";
			icon_image.fillAmount = perCent/100;
			/*icon_text.text=(Math.Round(float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture),2)).ToString() + "%";
			icon_image.fillAmount = float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture)/100;*/
			if (icon_image.fillAmount > 0.8f) icon_halo.enabled = true; else icon_halo.enabled = false;
		} else {
			errorIcon.SetActive(true);
			indicatorPanelcontroller.SetErrorText("\nError: no value found for key '"+buttonKeyData.keyModel.id+"'");
			icon_text.text = "Error";
		}
		yield return null;
	}
	
	private void DeactivateButton()
	{
		buttonDeactivated = true;
		Color tempcolor = GetComponent<Image>().color;
		tempcolor = Color.red;
		tempcolor.a = .2f;
		GetComponent<Image>().color = tempcolor;
	}
	
	private void ActivateButton()
	{
		buttonDeactivated = false;
		GetComponent<Image>().color = originalColor;
	}
	
	public void ManageErrorPanel()
	{
		indicatorPanelcontroller.ManageErrorPanel();
	}
}

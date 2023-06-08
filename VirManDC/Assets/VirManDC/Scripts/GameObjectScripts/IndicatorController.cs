using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Dtos;
using TMPro;

public class IndicatorController : MonoBehaviour
{

    //[Header("0 - General, 1 - CPU, 2 - Disks, 3 - Memory")]
    public int buttonID;
    private ScriptPanelInfo scriptPanel;
    private KeyData buttonKeyData;
    public Image fillIconImage;
	public TextMeshProUGUI icon_text;
    public Behaviour icon_halo;
    public GameObject errorIcon;
    // For debugging (?)
    public string keySemaforo;

    /// <summary>
    ///This methods sets the data located in the SlotDataAndControl in the parent object
    ///into the info panel. Its called the first time the slot is open, and when an
    ///update is called.
    /// </summary>
    /// <returns></returns>
	public IEnumerator UpdateDataButton(List<KeyData> newKeyData)
    {
        buttonKeyData = newKeyData[buttonID];
        Debug.Log("Data to update: " + newKeyData[buttonID].keyModel);
        //if (!initialized)
        //	Inicialize();

        //indicatorPanelcontroller.RestoreErrorText();
        //if (buttonDeactivated)
        //	ActivateButton();

        //if (!isVMButton){
        //StartCoroutine(AddDataToPanel(dataSlot.appDataList[buttonID]));
        //DataApiContainer dataSlot = slotDataAndControl.dataApiContainer;
        //buttonKeyData=dataSlot.keyDataList[buttonID];
        yield return StartCoroutine(UpdateIconData());
		//}
		
    }

    private IEnumerator UpdateIconData()
	{
		if (buttonKeyData.keyValue!=null) {
			float value = (float)Math.Round(float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture),2);
			float perCent = (value * 100)/buttonKeyData.keyModel.topLimit;
			icon_text.text = perCent.ToString();
			if (buttonKeyData.keyModel.topLimit==100)
				icon_text.text = icon_text.text + "%";
			fillIconImage.fillAmount = perCent/100;
			/*icon_text.text=(Math.Round(float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture),2)).ToString() + "%";
			icon_image.fillAmount = float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture)/100;*/
			//?if (fillIconImage.fillAmount > 0.8f) icon_halo.enabled = true; else icon_halo.enabled = false;
		} else {
			//?errorIcon.SetActive(true);
			ErrorManager.NewErrorMessage("\nError: no value found for key '"+buttonKeyData.keyModel.id+"'");
			//?icon_text.text = "Error";
		}
		yield return null;
	}

    /*public IEnumerator UpdateDataButtonAndPanel(){
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
		
	}*/
}
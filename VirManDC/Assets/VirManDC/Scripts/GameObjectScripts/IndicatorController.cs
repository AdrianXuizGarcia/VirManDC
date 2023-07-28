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
    //private ScriptPanelInfo scriptPanel;
    private KeyData buttonKeyData; // Shared between all buttons. Not problem, is little
    public Image fillIconImage;
	public TextMeshProUGUI icon_text;
    public Behaviour icon_halo;
    public GameObject errorIcon;
    // For debugging (?)
    public string keySemaforo;
    private SlotComponentsReferences slotComponentsReferences;

    void Start(){
        slotComponentsReferences = this.GetComponentInParent<SlotComponentsReferences>();
    }

    /// <summary>
    ///This methods sets the data located in the SlotDataAndControl in the parent object
    ///into the info panel. Its called the first time the slot is open, and when an
    ///update is called.
    /// </summary>
    /// <returns></returns>
	public IEnumerator UpdateDataButton(List<KeyData> newKeyData)
    {
        buttonKeyData = newKeyData[buttonID];
        yield return StartCoroutine(UpdateIconData());
		//}
		
    }

    private IEnumerator UpdateIconData()
	{
		if (buttonKeyData.keyValue!=null) {
            float value = (float)Math.Round(float.Parse(buttonKeyData.keyValue, CultureInfo.InvariantCulture),2);
            float perCent = (value * 100)/buttonKeyData.keyModel.topLimit;
            string perCentString = ((float)Math.Round(perCent, 2)).ToString();
            //icon_text.text = perCent.ToString();
            icon_text.text = perCentString;
            //if (buttonKeyData.keyModel.topLimit==100)
            //	icon_text.text = icon_text.text + "%";
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

	public void ChangeFocusedIndicator(){
        if(slotComponentsReferences)
            slotComponentsReferences.SwapDataPanelPage(buttonID);
        //x.ChangeFocusedIndicator(buttonID);
    }
}
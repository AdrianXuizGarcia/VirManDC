// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using VMDC.Dtos;

public class WarningController : MonoBehaviour
{
	public GameObject warningMainPanel;
    public TextMeshProUGUI warningText;
    public ButtonConfigHelper buttonConfigHelper;
	public WarningSlotIconController warningSlotIconController;
	private List<WarningLastData> warningDataList;

	void Start(){
        warningMainPanel.SetActive(false);
        warningText.text = "No warnings detected";
    }
	
	public void UpdateWarningData (List<WarningLastData> newDataList){
		warningDataList = newDataList;
		// WIP
		if (warningDataList == null){
            SetButtonToAdjust("WarningWhite");
            warningMainPanel.SetActive(false);
			warningSlotIconController.NewStateIcon(false);
		}
		else {
			SetButtonToAdjust("WarningRed");
			UpdateUI();
			warningSlotIconController.NewStateIcon(true);
		}
	}

	/// <summary>
    /// Since this controller is in the panel, this petition avoids
    /// a dependance with the slotController. Should be one reference on
    /// main parent, so this call returns something
    /// </summary>
	public void NewPetitionForWarningData(){
        this.GetComponentInParent<SlotComponentsReferences>().NewPetitionForWarningData();
    }
	
	/*public void NextWarning(){
		actualWarning = actualWarning+1;
		if (actualWarning > warningDataList.Count)
			actualWarning = 1;
		UpdateUI();
	}
	*/
	private void UpdateUI(){
        //WarningLastData warningData = warningDataList[actualWarning-1];
        warningText.text = "";
        foreach (WarningLastData warningData in warningDataList)
        {
            warningText.text = warningText.text + "- " + warningData.warningDescription + "\n\n";
        }
        warningMainPanel.SetActive(true);
	}

	public void SetButtonToAdjust(string buttonName){
		//ButtonConfigHelper buttonConfigHelper = gameObject.GetComponent<ButtonConfigHelper>();
		buttonConfigHelper.SetQuadIconByName(buttonName);
	}
}

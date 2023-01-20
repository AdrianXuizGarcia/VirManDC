using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Dtos;

public class WarningController : MonoBehaviour
{
    public GameObject warningPanel;
	public GameObject warningCanvas;
	public Text warningChangeButton;
    public Text warningText;
	
	private Color orange = new Color32(255, 128, 8, 230);
	private Color red = new Color32(236, 9, 0, 230);
	private Color blue = new Color32(0, 176, 236, 230);
	
	private List<WarningLastData> warningDataList;
	private int actualWarning = 1;

	private void SetColorViaSeverity(int severity)
	{
		Image image = warningPanel.GetComponent<Image>();
		if (severity>4) {
			image.color = red;
		} else if (severity>2) {
			image.color = orange;
		} else {
			image.color = blue;
		}
	}
	
	public void ActivePanel(){
		warningPanel.SetActive(!warningPanel.activeSelf);
	}
	
	public void UpdateWarningData (List<WarningLastData> newDataList){
		warningDataList = newDataList;
		// WIP
		if (warningDataList == null){
			warningCanvas.SetActive(false);
		}
		else {
			UpdateUI();
		}
	}
	
	public void NextWarning(){
		actualWarning = actualWarning+1;
		if (actualWarning > warningDataList.Count)
			actualWarning = 1;
		UpdateUI();
	}
	
	private void UpdateUI(){
		WarningLastData warningData = warningDataList[actualWarning-1];
		SetColorViaSeverity(warningData.priority);
        warningText.text = warningData.warningDescription;
		warningChangeButton.text = actualWarning+"/"+ warningDataList.Count;
		warningCanvas.SetActive(true);
	}
}

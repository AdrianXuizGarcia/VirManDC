using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Dtos;
using VMDC.Constants;

public class IndicatorPanelController_old : MonoBehaviour
/*
	There is one script instance for each slot
*/
{
	
	private IndicatorPanelManager indicatorPanelManager;
	private IndicatorsPanelData normalPanelData;
	private IndicatorsPanelData vmPanelData;
	
	private List<GameObject> normalSemaforos = new List<GameObject>();
	private List<GameObject> vmSemaforos = new List<GameObject>();
	
	private bool usingVmSemaforos = false;
	
	public GameObject panelSemaforo;
	
	public GameObject panelInicial;
	public GameObject errorPanel;
	public GameObject listaElementos;
	
	private ScriptPanelInfo scriptPanel;
	private Text errorText;
	private string originalErrorText;
		
	private bool initialized = false;
	
	void OnEnable () {
		if (!initialized)
			Inicialize();
	}
	
	private void Inicialize()
	{
		scriptPanel = listaElementos.GetComponent<ScriptPanelInfo>();
		
		//TODO errorText = errorPanel.transform.GetChild(0).GetComponent<Text>();
		//originalErrorText = errorText.text;
	}
	
	public void RestoreErrorText(){
		errorText.text = originalErrorText;
	}
	
	public void SetErrorText(string error){
		errorText.text=errorText.text+error;
	}
	
	public ScriptPanelInfo GetScriptPanelInfo(){
		if (!initialized)
			Inicialize();
		return scriptPanel;
	}
	
	public DataApiSchema StartPanelAndSaveApiSchema(string type)
	/*
		First time the panel is open, apply the settings defined in the xml and stored in the 
		PanelSemaforoMasterManager. Then, instace the semaforos defined in the xml.
	*/
	{
		// Get the data asociated with the type
		indicatorPanelManager = GameObject.FindWithTag("IndicatorPanelManager").GetComponent<IndicatorPanelManager>();
		IndicatorsPanelData panelData = indicatorPanelManager.GetPanelData(type);
		if (panelData != null) {
			//ApplyPanelSettings(panelData);
			// To check if is vm type
			InstanceSemaforosPanel(panelData.listSemaforos, type==VMDCTags.VIRTUAL_MACHINE_TAG);
			// Save panel data settings
			if (type==VMDCTags.VIRTUAL_MACHINE_TAG)
				vmPanelData = panelData;
			else
				normalPanelData = panelData;
			return panelData.schema;
		}
		return null;
	}
	
	private void ApplyPanelSettings(IndicatorsPanelData panelData)
	{
		//Debug.Log("Starting the panel");
		
		// Get the image from the ScreenPanel (parent)
		Image image = panelSemaforo.transform.parent.gameObject.GetComponent<Image>();
		ApplyColor(image,panelData.screenBackgroundColor);
		
		// Get the image from this object
		image = panelSemaforo.GetComponent<Image>();
		ApplyColor(image,panelData.panelBackgroundColor);
	}
	
	private void InstanceSemaforosPanel(List<SemaforoData> listSemaforos, bool isVM)
	/*
		Instantiate the semaforo with the data given by the XML.
		
		Note: Two images are needed, one with the name in the xml and another one
		with the same name followed by "_color"
		Note2: If the hierarch of the items change, this need to be changed too
	*/
	{
		// Calculate the distance between the others semaforos
		float distance = 1.6f/listSemaforos.Count;
		float i = distance;
		int id = 0;
		foreach(SemaforoData panel in listSemaforos){
			// Load the semaforo prefab used as base
			GameObject semaforoPrefab = Resources.Load<GameObject>(VMDCPaths.semaforoPrefabPath);
			if (semaforoPrefab == null)
			{
				Debug.Log("ERROR: The semaforo prefab couldnt be loaded.");
			}
			GameObject semaforo = Instantiate(semaforoPrefab,panelSemaforo.transform) as GameObject;
			semaforo.transform.Translate(Vector3.left*i,Space.Self);
			semaforo.name = panel.name;
			semaforo.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>().text = panel.name;
			semaforo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/"+panel.imageName);
			semaforo.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/"+panel.imageName+"_color");
			semaforo.GetComponent<IndicatorController_old>().keySemaforo = panel.key;
			semaforo.GetComponent<IndicatorController_old>().isVMButton = panel.isVMButton;
			// Add it to the correspondent list, in order to update its data later
			if(!panel.isVMButton){
				semaforo.GetComponent<IndicatorController>().buttonID = id;
				id+=1;
				//dataScriptSemaforos.Add(semaforo.GetComponent<ScriptSemaforo>());
			} else {
				semaforo.GetComponent<IndicatorController>().buttonID = -666;
			}
			if (!isVM)
				normalSemaforos.Add(semaforo);
			else
				vmSemaforos.Add(semaforo);
				
			//i+=0.4f;
			i+=distance;
		}
	}
	
	private void ApplyColor(Image image, string newColor){
		// Parse the read color
		Color backColor = Color.clear; ColorUtility.TryParseHtmlString (newColor, out backColor);
		// Apply new color
		image.color = new Color(backColor.r, backColor.g, backColor.b, image.color.a);
	}
	
	public IEnumerator UpdateDataButtons()
	/* 
		This function update the data of the buttons depending of which list is active,
		the normal one, or the vm 
	*/
	{
		List<IndicatorController_old> auxList = null;
		
		/*if (usingVmSemaforos){
			auxList = ScriptList(vmSemaforos);
			//Debug.Log("Updateando semaforos virtuales");
		}
		else {*/
			auxList = ScriptList(normalSemaforos);
			//Debug.Log("Updateando semaforos normales");
		/*}*/
		
		foreach (IndicatorController_old script in auxList)
		{
			if(script.gameObject.activeSelf)
				yield return StartCoroutine(script.UpdateDataButton());
		}
	}
	
	private List<IndicatorController_old> ScriptList(List<GameObject> gameObjectList){
		List<IndicatorController_old> auxList = new List<IndicatorController_old>();
		foreach (GameObject semaforo in gameObjectList)
		{
			auxList.Add(semaforo.GetComponent<IndicatorController_old>());
		}
		return auxList;
	}
	
	// To swap between vmCanvas and normalCanvas
	public void IsVmCanvas(bool vmMode){
		foreach (GameObject semaforo in normalSemaforos)
			semaforo.SetActive(!vmMode);
		foreach (GameObject semaforo in vmSemaforos)
			semaforo.SetActive(vmMode);
		if (!vmMode)
			ApplyPanelSettings(normalPanelData);
		else
			ApplyPanelSettings(vmPanelData);
		
		usingVmSemaforos = vmMode;
	}
	
	public void ManageErrorPanel()
	{
		if (errorPanel.activeSelf)
			errorPanel.SetActive(false);
		else
			errorPanel.SetActive(true);
	}
	
	public bool ButtonWithIDHasBeenPressed(int buttonID)
	{
		if (scriptPanel==null)
			scriptPanel = listaElementos.GetComponent<ScriptPanelInfo>();
		
		if (scriptPanel.buttonActive == buttonID)
			HideInfo();
		else {
			if (scriptPanel.buttonActive == -1){
				panelInicial.SetActive(false);
				listaElementos.SetActive(true);
			}
			scriptPanel.buttonActive = buttonID;
			return true;
	   }
	   return false;
	}
	
	public void HideInfo()
	{
		listaElementos.SetActive(false);
		scriptPanel.buttonActive = -1;
		panelInicial.SetActive(true);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Constants;
using VMDC.Dtos;

public class IndicatorPanelController : MonoBehaviour
/*
	There is one script instance for each slot
*/
{
	
	private IndicatorPanelManager indicatorPanelManager;
	private IndicatorsPanelData indicatorsPanelData;
    private List<GameObject> indicatorsList = new List<GameObject>();

	public GameObject indicatorsSpawnPoint;

    public List<GameObject> indicatorsToDeactiveAtSTart;

	void Start() {
        foreach (GameObject indicator in indicatorsToDeactiveAtSTart)
        {
            indicator.SetActive(false);
        }
    }


    /// <summary>
    /// This function update the data of the buttons
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateDataButtons(List<KeyData> newKeyDataList) {
        foreach (GameObject indicator in indicatorsList)
		{
			// No need to pass all list, but is only 4 elements, so no problem
			yield return StartCoroutine(indicator.GetComponent<IndicatorReferences>().indicatorController.UpdateDataButton(newKeyDataList));
        }
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
			InstanceIndicatorsPanel(panelData.listSemaforos, type==VMDCTags.VIRTUAL_MACHINE_TAG);
			// Save panel data settings
			indicatorsPanelData = panelData;
            //Debug.Log("Panel instantiaded with:" + indicatorsList.Count);
            return panelData.schema;
		}
		return null;
	}

	private void InstanceIndicatorsPanel(List<SemaforoData> listSemaforos, bool isVM)
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
			GameObject semaforoPrefab = Resources.Load<GameObject>(VMDCPaths.indicatorPrefabPath);
			if (semaforoPrefab == null)
			{
				Debug.Log("ERROR: The semaforo prefab couldnt be loaded.");
			}
			GameObject indicator = Instantiate(semaforoPrefab,indicatorsSpawnPoint.transform) as GameObject;
            IndicatorReferences indicatorReferences = indicator.GetComponent<IndicatorReferences>();
            //indicator.transform.Translate(Vector3.left*i,Space.Self);
			indicator.name = panel.name;
			//semaforo.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>().text = panel.name;
			//?indicatorReferences.nombreElementoText = panel.name;
			//indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/"+panel.imageName);
			indicatorReferences.indicatorIconImage.sprite = Resources.Load<Sprite>("Sprites/"+panel.imageName);
			//indicator.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/"+panel.imageName+"_color");
			indicatorReferences.indicatorIconImageWithColor.sprite = Resources.Load<Sprite>("Sprites/"+panel.imageName+"_color");
            //indicator.GetComponent<IndicatorController>().keySemaforo = panel.key;
            indicatorReferences.indicatorController.keySemaforo = panel.key;
            //semaforo.GetComponent<IndicatorController>().isVMButton = panel.isVMButton;
            // Add it to the correspondent list, in order to update its data later
            //if(!panel.isVMButton){
            //indicator.GetComponent<IndicatorController>().buttonID = id;
            indicatorReferences.indicatorController.buttonID = id;
            id+=1;
				//dataScriptSemaforos.Add(semaforo.GetComponent<ScriptSemaforo>());
			/*} else {
				indicator.GetComponent<IndicatorController>().buttonID = -666;
			}*/
			indicatorsList.Add(indicator);
				
			//i+=0.4f;
			i+=distance;
		}
	}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AuxiliarMethods;
using VMDC.Dtos;
using VMDC.Constants;

/// <summary>
/// There is only one script instance. Has the info from the xml's.
/// </summary>
public class IndicatorPanelManager : MonoBehaviour
{
	
	private List<IndicatorsPanelData> panelInterfaceList;
	private List<SemaforoData> panelSemaforoList;
	private List<KeyModel> listKeyModel;
    //private ErrorManager errorManager;
    //public bool initialized = false;

    void OnEnable()
	{
		//errorManager = GameObject.FindWithTag("ErrorManager").GetComponent<ErrorManager>();
		panelSemaforoList = XML_extraMethods.LoadPanelSemaforoModels();
		panelInterfaceList = XML_extraMethods.LoadInterfacesSemaforosModels();
		listKeyModel = XML_extraMethods.LoadKeyValues();
		//UpdateKeyValueModels();
		//if (panelInterfaceList[0].schema == null)
		//	Debug.Log("Listo!");
		SetSemaforoDataToInterface();
        StaticDataHolder.IndicatorPanelManagerIsInitialized = true;
    }
	
	private void SetSemaforoDataToInterface()
	/*
		For each interface panel, we set the semaforos 
	*/
	{
		foreach (IndicatorsPanelData panel in panelInterfaceList)
		{
			foreach (SemaforoData semaforo in panel.listSemaforos)
			{
				// Get the model
				SemaforoData model = panelSemaforoList.Find(x => (x.id == semaforo.id));
				//Debug.Log("ID: "+semaforo.id);
				//Debug.Log("KEY: "+model.key);
				if (model==null){
					ErrorManager.NewErrorMessage("ERROR: No semaforo found for semaforo ID '"+semaforo.id+"'. Check if the semaforo exists in "+VMDCPaths.panelSemaforoModelsPath);
					break;
				}
				// Add new data
				semaforo.name = model.name;
				semaforo.key = model.key;
				semaforo.imageName = model.imageName;
				semaforo.application = model.application;
				semaforo.isVMButton = model.isVMButton;
				if (!semaforo.isVMButton)
					panel.schema.apiSchemaList.Add(new ApiSchema(model.application, GetKeyModelFromKeyID(model.key)));
				//else
				//	panel.schema.apiSchemaList.Add(new ApiSchema(null,null));
			}
		}
	}
	
	private KeyModel GetKeyModelFromKeyID(string keyID)
	{
		KeyModel model = listKeyModel.Find(x => x.id.Contains(keyID));
		if (model==null)
				ErrorManager.NewErrorMessage("ERROR: No keymodel found for key ID '"+keyID+"'. Check if the keymodel exists in "+VMDCPaths.KeyValueModelsPath);
		return model;
	}
	
	
	public IndicatorsPanelData GetPanelData(string type)
	/*
		This function allows the PanelSemaforo to know his settings depending of his type
	*/
	{
		IndicatorsPanelData data = panelInterfaceList.Find(x => x.id.Contains(type));
		if (data==null)
				ErrorManager.NewErrorMessage("ERROR: No info found for panel slot type '"+type+"'. Check if the type exists in "+VMDCPaths.interfacesSemaforoModelsPath);
		return data;
	}
	
	public void UpdateKeyValueModels()
	{
		listKeyModel = XML_extraMethods.LoadKeyValues();
	}

}

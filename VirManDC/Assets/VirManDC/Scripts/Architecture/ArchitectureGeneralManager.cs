// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

	/// <summary>
    /// This class is used to get the raw data of the architecture, and to
    /// reload it if necesary. This class also provide several useful functions,
    /// like setObjectsFromData.
    /// 
    /// To check how the data is readed, check ArchitectureXmlManager.
    /// To check how the data is applied to the GameObjects, check ArchitectureObjectsManager.
    /// </summary>
public class ArchitectureGeneralManager : MonoBehaviour
{	
	//[Header("Link to the Manager for objects adjustments")]
	private ArchitectureObjectsManager objectsManager;
	private ArchitectureXmlManager xmlManager;
	private ArchitectureRawData architectureRawData;
	
	//private ErrorManager errorManager;
	private LogInUIElementsController loginController;
	
    public bool Initialize()
	{
		objectsManager = GetComponent<ArchitectureObjectsManager>();
		// <-> errorManager = GameObject.FindWithTag("ErrorManager").GetComponent<ErrorManager>();
		// <-> loginController = GameObject.FindWithTag("LogInController").GetComponent<LogInUIElementsController>();
		
		//xmlManager = new ArchitectureXmlManager(errorManager, loginController);
		xmlManager = new ArchitectureXmlManager(loginController);
		Debug.Log("Loading...");
		LoadArchitectureData();
		Debug.Log("Loaded");
		// If there was a problem with the XML Manager, data is null
		return !(architectureRawData == null);
	}
	
	public void LoadArchitectureData()
	{
		architectureRawData = xmlManager.LoadArchitectureFromXml();
	}
	
	public ArchitectureRawData GetArchitectureRawData()
	{
		return architectureRawData;
	}
	
	public void SetObjectsFromData()
	{
		objectsManager.SetObjectsFromRawData(architectureRawData);
	}
}

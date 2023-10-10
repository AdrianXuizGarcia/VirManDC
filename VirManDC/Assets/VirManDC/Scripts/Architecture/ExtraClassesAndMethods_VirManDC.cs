// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using VMDC.Dtos;
using VMDC.Constants;

namespace AuxiliarMethods
{

	public static class XML_extraMethods
	{
		//private string KeyValueModelsPath = "ConfigurationFiles/KeyValueModels.xml";
		
		public static List<KeyModel> LoadKeyValues()
		/* 
			Load and return the list of the KeyValueModels from the XML
		*/
		{
			XmlReader reader = XmlReader.Create(VMDCPaths.KeyValueModelsPath);
			List<KeyModel> keyModels = new List<KeyModel>();
			
			while (reader.Read())
			{
				// Read Model: first the attributes
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "keyvaluemodel"){
					KeyModel keyModel = new KeyModel();
					keyModel.id = reader.GetAttribute(0);
					// For the rest of the elements, we read each node
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "key")
							keyModel.key =getItemFromReader(reader);
						if (reader.Name.ToLower() == "toplimit")
							keyModel.topLimit = float.Parse(getItemFromReader(reader),CultureInfo.InvariantCulture);
						/*if (reader.Name.ToLower() == "scalerack1u")
							modelData.scalerack1u = float.Parse(getItemFromReader(reader),CultureInfo.InvariantCulture);*/
					}
					keyModels.Add(keyModel);
					reader.Read();
				}
			}		
			return keyModels;
		}
		
		private static string getItemFromReader (XmlReader reader)
		/* Auxiliar function for LoadKeyValues */
		{
			string item = "Not a value";
			
			while (reader.NodeType != XmlNodeType.EndElement){
				reader.Read();
				if (reader.NodeType == XmlNodeType.Text)
				{
					item = reader.Value;
				}
			}
			reader.Read();
			return item;
		}
		
		/// <summary>
        /// Load and return the list of the PanelSemaforoModels from the XML
        /// </summary>
        /// <returns></returns>
		public static List<SemaforoData> LoadPanelSemaforoModels()
		{
			XmlReader reader = XmlReader.Create(VMDCPaths.panelSemaforoModelsPath);
			List<SemaforoData> panelSemaforoList = new List<SemaforoData>();
			
			while (reader.Read())
			{
				// Read Model: first the attributes
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "indicatorpanel"){
					SemaforoData semaforoData = new SemaforoData(int.Parse(reader.GetAttribute(0)));
					//semaforoData.id = int.Parse(reader.GetAttribute(0));
					// For the rest of the elements, we read each node
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "name")
							semaforoData.name =getItemFromReader(reader);
						if (reader.Name.ToLower() == "application")
							semaforoData.application = getItemFromReader(reader);
						if (reader.Name.ToLower() == "key")
							semaforoData.key = getItemFromReader(reader);
						if (reader.Name.ToLower() == "imagename")
							semaforoData.imageName = getItemFromReader(reader);
					}
					panelSemaforoList.Add(semaforoData);
					//Debug.Log("ID xml: "+semaforoData.key);
					reader.Read();
				}
			}		
			return panelSemaforoList;
		}

		/// <summary>
        /// Load and return the list of the ZabbixScript from the XML
        /// </summary>
        /// <returns></returns>
		public static List<ZabbixScriptData> LoadZabbixScriptList()
		{
			XmlReader reader = XmlReader.Create(VMDCPaths.zabbixScriptListPath);
			List<ZabbixScriptData> zabbixScriptList = new List<ZabbixScriptData>();
			
			while (reader.Read())
			{
				// Read Model: first the attributes
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "zabbixscript"){
					ZabbixScriptData zabbixScriptData = new ZabbixScriptData();
					zabbixScriptData.scriptid = int.Parse(reader.GetAttribute(0));
					// For the rest of the elements, we read each node
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "showname")
							zabbixScriptData.scripshowname =getItemFromReader(reader);
					}
					zabbixScriptList.Add(zabbixScriptData);
					reader.Read();
				}
			}		
			return zabbixScriptList;
		}
		
		
		public static List<IndicatorsPanelData> LoadInterfacesSemaforosModels()
		/* 
			Load and return the list of the InterfacesSemaforosModels from the XML
			0 means no semaforo to display
		*/
		{
			XmlReader reader = XmlReader.Create(VMDCPaths.interfacesSemaforoModelsPath);
			List<IndicatorsPanelData> indicatorsPanelDataList = new List<IndicatorsPanelData>();
			
			while (reader.Read())
			{
				// Read Model: first the attributes
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "paneldata"){
					IndicatorsPanelData panelSemaforoData = new IndicatorsPanelData();
					panelSemaforoData.id = reader.GetAttribute(0);
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						/*if (reader.Name.ToLower() == "panelbackgroundcolor")
							panelSemaforoData.panelBackgroundColor = getItemFromReader(reader);
						if (reader.Name.ToLower() == "screenbackgroundcolor")
							panelSemaforoData.screenBackgroundColor = getItemFromReader(reader);*/
						// For each semaforo, we add a new Semaforo to the list of the interface
						if (reader.Name.ToLower() == "indicator1"){
							int id = int.Parse(getItemFromReader(reader));
							if (id!=0)
								panelSemaforoData.listSemaforos.Add(new SemaforoData(id));
						}
						if (reader.Name.ToLower() == "indicator2"){
							int id = int.Parse(getItemFromReader(reader));
							if (id!=0)
								panelSemaforoData.listSemaforos.Add(new SemaforoData(id));
						}
						if (reader.Name.ToLower() == "indicator3"){
							int id = int.Parse(getItemFromReader(reader));
							if (id!=0)
								panelSemaforoData.listSemaforos.Add(new SemaforoData(id));
						}
						if (reader.Name.ToLower() == "indicator4"){
							int id = int.Parse(getItemFromReader(reader));
							if (id!=0)
								panelSemaforoData.listSemaforos.Add(new SemaforoData(id));
						}
					}
					reader.Read();
					indicatorsPanelDataList.Add(panelSemaforoData);
				}
			}		
			return indicatorsPanelDataList;
		}
		
	}
	
	
	public static class Files_extraMethods
	{
		public static bool CheckOrCreateDirectoryAuxFiles()
		/*
			Check if the directory for essential files exists. If it cannot be created, returns false
		*/
		{
			if (Directory.Exists(VMDCPaths.extraFilesDirPath))
				return true;
			else {
				try {
					Directory.CreateDirectory(VMDCPaths.extraFilesDirPath);
					return true;
				} catch (Exception e) {
					Debug.Log("The process failed: "+ e.ToString());
					return false;
				}
			}
		}
		
		public static string CheckIfAuxFilesMissing()
		/*
			Check if all the necessary files are found in the correct path. If any is missing, returns its path
		*/
		{
			string path = Application.persistentDataPath;
			
			if (!File.Exists(VMDCPaths.architecturePath))
				return VMDCPaths.architecturePath;
			if (!File.Exists(VMDCPaths.modelsRackPath))
				return VMDCPaths.modelsRackPath;
			if (!File.Exists(VMDCPaths.KeyValueModelsPath))
				return VMDCPaths.KeyValueModelsPath;
			if (!File.Exists(VMDCPaths.zabbixScriptListPath))
				return VMDCPaths.zabbixScriptListPath;
			if (!File.Exists(VMDCPaths.panelSemaforoModelsPath))
				return VMDCPaths.panelSemaforoModelsPath;
			if (!File.Exists(VMDCPaths.interfacesSemaforoModelsPath))
				return VMDCPaths.interfacesSemaforoModelsPath;
			//if (!File.Exists(VMDCPaths.configFilePath))
			//	return VMDCPaths.configFilePath;
			return "";
		}
		
		public static string CheckIfConfigurationFileMissing()
		{
			if (!File.Exists(VMDCPaths.configFilePath))
				return VMDCPaths.configFilePath;
			if (!File.Exists(VMDCPaths.defaultConfigFilePath))
				return VMDCPaths.defaultConfigFilePath;
			return "";
		}
	}
}

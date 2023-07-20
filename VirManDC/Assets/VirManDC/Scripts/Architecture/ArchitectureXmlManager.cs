using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using VMDC.Dtos;
using VMDC.Constants;
using System.IO;

/// <summary>
/// This class is used ONLY to read the XML file and return the data it contains 
/// to the main program.
/// 
/// To access the data and general functions, check ArchitectureGeneralManager. 
/// To check how the data is applied to the GameObjects, check ArchitectureObjectsManager.
/// </summary>
public class ArchitectureXmlManager 
{
	//private string architecturePath = "ExtraFiles/Architecture.xml";
	//private string modelsRackPath = "ExtraFiles/RackDataModels.xml";
	//private ErrorManager errorManager;
	private LogInUIElementsController loginController;
	
	public ArchitectureXmlManager(LogInUIElementsController lC)
	{
		//errorManager = eM;
		loginController = lC;
	}
	
	/// <summary>
    /// Load the architecture of the racks site.
    /// First we search the PlaceDescription, then each one of the racks,
    /// and for each rack, the slots that are in it.
    /// 
    /// Everything will be in a ArchitectureRawData that will be returned to the
    /// main program.
    /// This function only reads data and put it on the ArchitectureData object.
    /// </summary>
	public ArchitectureRawData LoadArchitectureFromXml()
	{
		ArchitectureRawData data = new ArchitectureRawData();
		LoadRackModels(data);
        //Debug.Log(VMDCPaths.architecturePath);
        //XmlReader reader = XmlReader.Create(VMDCPaths.architecturePath);
		XmlReader reader = XmlReader.Create(Path.Combine(VMDCPaths.extraFilesDirPath,StaticDataHolder.architectureName));
		// <-> ErrorManager errorManager = GameObject.FindWithTag("ErrorManager").GetComponent<ErrorManager>();
		
		try 
		{
			while (reader.Read())
			{
				//Debug.Log(reader.Name);
				// Read first line. Contains the place definition (size)
				/*if (reader.Name.ToLower() == "placedescription") {
						data.scalePosX = float.Parse(reader.GetAttribute(0),CultureInfo.InvariantCulture);
						data.scaleX = float.Parse(reader.GetAttribute(1),CultureInfo.InvariantCulture);
						data.scalePosZ = float.Parse(reader.GetAttribute(2),CultureInfo.InvariantCulture);
						data.scaleZ = float.Parse(reader.GetAttribute(3),CultureInfo.InvariantCulture);
						data.rotPosX = float.Parse(reader.GetAttribute(4),CultureInfo.InvariantCulture);
						data.rotPosZ = float.Parse(reader.GetAttribute(5),CultureInfo.InvariantCulture);
				}
				// Read the camera seetings
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "camerasettings") {
					CameraSettings cameraSettings = new CameraSettings();
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "posx")
							cameraSettings.posX = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "posy")
							cameraSettings.posY = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "posz")
							cameraSettings.posZ = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "rotation")
							cameraSettings.rotation = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
					}
					data.cameraSettings = cameraSettings;
				}*/
				// If isnt a rack
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "other"){
					OtherDto other = new OtherDto();
					other.otherID = int.Parse(reader.GetAttribute(0));
					other.name = reader.GetAttribute(1);
					other.model = reader.GetAttribute(2);
					other.graphics = reader.GetAttribute(3);
					// For the rest of the elements, we read each node
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "posx")
							other.posX = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "posy")
							other.posY = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "posz")
							other.posZ = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "rotation")
							other.rotation = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
					}
					data.others.Add(other);
					reader.Read();
				}
				// Read Racks: first the attributes
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "rack"){
					RackDto rack = new RackDto();
					rack.rackID = int.Parse(reader.GetAttribute(0));
					rack.name = reader.GetAttribute(1);
					rack.model = reader.GetAttribute(2);
					rack.modelB = reader.GetAttribute(3);
					rack.graphicsM = reader.GetAttribute(4);
					rack.graphicsBM = reader.GetAttribute(5);
					// For the rest of the elements, we read each node
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "posx")
							rack.posX = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "posy")
							rack.posY = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "posz")
							rack.posZ = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "rotation")
							rack.rotation = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
					}
					reader.Read();
					// Read Slots: first the attributes
					while (reader.NodeType != XmlNodeType.EndElement) {
						if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "slot"){
							RackSlotDto slot = new RackSlotDto();
							slot.slotNum = int.Parse(reader.GetAttribute(0));
							slot.name = reader.GetAttribute(1);
							/*// Special case size: if size is 1/2, we put a 0
							string size = reader.GetAttribute(2);
							if (size=="1/2") slot.size=0; else slot.size = int.Parse(size);*/
							slot.size = int.Parse(reader.GetAttribute(2));
							slot.model = reader.GetAttribute(3);
							slot.type = reader.GetAttribute(4);
							slot.isHypervisor = bool.Parse(reader.GetAttribute(5));
							// For the rest of the elements, we read each node
							while (reader.NodeType != XmlNodeType.EndElement){
								reader.Read();
								if (reader.Name.ToLower() == "slotid"){
									slot.slotID = GetItemFromReader(reader);
								}
								if (reader.Name.ToLower() == "posy")
									slot.posY = int.Parse(GetItemFromReader(reader));
							}
							rack.rackSlots.Add(slot);
						}//end slot if
						reader.Read();
					}//end slot while
					data.racks.Add(rack);
				}//end rack if
			}//end while
			return data;
		} catch (XmlException e)
		{
			ErrorManager.NewErrorMessage("Error while reading '"+VMDCPaths.architecturePath+"':\n"+e.Message);
			loginController.DeactivateInitialButtons();
			return null;
		}
	}
	
	private string GetItemFromReader (XmlReader reader)
	/* Auxiliar function for LoadArchitecture */
	{	
		string item = String.Empty;
		
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
	
	private void LoadRackModels(ArchitectureRawData data)
		/* 
		Load the necessary data to set the racks correctly, depending
		of what rack model is.
		This function only reads data and put it on the ArchitectureData object.
	*/
	{
		try
		{
			XmlReader reader = XmlReader.Create(VMDCPaths.modelsRackPath);
			while (reader.Read())
			{
				// Read Racks: first the attributes
				if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "rackmodel"){
					RackModelData modelData = new RackModelData();
					modelData.model = reader.GetAttribute(0);
					// For the rest of the elements, we read each node
					while (reader.NodeType != XmlNodeType.EndElement){
						reader.Read();
						if (reader.Name.ToLower() == "spaceinitservers1u")
							modelData.espacioInicioServers1u = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "space1uy")
							modelData.espacio1uY = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "spacerackx")
							modelData.espacioRackX = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "spacerackz")
							modelData.espacioRackZ = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "spaceinitcanvas1u")
							modelData.espacioInicioCanvas1u = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);
						if (reader.Name.ToLower() == "sizeyrack")
							modelData.size = int.Parse(GetItemFromReader(reader));
						/*if (reader.Name.ToLower() == "scalerack1u")
							modelData.scalerack1u = float.Parse(GetItemFromReader(reader),CultureInfo.InvariantCulture);*/
					}
					data.modelsRack.Add(modelData);
					reader.Read();
				}
			}
		} catch (Exception e)
		{
			ErrorManager.NewErrorMessage("Error while reading '"+VMDCPaths.modelsRackPath+"':\n"+e.Message);
			loginController.DeactivateInitialButtons();
		}
		//return data;
	}
}
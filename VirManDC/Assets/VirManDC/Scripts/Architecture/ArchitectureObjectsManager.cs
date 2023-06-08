using System;
using System.Collections;
using UnityEngine;
using VMDC.Dtos;
using VMDC.Constants;
using System.Collections.Generic;

/// <summary>
/// This class is used ONLY to apply the given data to the GameObjects of
/// the main program, which are 2 steps.
/// 
/// The first one is to enter the data from racks and slots (IP's, names...)
/// into the correct data script (like SlotDataAndControl). For this, we use
/// the data found in the RawArchitectureData and HostsData from the 
/// SlotDataFromAPI_Manager (NOTE: If there is no conection with the Zabbix Server,
/// the slots will still be generated, but with no contents).
/// The second one is to use the part of the data that define types, positions,
/// rotations... into the GameObject's Transforms.
/// 
/// To see how to access the raw architecture data and general functions, check ArchitectureGeneralManager. 
/// To check how the data is readed, check ArchitectureXmlManager.
/// </summary>
public class ArchitectureObjectsManager : MonoBehaviour
{
    public static ArchitectureObjectsManager Instance;
	public static event Action<RackBasicData> OnRackInstantiationEnd;
	public static event Action<Transform> OnRackPicked;
    private const string slotPrefix = "slotAnim_";
	//private const string slotsSpritesPath = "SlotSprites/";

    private Sprite[] sprites;
	
	private SlotDataFromAPI_Manager slotDFAM;

    //public GameObject floor;
    //public GameObject mainCamera;	

    public GameObject baseAsParentForRacks;
	public Transform originPointToSpawn;
    public GameObject scalePivotBase;
	public GameObject rotationPivotBase;

    private ArchitectureRawData rawData;

    void Start()
	{
		sprites = Resources.LoadAll<Sprite>(VMDCPaths.slotsSpritesPath);
		slotDFAM = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>();
	}

	void Awake(){
        Instance = this;
    }
	
	
	public void SetObjectsFromRawData(ArchitectureRawData data)
	{
        // Get the HostData from the Api
        // <-> yield return StartCoroutine(slotDFAM.UpdateHostsData());

        // Move and scale up the floor to match the racks
        scalePivotBase.transform.localPosition = new Vector3(data.scalePosX, scalePivotBase.transform.localPosition.y, data.scalePosZ);
        scalePivotBase.transform.localScale = new Vector3(data.scaleX, scalePivotBase.transform.localScale.y, data.scaleZ);

		// Move the rotation pivot of the base
		rotationPivotBase.transform.localPosition = new Vector3(data.rotPosX, rotationPivotBase.transform.localPosition.y, data.rotPosZ);
        // Apply the camera settings (if readed correctly)
        // <-> ApplyCameraSettings(data);

        try {
            rawData = data;

            foreach (OtherDto other in data.others)
            {
                // We get the RackModelData from the ArchitectureRawData of the rack's model
                RackModelData other_model_data = data.modelsRack.Find(x => x.model.Contains(other.model));
                if (other_model_data == null)
                    ErrorManager.NewErrorMessageThrowException("No info found for model '" + other.model + "'. Check the file '" + VMDCPaths.modelsRackPath + "'.");

                // Set the position and rotation of the item, and instantiate it
                Vector3 other_pos = new Vector3(other.posX * other_model_data.espacioRackX, other.posY, other.posZ * other_model_data.espacioRackZ)+originPointToSpawn.position;
                Quaternion other_rot = Quaternion.Euler(0, other.rotation, 0);

                GameObject other_instanciado = InstantiateOther(other, other_pos, other_rot,baseAsParentForRacks.transform);

                // Change the name of the GameObject
                //other_instanciado.transform.Find ("rack_Cube/Canvas/Text").GetComponent<Text>().text = other.otherID.ToString();
                other_instanciado.name = other.name;
            }

            foreach (RackDto rackData in data.racks)
            {
                // We get the RackModelData from the ArchitectureRawData of the rack's model
                RackModelData rack_model_data = data.modelsRack.Find(x => x.model.Contains(rackData.model));
                if (rack_model_data == null)
                    ErrorManager.NewErrorMessageThrowException("No info found for model '" + rackData.model + "'. Check the file '" + VMDCPaths.modelsRackPath + "'.");

                // Set the position and rotation of the rack, and instantiate it
                Vector3 rack_pos = new Vector3(rackData.posX * rack_model_data.espacioRackX, rackData.posY, rackData.posZ * rack_model_data.espacioRackZ)+originPointToSpawn.position;
                //Debug.Log(rackData.name +":"+  rackData.posX* rack_model_data.espacioRackX+":" + rackData.posZ* rack_model_data.espacioRackZ);
                Quaternion rack_rot = Quaternion.Euler(0, rackData.rotation, 0);

                GameObject rack_instanciado = InstantiateRack(rackData, rack_pos, rack_rot,baseAsParentForRacks.transform);

                // Change the name of the GameObject (useful for debugging, not necessary)
                // --> Only works for old prefab, maybe useful for new changes 
                //rack_instanciado.transform.Find ("rack_Cube/Canvas/Text").GetComponent<Text>().text = rack.rackID.ToString();
                rack_instanciado.name = rackData.name;

                rack_instanciado.transform.GetChild(0).GetComponent<DataBasicRackController>().rackID = rackData.rackID;

                // Create a filledSlots list to calculate later the filling with covers
                //List<bool> emptySlotsList = new List<bool>(rack_model_data.size);
                bool[] filledSlotsList = new bool[rack_model_data.size];

				Transform spawnSlotTransform = rack_instanciado.transform.GetChild(2).transform.GetChild(0).transform;

                foreach (RackSlotDto slot in rackData.rackSlots)
                {
                    // Instantiate the slot within the rack position
                    GameObject slot_instance = InstantiateSlot(slot, spawnSlotTransform);
                    //Debug.Log(rack_instanciado.transform.GetChild(2).transform);
                    // Set the slot on the "begining" Y (which is not the same as the rack)
                    slot_instance.transform.Translate(Vector3.up * rack_model_data.espacioInicioServers1u);
                    // Translate upwards the slot to his Y position
                    slot_instance.transform.Translate(Vector3.up * slot.posY * (rack_model_data.espacio1uY));
					// Adjustment for the scale
                    ////slot_instance.transform.Translate(Vector3.down * (rack_model_data.espacioInicioServers1u / rack_model_data.size));
                    //// Move the canvas to the correct position
                    ////GameObject canvas_instance = slot_instance.transform.GetChild(0).GetComponent<SlotRackInteraction>().GetPanelCanvas();
                    ////canvas_instance.transform.position += new Vector3(0, rack_model_data.espacioInicioCanvas1u * slot.size, 0);
                    // Set the slot as a child of the rack
                    slot_instance.transform.SetParent(spawnSlotTransform);
                    // Mark the slots filled
                    for (int i = 0; i < slot.size; i++)
                    {
                        filledSlotsList[i + slot.posY] = true;
                    }
                    // Set data from architecture into the rack data controller
                    slot_instance.GetComponentInChildren<SlotData>().SetArchitectureData(slot);
                }

                // Fill empty slots with covers
                FillEmptySlotsOfRack(filledSlotsList, spawnSlotTransform, rack_model_data);

				// Instantiation is over, so we send the signal to the suscribers
                RackBasicData rackBasicData = new RackBasicData(rack_instanciado.transform, rack_instanciado.name);
				OnRackInstantiationEnd?.Invoke(rackBasicData);
                //OnRackInstantiationEnd?.Invoke(rack_instanciado.transform);
            }

        } catch (Exception e) {Debug.Log(e);}
    }
	
	private GameObject InstantiateOther(OtherDto other, Vector3 other_pos, Quaternion other_rot,Transform parent)
	/* 
		This function control the Instantiate of the item and returns null + Error message if something went wrong.
		Resources.Load() returns null if the GameObject is not found. 
		The name of the object must be "otherModel_definition". 
	*/
	{
		GameObject other_prefab = Resources.Load<GameObject>(VMDCPaths.othersModelsPath+other.model+"_"+other.graphics);
		if (other_prefab == null)
		{
			ErrorManager.NewErrorMessageThrowException("The model '"+other.model+"_"+other.graphics+"' was not found in the path '"+VMDCPaths.othersModelsPath+"'.");
			return null;
		}
		return Instantiate(other_prefab,other_pos,other_rot,parent) as GameObject;
	}
	
	private GameObject InstantiateRack(RackDto rackData, Vector3 rack_pos, Quaternion rack_rot, Transform parent)
	/* 
		This function control the Instantiate of the rack and returns null + Error message if something went wrong.
		Resources.Load() returns null if the GameObject is not found. 
		The name of the object must be "rackModel_definition".
	*/
	{
		GameObject rack_prefab = Resources.Load<GameObject>(VMDCPaths.racksModelsPath+rackData.model+"_"+rackData.graphicsM);
		if (rack_prefab == null)
		{
			ErrorManager.NewErrorMessageThrowException("The model '"+rackData.model+"_"+rackData.graphicsM+"' was not found in the path '"+VMDCPaths.racksModelsPath+"'.");
			return null;
		}
		return Instantiate(rack_prefab,rack_pos,rack_rot,parent) as GameObject;
	}
	
	
	private GameObject InstantiateSlot(RackSlotDto slot_data, Transform parentTransform)
	/* 
		This function control the Instantiate of the slot and returns null + Error message if something went wrong.
		Resources.Load() returns null if the GameObject is not found. 
		The name of the object must be "rackSlotPrefix_sizeU".
		Beware: changes on the slotPrefab structure may need adjustements here.
	*/
	{
	    Sprite spriteAssigned = null;
        /*
		// ----> This way is to instantiate diferent slots_rack models. Use this when using models that are diferent on hardware <---
		string size = slot_data.size.ToString();
		if (slot_data.size == 0) size = "1-2";
		GameObject slot_prefab = Resources.Load<GameObject>(VMDCPaths.slotsModelsPath+slotPrefix+size+"U");
		if (slot_prefab == null)
		{
			errorManager.NewErrorMessage("ERROR: The model '"+slotPrefix+size+"U"+"' couldnt be found in the path \""+VMDCPaths.slotsModelsPath+"\".");
			return null;
		}
		GameObject slot_instance = Instantiate(slot_prefab,rackTransform) as GameObject; // leave
		slot_instance.name = slot_data.name;*/

        // -----> this way is changing the scale of the slot_rack, always same model <----
        ////GameObject slot_prefab = Resources.Load<GameObject>(VMDCPaths.slotsModelsPath+"slotAnim_1-2U");
        string nameServer = "BasicModel_Slot";
        GameObject slot_prefab = Resources.Load<GameObject>(VMDCPaths.slotsModelsPath+nameServer);
		if (slot_prefab == null)
		{
			ErrorManager.NewErrorMessageThrowException("The model '"+nameServer+"' couldnt be found in the path \""+VMDCPaths.slotsModelsPath+"\".");
			return null;
		}
		GameObject slot_instance = Instantiate(slot_prefab,parentTransform) as GameObject;
        slot_instance.name = slot_data.name;
		
		// We scale up the "graphic" part of the rack to match his size
		Vector3 slotScale = slot_instance.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform.localScale;
		slot_instance.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform.localScale = new Vector3(1,slotScale.y * slot_data.size,1);
		//Debug.Log(slotScale);
        ////slot_instance.transform.GetChild(1).GetChild(0).gameObject.transform.localScale = new Vector3(1,slot_data.size,1);
        // ---> end diferent ways <---

        // Asign the sprite correspondent to the model
        SpriteRenderer spriteRenderer = slot_instance.transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
		foreach (Sprite i in sprites)
		{
			if (i.name == slot_data.model)
			{
				spriteAssigned = i;
				break;
			}
		}
		
		if (spriteAssigned == null)
			ErrorManager.NewErrorMessageThrowException("The model '"+slot_data.model+"' couldnt be found.");
		else
			spriteRenderer.sprite = spriteAssigned;
		
		// Asign slot data to SlotDataAndControl script
		////AsignSlotData(slot_instance.GetComponent<SlotDataAndControl>(),slot_data);
		
		// Asign the canvas correspondent to the slot type
		//AsignCanvasFromType(slot_instance,slot_data);

		return slot_instance;
	}

	/*private void AsignSlotData(SlotDataAndControl slot, RackSlotDto slot_data)
		Asign slot data from RackSlotDto and HostsData to SlotDataAndControl component.
	
	{
		slot.slotName = slot_data.name;
		slot.slotNum = slot_data.slotNum;
		slot.type = slot_data.type;
		
		// Depending of the type of id, we set IP or hostID
		String slotID = slot_data.slotID;
		if (slotID.Contains("."))
			slot.ip = slotID;
		else
			slot.hostID = slotID;
		
		// Get the data from the API
		///slotDFAM.SetHostsDataToSlotData(slot, slot.hostID, slot.ip);
	}*/
	
	/*private void ApplyCameraSettings(ArchitectureRawData data)
	{
		if (data.cameraSettings!=null) {
			mainCamera.transform.position = new Vector3(data.cameraSettings.posX, data.cameraSettings.posY, data.cameraSettings.posZ);
			mainCamera.transform.Rotate(new Vector3(0, data.cameraSettings.rotation, 0));	
		} else 
			ErrorManager.NewErrorMessage("The camera data Settings could not be readed.");
	}*/
	
	private void FillEmptySlotsOfRack(bool[] filledSlotsList,Transform parentTransform, RackModelData rack_model_data)
	{
        string coverName = "BasicModel_Cover";
        // Instantiate each empty slot
        for(int i=0;i<filledSlotsList.Length;i++)
		{
			if (!filledSlotsList[i])
			{
				// Instantiate the slot within the rack position
				GameObject slot_prefab = Resources.Load<GameObject>(VMDCPaths.slotsModelsPath+coverName);
				if (slot_prefab == null)
				{
					ErrorManager.NewErrorMessageThrowException("The model '"+coverName+"' couldnt be found in the path \""+VMDCPaths.slotsModelsPath+"\".");
				}
				GameObject slot_instance = Instantiate(slot_prefab,parentTransform) as GameObject;
				slot_instance.name = "cover";
				// Set the slot on the "begining" Y (which is not the same as the rack)
				slot_instance.transform.Translate (Vector3.up * rack_model_data.espacioInicioServers1u);
				// Translate upwards the slot to his Y position
				slot_instance.transform.Translate (Vector3.up * i * (rack_model_data.espacio1uY));
				// Set the slot as a child of the rack
				slot_instance.transform.SetParent(parentTransform);
			}
		}
	}





	public void SetPickedRack(int rackID)
	{
        // Get the HostData from the Api
        // <-> yield return StartCoroutine(slotDFAM.UpdateHostsData());
        // Scale up the floor to match the racks
        // <-> floor.transform.localScale = new Vector3(data.sizeX, floor.transform.localScale.y, data.sizeZ);
        // Apply the camera settings (if readed correctly)
        // <-> ApplyCameraSettings(data);

        try {
			// We get the RackModelData from the ArchitectureRawData of the rack's model
			RackDto rackData = rawData.racks.Find(x => x.rackID==rackID);

			// Set the position and rotation of the rack, and instantiate it
			Vector3 rack_pos = originPointToSpawn.position;

            //Debug.Log(rackData.name +":"+  rackData.posX* rack_model_data.espacioRackX+":" + rackData.posZ* rack_model_data.espacioRackZ);
            Quaternion rack_rot = originPointToSpawn.rotation;

            GameObject rack_instanciado = InstantiateRack2(rackData, rack_pos, rack_rot,originPointToSpawn.transform);

			// Change the name of the GameObject (useful for debugging, not necessary)
			// --> Only works for old prefab, maybe useful for new changes 
			//rack_instanciado.transform.Find ("rack_Cube/Canvas/Text").GetComponent<Text>().text = rack.rackID.ToString();
			rack_instanciado.name = rackData.name;

			// Create a filledSlots list to calculate later the filling with covers
			RackModelData rack_model_data = rawData.modelsRack.Find(x => x.model.Contains(rackData.model));
			bool[] filledSlotsList = new bool[rack_model_data.size];

			foreach (RackSlotDto slot in rackData.rackSlots)
			{
				// Instantiate the slot within the rack position
				GameObject slot_instance = InstantiateSlot2(slot, rack_instanciado.transform.GetChild(2).transform);
				//Debug.Log(rack_instanciado.transform.GetChild(2).transform);
				// Set the slot on the "begining" Y (which is not the same as the rack)
				slot_instance.transform.Translate(Vector3.up * rack_model_data.espacioInicioServers1u);
				// Translate upwards the slot to his Y position
				slot_instance.transform.Translate(Vector3.up * slot.posY * (rack_model_data.espacio1uY));
				//// Move the canvas to the correct position
				////GameObject canvas_instance = slot_instance.transform.GetChild(0).GetComponent<SlotRackInteraction>().GetPanelCanvas();
				////canvas_instance.transform.position += new Vector3(0, rack_model_data.espacioInicioCanvas1u * slot.size, 0);
				// Set the slot as a child of the rack
				slot_instance.transform.SetParent(rack_instanciado.transform.GetChild(2).transform);
				// Mark the slots filled
				for (int i = 0; i < slot.size; i++)
				{
					filledSlotsList[i + slot.posY] = true;
				}

			}

			// Fill empty slots with covers
			FillEmptySlotsOfRack(filledSlotsList, rack_instanciado.transform.GetChild(2).transform, rack_model_data);

			// Instantiation is over, so we send the signal to the suscribers
			RackBasicData rackBasicData = new RackBasicData(rack_instanciado.transform, rack_instanciado.name);
			// TODO: Guardar la transform de la ubicacion del rack, no de la base
			OnRackPicked?.Invoke(originPointToSpawn.transform);
			//OnRackInstantiationEnd?.Invoke(rack_instanciado.transform);

        } catch (Exception e) {Debug.Log(e);}
    }

	private GameObject InstantiateRack2(RackDto rackData, Vector3 rack_pos, Quaternion rack_rot, Transform parent)
	/* 
		This function control the Instantiate of the rack and returns null + Error message if something went wrong.
		Resources.Load() returns null if the GameObject is not found. 
		The name of the object must be "rackModel_definition".
	*/
	{
		GameObject rack_prefab = Resources.Load<GameObject>(VMDCPaths.racksModelsPath+rackData.model+"_"+rackData.graphicsM);
		if (rack_prefab == null)
		{
			ErrorManager.NewErrorMessage("The model '"+rackData.model+"_"+rackData.graphicsM+"' was not found in the path '"+VMDCPaths.racksModelsPath+"'.");
			return null;
		}
		return Instantiate(rack_prefab,rack_pos,rack_rot,parent) as GameObject;
	}

	private GameObject InstantiateSlot2(RackSlotDto slot_data, Transform parentTransform)
	/* 
		This function control the Instantiate of the slot and returns null + Error message if something went wrong.
		Resources.Load() returns null if the GameObject is not found. 
		The name of the object must be "rackSlotPrefix_sizeU".
		Beware: changes on the slotPrefab structure may need adjustements here.
	*/
	{
	    Sprite spriteAssigned = null;

        string nameServer = "Server";
        GameObject slot_prefab = Resources.Load<GameObject>(VMDCPaths.slotsModelsPath+nameServer);
		if (slot_prefab == null)
		{
			ErrorManager.NewErrorMessage("The model '"+nameServer+"' couldnt be found in the path \""+VMDCPaths.slotsModelsPath+"\".");
			return null;
		}
		GameObject slot_instance = Instantiate(slot_prefab,parentTransform) as GameObject;
        slot_instance.name = slot_data.name;
		
		// We scale up the "graphic" part of the rack to match his size
		slot_instance.transform.GetChild(0).gameObject.transform.localScale = new Vector3(1,slot_data.size,1);
		// ---> end diferent ways <---
		
		// Asign the sprite correspondent to the model
		SpriteRenderer spriteRenderer = slot_instance.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
		foreach (Sprite i in sprites)
		{
			if (i.name == slot_data.model)
			{
				spriteAssigned = i;
				break;
			}
		}
		
		if (spriteAssigned == null)
			ErrorManager.NewErrorMessage("The model '"+slot_data.model+"' couldnt be found.");
		else
			spriteRenderer.sprite = spriteAssigned;
		
		// Asign slot data to SlotDataAndControl script
		////AsignSlotData(slot_instance.GetComponent<SlotDataAndControl>(),slot_data);
		
		// Asign the canvas correspondent to the slot type
		//AsignCanvasFromType(slot_instance,slot_data);

		return slot_instance;
	}
}

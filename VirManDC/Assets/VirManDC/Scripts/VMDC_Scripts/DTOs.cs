using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace VMDC.Dtos
/*
	Here are the DTO's classes used by the program to access data. These are used
	to avoid the direct knowlegde of the API response system or the XML reading process,
	therefore giving a layer of abstraction.
*/
{

	public class ArchitectureRawData
	/* 
		This class should be used only to read from XML.
		To use data for the others scripts, use other classes
		like SlotDataAndControl 
	*/
	{
		public ArchitectureRawData() {
			racks = new List<RackDto>();
			modelsRack = new List<RackModelData>();
			others = new List<OtherDto>();
		}
		public float scalePosX;
        public float scaleX;
        public float scalePosZ;
        public float scaleZ;
		public float rotPosX;
		public float rotPosZ;
        public CameraSettings cameraSettings;
		public List<RackDto> racks;
		public List<RackModelData> modelsRack;
		public List<OtherDto> others;
	}
	
	public class CameraSettings
	{
		public float posX;
		public float posY;
		public float posZ;
		public float rotation;
	}
	
	public class OtherDto 
	{
		public int otherID;
		public string name;
		public string model;
		public string graphics;
		public float posX;
		public float posY;
		public float posZ;
		public float rotation;
	}
	
	public class RackDto 
	/*
		This class contains the info of a rack
	*/
	{
		public RackDto() {
			rackSlots = new List<RackSlotDto>();
		}
		public int rackID;
		public string name;
		public string model;
		public string modelB;
		public string graphicsM;
		public string graphicsBM;
		public float posX;
		public float posY;
		public float posZ;
		public float rotation;
		public List<RackSlotDto> rackSlots;
	}

	public class RackSlotDto
	/*
		This class contains the info of a rack slot
	*/
	{

		public int slotNum;
		public string name;
		public string slotID; // Can be IP or hostID
		
		public int size;
		public string type;
		public string model;
		public int posY;
        public bool isHypervisor;
    }
	
	public class RackModelData
	/*
		This class contains the info of a rack model
	*/
	{
		public string model;
		public float espacioInicioServers1u;
		public float espacioInicioCanvas1u;
		public float espacio1uY;
		public int size;
		public float espacioRackX; // ?
		public float espacioRackZ; // ?
	}
	
	
	public class DataApiSchema
	/*
		Container for the list of API Schema,
		one for each semaforo
	*/
	{
		public DataApiSchema() {
			apiSchemaList = new List<ApiSchema>();
		}
		public List<ApiSchema> apiSchemaList;
	}
	
	public class ApiSchema
	{
		public ApiSchema(string app, KeyModel key) {
			application = app;
			keyModel = key;
		}
		public string application;
		public KeyModel keyModel;
	}
	
	public class DataApiContainer
	/*
		Container for the API Data
	*/
	{
		public DataApiContainer() {
			this.appDataList = new List<List<InfoApi>>();
			this.keyDataList = new List<KeyData>();
		}
		// Add more if necessary
		public List<List<InfoApi>> appDataList;
		public List<KeyData> keyDataList;
		//public KeyValues keyValues;
	}
	
	public class KeyData
	{
		public KeyData (string value, KeyModel model)
		{
			keyValue = value;
			keyModel = model;
		}
		public string keyValue;
		public KeyModel keyModel;
	}
	
	public class KeyModel
	{
		public string id;
		public string key;
		public float topLimit;
	}
	
	public class InfoApi
	{
		public InfoApi(string key_, string name, string description, string lastValue)
		{
			this.key_ = key_;
			this.name = name;
			this.description = description;
			this.lastValue = lastValue;
		}
		
		public string key_;
		public string name;
		public string description;
		public string lastValue;
	}
	
	
	public class HostsZabbixData
	/*
		This class contains all the hosts being monitorized
		by the progam 
	*/
	{
		public HostsZabbixData() {
			listHosts = new List<HostZabbixData>();
		}
		public List<HostZabbixData> listHosts;
	}
	
	public class HostZabbixData {
		//public HostZabbixData() {
		//	vmlist = new List<VMZabbixData>();
		//}
		//public List<VMZabbixData> vmlist; // Virtual machines list
		public string hostID;
		public string hostname;
		public string hostIP;
		public string host;
		public string descriptionHost;
	}

	public class VMData {
		public string hostID;
		public string hostname;
		public bool isVmActive;
	}

	public class WarningLastData {
		public WarningLastData (string w, int p){
			warningDescription = w;
			priority = p;
		}
		public string warningDescription;
		public int priority;
	}
	
	public class IndicatorsPanelData {
		public IndicatorsPanelData() {
			listSemaforos = new List<SemaforoData>();
			schema = new DataApiSchema();
		}
		public string id;
		public string screenBackgroundColor;
		public string panelBackgroundColor;
		public List<SemaforoData> listSemaforos;
		public DataApiSchema schema;
	}
	
	public class SemaforoData {
		public SemaforoData(int id) {
			this.id = id;
		}
		public int id;
		public string name;
		public string application;
		public string key;
		public string imageName;
		public bool isVMButton;
	}

	public class RackBasicData {
		public RackBasicData(Transform t, string n){
            this.transform = t;
            this.rackName = n;
        }
        public Transform transform;
        public string rackName;
    }

	public class ZabbixScriptData {
        public int scriptid;
        public string scripshowname;
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class SlotDataFromAPI_Manager : MonoBehaviour
{
	/* 
		This class is used to get and store the data from the 
		Zabbix API through ApiZabbixPetitions.
	*/
	
	private ZabbixPetitions apiPetitions;
	private HostsZabbixData hostsData;
	private ErrorManager errorManager;
	
	void OnEnable()
	{
		errorManager = GameObject.FindWithTag("ErrorManager").GetComponent<ErrorManager>();
		apiPetitions = GameObject.FindGameObjectWithTag("ZabbixPetitions").GetComponent<ZabbixPetitions>();
	}
	
	public IEnumerator GetMainDataFromApi(DataApiSchema dataApiSchema, string hostID, Action<DataApiContainer> callback)
	{
		DataApiContainer returnedMainData = null;
		yield return StartCoroutine(apiPetitions.GetApiData(dataApiSchema,hostID,(DataApiContainer aux) => returnedMainData=aux));
		callback(returnedMainData);
		//return apiPetitions.GetApiData(dataApiSchema,hostID);	
	}
	
	public IEnumerator GetWarningsData(string hostID, Action<List<WarningLastData>> callback)
	{
		List<WarningLastData> returnedWarningData = null;
		yield return StartCoroutine(apiPetitions.WarningsPetition(hostID,(List<WarningLastData> aux) => returnedWarningData=aux));
		callback(returnedWarningData);
		//return apiPetitions.WarningsPetition(hostID);
	}
	
	public IEnumerator UpdateHostsData()
	/*
		This function gets the hosts data once we are logged in.
	*/
	{
		yield return StartCoroutine(apiPetitions.getHostsData((HostsZabbixData aux) => hostsData=aux));
		if (hostsData == null)
			errorManager.NewErrorMessage("ERROR: The HostsData couldnt be readed. Are you logged?");
	}
	
	public IEnumerator GetHostGroupID(string hostname,Action<string> callback)
	{
		string hostgroupID = "Default";
		yield return StartCoroutine(apiPetitions.getHostGroupID(hostname,(string aux) => hostgroupID=aux));
		callback(hostgroupID);
	}
	
	public IEnumerator GetVMsData(string hostgroupID, string key, Action<List<VMData>> callback)
	{
		List<VMData> vmList = new List<VMData>();
		if (hostgroupID!=null)
			yield return StartCoroutine(apiPetitions.getVMfromHostGroupID(hostgroupID, key,(List<VMData> aux) => vmList=aux));
		callback(vmList);
	}
	
	public void SetHostsDataToSlotData(SlotDataAndControl slotDaC, string hostID, string ip)
	/* 
		This function sets the dataHost parameters into the SlotDataAndControl
		component. Beware: you have to had called UpdateHostData at least once.
	*/
	{
		// If no hostsData is found (UpdateHostsData didnt called or check Architecture mode)
		if (hostsData == null) {
			errorManager.NewErrorMessage("ERROR: The HostsData couldnt be readed. Slot will be empty");
			slotDaC.checkSlotDemoMode = true;
		} else {
			// If the hostID is null, we search from IP. If not, we get the IP from the host
			if (hostID=="")
				GetHostIDFromIP(slotDaC,ip);
			else
				GetIPFromHostID(slotDaC,hostID);
			// If we didnt found a host for the IP, deactivate the slot
			if (slotDaC.hostID==""||slotDaC.ip=="")
			{
				slotDaC.slotIsDeactivated = true;
				errorManager.NewErrorMessage("ERROR: A host couldnt be found for this IP/hostID: '"+ip+hostID+"'. Make sure the IP/hostID is correct");
			}
		}
	}
	
	private void GetHostIDFromIP(SlotDataAndControl slotDaC, string ip)
	/*
		Search the IP in the list of hosts, and asign the data
	*/
	{
		// Note: IP must be unique
		//Debug.Log("Getting id from ip "+ip);
		foreach (HostZabbixData hostData in hostsData.listHosts)
		{
			if (hostData.hostIP == ip)
			{
				slotDaC.hostID = hostData.hostID;
				slotDaC.hostname = hostData.hostname;
				slotDaC.host = hostData.host;
				slotDaC.descriptionHost = hostData.descriptionHost;
				return;
			}
		}
	}
	
	private void GetIPFromHostID(SlotDataAndControl slotDaC, string hostID)
	{
		//Debug.Log("Getting ip from id "+hostID);
		foreach (HostZabbixData hostData in hostsData.listHosts)
		{
			if (hostData.hostID == hostID)
			{
				slotDaC.ip = hostData.hostIP;
				slotDaC.hostname = hostData.hostname;
				slotDaC.host = hostData.host;
				slotDaC.descriptionHost = hostData.descriptionHost;
				return;
			}
		}
	}
	
	public List<SlotDataAndControl> GetVirtualSlots()
	// WIP
	{
		// TODO, NOW IS A MOCK
		/*
		List<SlotDataAndControl> virtualSlotList = new List<SlotDataAndControl>();
		
		SlotDataAndControl slotvirtual_1 = new SlotDataAndControl();
		SlotDataAndControl slotvirtual_2 = new SlotDataAndControl();
		SlotDataAndControl slotvirtual_3 = new SlotDataAndControl();
		slotvirtual_1.slotName = "slotVirtual_1";
		slotvirtual_2.slotName = "CerebroMock_2";
		slotvirtual_3.slotName = "LELELOE_3";
		DataApiContainer data1 = new DataApiContainer();
		List<InfoApi> items1 = new List<InfoApi>();
		for (int i = 0; i<10; i++)
			items1.Add(new InfoApi("key1","name","looooooooooooooool","10"));
		data1.generalItems = items1;
		KeyValues keyValues = new KeyValues();
		keyValues.generalValue = new KeyValue("10",100f);
		//keyValues.generalKey = "lol";
		data1.keyValues = keyValues;
		slotvirtual_1.dataApiContainer = data1;
		virtualSlotList.Add(slotvirtual_1);
		virtualSlotList.Add(slotvirtual_2);
		virtualSlotList.Add(slotvirtual_3);
		return virtualSlotList;
		*/
		return null;
	}
}

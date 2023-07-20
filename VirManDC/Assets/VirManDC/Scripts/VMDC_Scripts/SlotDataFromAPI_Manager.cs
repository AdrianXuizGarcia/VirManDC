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
	[SerializeField]
	private ZabbixPetitions apiPetitions;
    private BehaviourSlotController[] listBehaviour;

    public HostsZabbixData hostsData;
	//private ErrorManager errorManager;
	
	void OnEnable()
	{
		//errorManager = GameObject.FindWithTag("ErrorManager").GetComponent<ErrorManager>();
		//apiPetitions = GameObject.FindGameObjectWithTag("ZabbixPetitions").GetComponent<ZabbixPetitions>();
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

	// TODO
	/*public IEnumerator GetAllWarningsData(string hostID, Action<List<WarningLastData>> callback)
	{
		List<WarningLastData> returnedWarningData = null;
		yield return StartCoroutine(apiPetitions.AllWarningsPetition(hostID,(List<WarningLastData> aux) => returnedWarningData=aux));
		callback(returnedWarningData);
		//return apiPetitions.WarningsPetition(hostID);
	}*/
	public void GetAllWarningsData()
	{
		StartCoroutine(GetAllWarningsData_Co());
	}

	//TODO: cambiar por lista de slots referenciados, en vez de find global?
	public IEnumerator GetAllWarningsData_Co(){
		//Debug.Log("searching for behaviours...");
		yield return StartCoroutine(GetListBehaviour());
        //Debug.Log("Founded! now updating...");
		foreach(BehaviourSlotController controller in listBehaviour){
            controller.UpdateWarningData();
        }
    }

	private IEnumerator GetListBehaviour(){
		listBehaviour = GameObject.FindObjectsOfType<BehaviourSlotController>();
		yield return null;
	}
	

	public IEnumerator MakeApiVersionPetition(Action<string> callback){
        string response = "";
        yield return StartCoroutine(apiPetitions.MakeApiVersionPetition((string aux) => response=aux));
        //Debug.Log(log);
        callback(response);
    }

	
	public IEnumerator UpdateHostsData()
	/*
		This function gets the hosts data once we are logged in.
	*/
	{
		yield return StartCoroutine(apiPetitions.getHostsData((HostsZabbixData aux) => hostsData=aux));
		if (hostsData == null)
			ErrorManager.NewErrorMessage("ERROR: The HostsData couldnt be readed. Are you logged?");
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

	public IEnumerator ExecuteScriptOnHostID(string hostid, int serverid,Action<string> callback)
	{
		string response = "";
		yield return StartCoroutine(apiPetitions.executeScriptOnHost(hostid,serverid,(string aux) => response=aux));
		if (response is null)
            ErrorManager.NewErrorMessage("ERROR: The instruction couldnt be executed");
        callback(response);
	}
	
	/// <summary>
	/// This function sets the dataHost parameters into the SlotDataAndControl
	///	component. Beware: you have to had called UpdateHostsData at least once.
    /// </summary>
    /// <param name="slotData"></param>
    /// <param name="slotControl"></param>
	public void SetHostsDataToSlotData(SlotData slotData, SlotControl slotControl)
	{
		// If no hostsData is found (UpdateHostsData wasnt called or check Architecture mode)
		if (hostsData == null) {
			ErrorManager.NewErrorMessage("ERROR: The HostsData couldnt be readed. Slot will be empty");
			slotControl.slotIsDeactivated = true;
		} else {
			bool found = GetHostIDorIPFromSlotID(slotData);
			// If we didnt found a host for the IP, deactivate the slot
			if (!found){
				slotControl.slotIsDeactivated = true;
				ErrorManager.NewErrorMessage("ERROR: A host couldnt be found for this IP/hostID: '"+slotData.slotID+"'. Make sure the IP/hostID is correct");
			}
		}
	}
	
	private bool GetHostIDorIPFromSlotID(SlotData slotData)
	/*
		Search the IP in the list of hosts, and asign the data
	*/
	{
        HostZabbixData foundHostData = null;
        // Assign first, then check 
        foreach (HostZabbixData hostData in hostsData.listHosts)
        {
            if (slotData.slotID == hostData.hostIP || slotData.slotID == hostData.hostID)
                foundHostData = hostData;
            //Debug.Log("Comparado '" + slotData.slotID + "' con '" + hostData.hostID + "/" + hostData.hostIP+":"+(foundHostData==null));
        }
		if (foundHostData!=null){
			slotData.hostID = foundHostData.hostID;
			slotData.ip = foundHostData.hostIP;
			slotData.hostname = foundHostData.hostname;
			slotData.host = foundHostData.host;
			slotData.descriptionHost = foundHostData.descriptionHost;
		}
		return (foundHostData!=null);
    }

}

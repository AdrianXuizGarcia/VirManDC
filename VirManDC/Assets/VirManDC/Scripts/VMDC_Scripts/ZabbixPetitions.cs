using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using VMDC.Dtos;
using VMDC.Constants;
using ResponseAPIClasses;
using VMDC.AuxiliarConfiguration;
using VMDC.JsonDtos;
using Newtonsoft.Json;

// This class contains all the petitions to the Zabbix API
public class ZabbixPetitions : MonoBehaviour
	{
		// DATA PETITIONS
		public IEnumerator GetApiData(DataApiSchema dataApiSchema,string hostID, Action<DataApiContainer> callback)
		{
			DataApiContainer dataContainer = new DataApiContainer();
			
			foreach (ApiSchema schema in dataApiSchema.apiSchemaList) {
				List<InfoApi> data = null;
				yield return StartCoroutine(GetApplicationData(schema.application,hostID,(List<InfoApi> aux) => data=aux));
				dataContainer.appDataList.Add(data);
				dataContainer.keyDataList.Add(GetKeyData(schema.keyModel,data));
			}
			callback(dataContainer);
		}
		
		// Auxiliar functions for GetData
		private IEnumerator GetApplicationData(string nameApplication, string hostID, Action<List<InfoApi>> callback) 
		{
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
            List<string> output = new List<string>();
            output.Add("key_");
			output.Add("name");
            output.Add("description");
            output.Add("lastvalue");
            dic.Add("output", output);
            dic.Add("hostids", hostID);
            dic.Add("application", nameApplication);
            Request r1 = new Request("item.get", dic, 1, ZabbixConfig.authKey);
			//Debug.Log(MakePetition(r1, ZabbixConfig.ipServer_S));
			// WIP
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));			
            ResponseItems values = JsonConvert.DeserializeObject<ResponseItems>(responseString);
			Debug.Log("Response: "+responseString);
        	//Debug.Log("LIst: "+values.result.ToString());
       		if (values.result.Count==0)
				Debug.Log("<color=red>ERROR</color>: No data returned by the Zabbix API for the application "+nameApplication+" on host "+hostID);
			callback(SetResponseToInfoApi(values.result));
		}
		
		private List<InfoApi> SetResponseToInfoApi(List<ResponseItem> dataList)
		{
			List<InfoApi> listInfoApi = new List<InfoApi>();
			foreach (ResponseItem data in dataList)
				//listInfoApi.Add(new InfoApi(data.key_,data.description,data.lastvalue));
				listInfoApi.Add(new InfoApi(data.key_,data.name,data.description,data.lastvalue));
			return listInfoApi;
		}
		
		// ICON DATA UPDATE
		private KeyData GetKeyData(KeyModel keyModel, List<InfoApi> dataList)
		{
			InfoApi data = dataList.Find(x => x.key_.Contains(keyModel.key));
			if (data==null){
				Debug.Log("ERROR: No data found for key '"+keyModel.id+"'. Check if the key is in the data application or if the data couldnt be loaded.");
				return new KeyData(null,keyModel);
			}
			return new KeyData(data.lastValue,keyModel);
        
    }
		
		// WARNING PETITIONS
		public IEnumerator WarningsPetition(string hostID,Action<List<WarningLastData>> callback)
		{
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			List<string> output = new List<string>();
			output.Add("description");
			output.Add("priority");
			output.Add("error");
			dic.Add("output", output);
			dic.Add("hostids", hostID);
			dic.Add("selectLastEvent", "extend");
			dic.Add("sortfield", "lastchange");
			dic.Add("monitored", "true");
			dic.Add("only_true", "");
			dic.Add("maintenance", "false");
			dic.Add("limit", "20");
			Request r1 = new Request("trigger.get", dic, 1, ZabbixConfig.authKey);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));
           Response values = JsonConvert.DeserializeObject<Response>(responseString);
			if (values.result.Count == 0)
			{
				//return new WarningLastData("",0);
				callback(null);
			}
			else
			{
				//string warningData = JsonUtility.ToJson(values.result,true);
				string warningData = JsonConvert.SerializeObject(values.result, Formatting.Indented);
				//Debug.Log(warningData);
				List<WarningData> listWarningData = JsonConvert.DeserializeObject<List<WarningData>>(warningData);
				//Debug.Log("Items: "+listWarningData.Count);
				//Debug.Log(JsonConvert.SerializeObject(listWarningData[0], Formatting.Indented));
				callback(DefineWarningDataFromResponse(listWarningData));
			}
		}
		
		private List<WarningLastData> DefineWarningDataFromResponse(List<WarningData> dataList)
		// TODO: check the diferences between priority and severity
		{
			//return new WarningLastData(data.lastEvent.name,data.priority);
			List<WarningLastData> lastDataList = new List<WarningLastData>();
			foreach(WarningData data in dataList)
				lastDataList.Add(new WarningLastData(data.lastEvent.name,data.priority));
			return lastDataList.OrderBy(o=>o.priority).ToList();
			//return lastDataList;
		}

		public IEnumerator AllWarningsPetition(string hostID,Action<List<WarningLastData>> callback)
		{
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			List<string> output = new List<string>();
			output.Add("hostid"); //?
			output.Add("description");
			output.Add("priority");
			output.Add("error");
			dic.Add("output", output);
			dic.Add("selectLastEvent", "extend");
			dic.Add("sortfield", "lastchange");
			dic.Add("monitored", "true");
			dic.Add("only_true", "");
			dic.Add("maintenance", "false");
			dic.Add("limit", "20");
			dic.Add("selectHosts", ""); //?
			
			Request r1 = new Request("trigger.get", dic, 1, ZabbixConfig.authKey);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux,true));
           Response values = JsonConvert.DeserializeObject<Response>(responseString);
			if (values.result.Count == 0)
			{
				//return new WarningLastData("",0);
				callback(null);
			}
			else
			{
				//string warningData = JsonUtility.ToJson(values.result,true);
				string warningData = JsonConvert.SerializeObject(values.result, Formatting.Indented);
				//Debug.Log(warningData);
				List<WarningData> listWarningData = JsonConvert.DeserializeObject<List<WarningData>>(warningData);
				//Debug.Log("Items: "+listWarningData.Count);
				//Debug.Log(JsonConvert.SerializeObject(listWarningData[0], Formatting.Indented));
				callback(DefineWarningDataFromResponse(listWarningData));
			}
		}
		
		
		// HOST DATA PETITION
		public IEnumerator getHostsData(Action<HostsZabbixData> callback)
		/*
			This function returns the HostsData, containing the list of
			all the hosts. Return null if not logged yet.
		*/
        {
            if (ZabbixConfig.authKey != null)
            {
				// Define and make the API Petition
                Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
                List<string> output = new List<string>();
                output.Add("hostid");
                output.Add("host");
				output.Add("name");
				output.Add("description");
                dic.Add("output", output);
                List<string> selectInterfaces = new List<string>();
                selectInterfaces.Add("interfaceid");
                selectInterfaces.Add("ip");
                dic.Add("selectInterfaces", selectInterfaces);
                Request r1 = new Request("host.get", dic, 1, ZabbixConfig.authKey);
				string responseString = "Default";
				yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));			
				ResponseIdHosts values = JsonConvert.DeserializeObject<ResponseIdHosts>(responseString);
				// For debugging
				//Debug.Log("Respuesta json de host.get: ");
				//Debug.Log(JsonConvert.SerializeObject(values, Formatting.Indented));
				// Once we have the response, set it to the HostsData class
				callback(DefineHostsDataFromResponse(values));
            } else {
				callback(null);
			}
        }
		
		private HostsZabbixData DefineHostsDataFromResponse(ResponseIdHosts hosts)
		/*
			This function gets the Data from the response and put it in a
			HostData.
		*/
		{
			HostsZabbixData hostsData = new HostsZabbixData();

            foreach (IdHostsData j in hosts.result)
            {
				HostZabbixData hostData = new HostZabbixData();
				hostData.hostID=j.hostid;
				hostData.hostname=j.name;
				hostData.host=j.host;
				hostData.descriptionHost=j.description;
				// REPASO: suponemos sólo una interfaz por host
                /*foreach (IdHostInterface k in j.interfaces)
                {
                    if (ip == k.ip)
                    {
                        slot_data.hostID = j.hostid;
                    }
                }*/
				//hostData.hostIP=CheckIfIsZabbixServerIP(j.interfaces[0].ip);
				hostData.hostIP=j.interfaces[0].ip;
				hostsData.listHosts.Add(hostData);
            }
			return hostsData;
        }
		
		private string CheckIfIsZabbixServerIP(string ip)
		/* 
			If the slot has the Zabbix server IP, it wont be find.
			This patch will fix that
		*/
		{
			if (ip == "127.0.0.1")
				return ZabbixConfig.ipServer;
			else
				return ip;
		}
		
		
		// BASIC VM DATA PETITION
		public IEnumerator getHostGroupID(string hostname, Action<string> callback)
		/*
			This function returns the HostGroupID of the hostname
		*/
        {
			// Define and make the API Petition
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			//List<string> output = new List<string>();
			//output.Add("groupid");
			//output.Add("host");
			//output.Add("name");
			//output.Add("description");
			dic.Add("output", "extend");
			Dictionary<string, string> name = new Dictionary<string, string>();
			name.Add("name",hostname);
			//filter.Add(name);
			//selectInterfaces.Add("ip");
			dic.Add("filter", name);
			Request r1 = new Request("hostgroup.get", dic, 1, ZabbixConfig.authKey);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));			
			ResponseHostGroup values = JsonConvert.DeserializeObject<ResponseHostGroup>(responseString);
			// For debugging
			//Debug.Log("Respuesta json de host.get: ");
			//Debug.Log(JsonConvert.SerializeObject(values, Formatting.Indented));
			// Once we have the response, send it back
			if (values.result.Count == 0)
				callback(null);
			else
				callback(values.result[0].groupid);
        }
		
		
		// CUSTOM SCRIPT PETITION
		public IEnumerator executeScriptOnHost(string hostid, int scriptid, Action<string> callback)
		/*
			This function returns the HostGroupID of the hostname
		*/
        {
			// Define and make the API Petition
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
            dic.Add("hostid", hostid);
            dic.Add("scriptid", scriptid);
            Request r1 = new Request("script.execute", dic, 1, ZabbixConfig.authKey);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));			
			ResponseScriptExecute values = JsonConvert.DeserializeObject<ResponseScriptExecute>(responseString);
			// For debugging
			Debug.Log("Respuesta json de script.execute con hostid "+hostid+" y scriptid "+scriptid+": ");
			Debug.Log(JsonConvert.SerializeObject(values, Formatting.Indented));
			// Once we have the response, send it back
			if (values.result is null)
				callback(null);
			else
				callback(values.result.value);
        }
		// VMS FROM HOST PETITION
		public IEnumerator getVMfromHostGroupID(string hostgroupId, string key, Action<List<VMData>> callback)
		/*
			This function returns the HostsData, containing the list of
			all the hosts. Return null if not logged yet.
		*/
        {
			// Define and make the API Petition
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			List<string> output = new List<string>();
			output.Add("hostid");
			output.Add("host");
			output.Add("name");
			output.Add("description");
			dic.Add("output", output);
			dic.Add("groupids", hostgroupId);
			Request r1 = new Request("host.get", dic, 1, ZabbixConfig.authKey);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));			
			ResponseIdHosts values = JsonConvert.DeserializeObject<ResponseIdHosts>(responseString);
			// For debugging
			//Debug.Log("Respuesta json de host.get: ");
			//Debug.Log(JsonConvert.SerializeObject(values, Formatting.Indented));
			// Once we have the response, set it to the VMData class
			List<VMData> listVM = null;
			yield return StartCoroutine(DefineVMFromResponse(values,key,(List<VMData> aux) => listVM=aux));
			callback(listVM);
			//callback(DefineVMFromResponse(values));
        }
		
		private IEnumerator DefineVMFromResponse(ResponseIdHosts hosts, string key, Action<List<VMData>> callback)
		{
			List<VMData> listVM = new List<VMData>();	
			foreach (IdHostsData j in hosts.result)
            {
				VMData vmData = new VMData();
				vmData.hostID=j.hostid;
				vmData.hostname=j.name;
				// To check if the virtual machine is being used:
				bool isActive = false;
				yield return StartCoroutine(GetVmCheckData(key,vmData.hostID,(bool aux) => isActive=aux));
				vmData.isVmActive = isActive;
				//(data.lastValue!=0);
				listVM.Add(vmData);
            }
			//return listVM;
			callback(listVM);
		}
		
		private IEnumerator GetVmCheckData(string key, string hostID, Action<bool> callback)
		{
			// Define and make the API Petition
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			List<string> output = new List<string>();
            output.Add("key_");
            output.Add("lastvalue");
            dic.Add("output", output);
            dic.Add("hostids", hostID);
            Dictionary<string, System.Object> search = new Dictionary<string, System.Object>();
			search.Add("key_", key);
			//selectInterfaces.Add("ip");
			dic.Add("search", search);
			Request r1 = new Request("item.get", dic, 1, ZabbixConfig.authKey);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux));			
			ResponseItems listItems =  JsonConvert.DeserializeObject<ResponseItems>(responseString);
			// For debugging
			//Debug.Log("Respuesta json de item.get: ");
			//Debug.Log(JsonConvert.SerializeObject(listItems, Formatting.Indented));
			
			callback(listItems.result[0].lastvalue!="0");
		}
		
		
		// LOG IN PETITIONS
		public IEnumerator MakeLogInPetition(string user, string password, Action<string> callback,bool showLog=false)
		/* 
			Return null if we cant get a response from the server.
			Return "" (empty string) if the response is not good.
			Return the response if its correct.
		*/
		{
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			dic.Add("user", user);
			dic.Add("password", password);
			Request r1 = new Request("user.login", dic, 1, null);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux,showLog));
			if (showLog)
				Debug.Log("Response:" + responseString);
			if (responseString=="")
				callback(null);
			else {
				string deserializedResponse = JsonConvert.DeserializeObject<ResponseLoggin>(responseString).result;
				if (showLog)
					Debug.Log("Response:" + deserializedResponse);
				if (deserializedResponse==null){
					ErrorManager.NewErrorMessage("The API Zabbix was reachable, but something went wrong.");
					ErrorManager.NewErrorMessage($"Detailed Error returned by API Zabbix: {responseString}");			
					callback("");
				}					
				else 
					callback(deserializedResponse);
				}
		}

		
		// API VERSION PETITION
		public IEnumerator MakeApiVersionPetition(Action<string> callback,bool showLog=false)
		/* 
			Return null if we cant get a response from the server.
			Return "" (empty string) if the response is not good.
			Return the response if its correct.
		*/
		{
			Dictionary<string, System.Object> dic = new Dictionary<string, System.Object>();
			Request r1 = new Request("apiinfo.version", dic, 1, null);
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(r1,(string aux) => responseString=aux,showLog));
			/*BasePetition bp = new BasePetition();
			bp.jsonrpc = "2.0";
			bp.method = "user.login";
			bp.@params = new LogInPetitionParams(user, password);
			bp.id = "1";
			//bp.auth = null;
			string responseString = "Default";
			yield return StartCoroutine(MakePetition(bp,(string aux) => responseString=aux));*/
			if (showLog)
				Debug.Log("Response:" + responseString);
			if (responseString=="")
				callback(null);
			else {
				string deserializedResponse = JsonConvert.DeserializeObject<ResponseLoggin>(responseString).result;
				if (showLog)
					Debug.Log("Response:" + deserializedResponse);
				if (deserializedResponse==null){
					ErrorManager.NewErrorMessage("The API Zabbix was reachable, but something went wrong.");
					ErrorManager.NewErrorMessage($"Detailed Error returned by API Zabbix: {responseString}");			
					callback("");
				}					
				else 
					callback(deserializedResponse);
				}
		}

		
		// MAIN METHOD
		public IEnumerator MakePetition(Request zbxRequest, Action<string> callback,bool showLog=false)
		// Return "" if an error occurred
		{
			// First we set the url 
			var url = "http://" + ZabbixConfig.ipServer + "/" + ZabbixConfig.urlZabbixAPI;
			//string jsonParams = JsonUtility.ToJson(zbxRequest);
			string jsonParams = JsonConvert.SerializeObject(zbxRequest);
			if (showLog)
				Debug.Log("Params:"+jsonParams);
			//Debug.Log("URL:"+url);
			
			// Make a new POST request with the params we have set
			var www = new UnityWebRequest(url, "POST");
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonParams);
			www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json-rpc");
			
			// Now wait for the request to be sent
			yield return www.SendWebRequest();
			
			// We send back the result, as success or as error
			if (www.result == UnityWebRequest.Result.Success){
				if (showLog)
					Debug.Log($"Success with conection: {www.downloadHandler.text}");
				callback(www.downloadHandler.text);
				yield return null;
			}
			else {
				callback(ManageErrorFromWebRequest(www));
            yield return null;
			}
		}
		
		// Auxiliar method to manage the error messages from the UnityWebRequest
		private string ManageErrorFromWebRequest(UnityWebRequest www){
			ErrorManager.NewErrorMessage("Problem trying to connect to API Zabbix");
			switch (www.result)
			{
				case UnityWebRequest.Result.ConnectionError:
					ErrorManager.NewErrorMessage("Failed to communicate with the server");
					break;
				case UnityWebRequest.Result.ProtocolError:
					ErrorManager.NewErrorMessage("The server returned an error response");
					break;
				case UnityWebRequest.Result.DataProcessingError:
					ErrorManager.NewErrorMessage("Error processing data");
					break;
				default:
					break;
			}
			ErrorManager.NewErrorMessage($"Detailed Error: {www.error}");			
			//callback(www.error);
			return "";
		}
	}
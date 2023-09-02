using System.Collections;
using System.Collections.Generic;
using AuxiliarMethods;
using TMPro;
using UnityEngine;
using VMDC.Dtos;

public class ScriptListPanelController : MonoBehaviour
{
    private SlotDataFromAPI_Manager slotDataFromAPI_Manager;
    private List<ZabbixScriptData> zabbixScriptList;
    private SlotData slotDataReference;
    public TextMeshProUGUI outputText;
    public GameObject prefabScriptElement;
    public GameObject baseToSpawnPrefabTest;

    void Start()
    {
        DeactivateAnyChilds();
        slotDataFromAPI_Manager = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>();
        slotDataReference = this.GetComponentInParent<SlotComponentsReferences>().GetSlotDataReference();
        zabbixScriptList = XML_extraMethods.LoadZabbixScriptList();
        StartCoroutine(SpawnScriptElementList());
        outputText.text = "";
    }

    private void DeactivateAnyChilds(){
        for (int i = 0; i < baseToSpawnPrefabTest.transform.childCount; i++){
            baseToSpawnPrefabTest.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnScriptElementList(){
        foreach(ZabbixScriptData element in zabbixScriptList)
        {
            GameObject elementInstance = Instantiate(prefabScriptElement, baseToSpawnPrefabTest.transform);
            elementInstance.GetComponentInChildren<ScriptElementController>().SetValues(
                element.scripshowname,
                element.scriptid,
                slotDataReference.hostID
            );
            }
        yield return null;

    }
    /// <summary>
    /// Called from ScriptElementController
    /// </summary>
    /// <param name="hostid"></param>
    /// <param name="scriptid"></param>
    public IEnumerator NewScriptExecution_Co(string hostid, int scriptid){
        
        string response = "";
        yield return StartCoroutine(slotDataFromAPI_Manager.ExecuteScriptOnHostID(hostid,scriptid,(string aux) => response=aux));
        if (response is null)
            outputText.text = "ERROR: The instruction couldnt be executed";
        else
            outputText.text = response;

    }
}

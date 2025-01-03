// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController_MainScene : MonoBehaviour
{

    public bool callForArchitecture = true;
    public bool callForZabbixData = true;
    public SlotDataFromAPI_Manager slotDFAM;

    public ArchitectureGeneralManager architectureGeneralManager;
    public AllWarningManager allWarningManager;

    void Start()
    {
        //guiSlotServerTest.SetActive(false);
        //guiSlotServerTest.transform.Rotate(0.0f, rotationGui, 0.0f);
        if (architectureGeneralManager && callForArchitecture) {
            architectureGeneralManager.Initialize();
            architectureGeneralManager.SetObjectsFromData();
        }
        //if (slotDFAM && callForZabbixData) {
        if (!StaticDataHolder.architectureMode) {
            //Debug.Log("es " + StaticDataHolder.architectureMode);
            StartCoroutine(InitHostsDataCoroutine());
            //StartCoroutine(InitScriptCoroutine("10829",1)); // For testing
            //StartCoroutine(InitScriptCoroutine("10829",4)); // For testing
            //StartCoroutine(InitScriptCoroutine("10829",3)); // For testing
            //StartCoroutine(MakeApiVersionPetitionCo());
        }
    }

    private IEnumerator InitHostsDataCoroutine(){
        yield return StartCoroutine(slotDFAM.UpdateHostsData());
        allWarningManager.GetAllWarningsData();
        //Debug.Log("Data updated!");
        //Debug.Log(slotDFAM.hostsData);
    }

    //? For debugging 
    private IEnumerator InitScriptCoroutine(string hostid, int scriptid){
        string response = "";
        yield return StartCoroutine(slotDFAM.ExecuteScriptOnHostID(hostid,scriptid,(string aux) => response=aux));
        Debug.Log("Response at end: "+response);
        //Debug.Log(slotDFAM.hostsData);
    }
    /*
    private IEnumerator MakeApiVersionPetitionCo(){
        yield return StartCoroutine(slotDFAM.MakeApiVersionPetition());
        //Debug.Log("Data updated!");
        //Debug.Log(slotDFAM.hostsData);
    }
    */

}

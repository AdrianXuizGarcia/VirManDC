using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject selectRack;
    public GameObject guiSlot;

    public GameObject guiSlotServerTest;
    public float rotationGui = 290;

    public bool callForArchitecture = true;
    public bool callForZabbixData = true;
    public SlotDataFromAPI_Manager slotDFAM;

    public ArchitectureGeneralManager architectureGeneralManager;

    void Start()
    {
        selectRack.SetActive(false);
        //guiSlot.SetActive(false);

        //guiSlotServerTest.SetActive(false);
        //guiSlotServerTest.transform.Rotate(0.0f, rotationGui, 0.0f);
        if (architectureGeneralManager && callForArchitecture) {
            architectureGeneralManager.Initialize();
            architectureGeneralManager.SetObjectsFromData();
        }
        //if (slotDFAM && callForZabbixData) {
        if (!StaticDataHolder.architectureMode) {
            //Debug.Log("es " + StaticDataHolder.architectureMode);
            StartCoroutine(InitDataCoroutine());
        }
    }

    private IEnumerator InitDataCoroutine(){
        yield return StartCoroutine(slotDFAM.UpdateHostsData());
        //Debug.Log("Data updated!");
        //Debug.Log(slotDFAM.hostsData);
    }

    

}

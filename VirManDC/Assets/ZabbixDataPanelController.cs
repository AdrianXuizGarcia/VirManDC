using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZabbixDataPanelController : MonoBehaviour
{
    public GameObject panelGameobject;
    public GameObject prefabTest;
    public GameObject baseToSpawnPrefabTest;
    public int numberOfInstances = 15;
    //private bool panelIsOpen = false;

    /*public void InitializeData(){
        UpdateData();
    }

    public void UpdateData(data) {
        DeleteDataObjects();
        SpawnDataObjectsFromData(data);
        ShowOnlyActiveButton(0 by default);


        /*for(int i = 0; i < numberOfInstances; i++)
        {
            int j = i * 10;
            Instantiate(prefabTest, baseToSpawnPrefabTest.transform);
            prefabTest.GetComponent<DataPanelElementController>().SetValues("Hola " + i, "Values for indicator", j.ToString());
        }
        panelGameobject.SetActive(panelIsOpen);*
    }

    public void SwapPanelState(){
        panelGameobject.SetActive(!panelIsOpen);
        panelIsOpen = !panelIsOpen;
    }
    private void DeleteDataObjects(){
        Empty
    }*/

}

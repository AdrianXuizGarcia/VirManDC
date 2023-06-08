using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZabbixDataPanelController : MonoBehaviour
{
    public GameObject panelGameobject;
    public GameObject prefabTest;
    public GameObject baseToSpawnPrefabTest;
    public int numberOfInstances = 15;
    private bool panelIsOpen = false;

    void Start() {
        for(int i = 0; i < numberOfInstances; i++)
        {
            Instantiate(prefabTest, baseToSpawnPrefabTest.transform);
        }
        panelGameobject.SetActive(panelIsOpen);
    }

    public void SwapPanelState(){
        panelGameobject.SetActive(!panelIsOpen);
        /*if (panelIsOpen)
            SetButton_ReadyToClose();
        else 
            SetButton_ReadyToOpen();*/
        panelIsOpen = !panelIsOpen;
    }
}

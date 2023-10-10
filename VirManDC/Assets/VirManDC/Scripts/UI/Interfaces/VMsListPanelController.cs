// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Dtos;

public class VMsListPanelController : MonoBehaviour
{
    public GameObject panelGameobject;
    public GameObject prefabTest;
    public GameObject baseToSpawnPrefabTest;
    public int limitOfElementsToBeShown = 15;
    private List<GameObject> listVmsElement = new List<GameObject>();
    private bool panelIsOpen = false;

    void Start() {
        DeactivateAnyChilds();
        panelGameobject.SetActive(panelIsOpen);
    }

    public IEnumerator Init(List<VMData> listOfElements) {
        //DeactivateAnyChilds();
        ResetUIScroll();
        DeleteAllDataObjects();
        int ElementsBeingShown = 0;
        for (int i = 0; i < listOfElements.Count; i++)
        {
            GameObject elementInstance = Instantiate(prefabTest, baseToSpawnPrefabTest.transform);
            listVmsElement.Add(elementInstance);
            elementInstance.GetComponentInChildren<VMButtonController>().SetValues(
                    TruncateString(listOfElements[i].hostname, 50)
                    ,listOfElements[i].hostID
                    ,listOfElements[i].isVmActive
                    //valueParsed
                    );
            if (ElementsBeingShown >= limitOfElementsToBeShown)
                break;
        }
        panelGameobject.SetActive(panelIsOpen);
        yield return null;
    }

        public void SwapPanelState(){
        panelGameobject.SetActive(!panelIsOpen);
        panelIsOpen = !panelIsOpen;
    }

    private void DeactivateAnyChilds(){
        for (int i = 0; i < baseToSpawnPrefabTest.transform.childCount; i++){
            baseToSpawnPrefabTest.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void DeleteAllDataObjects(){
        for (int i = 0; i < listVmsElement.Count; i++){
            DeleteDataObjects(i);
        }
    }

    private void DeleteDataObjects(int page){
        foreach(GameObject element in listVmsElement)
            Destroy(element);
        listVmsElement.Clear();
    }

    private void ResetUIScroll(){
        ScrollRect temp = baseToSpawnPrefabTest.GetComponentInParent<ScrollRect>();
        if(temp)
            temp.verticalNormalizedPosition = 1f; // Patch to reset Scroll UI
    }

    /// <summary>
    /// Patch because Text Mesh Pro UI dont work correctly in this scenario
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="truncationSuffix"></param>
    /// <returns></returns>
    public string TruncateString(string value, int maxLength, string truncationSuffix = "…"){
        return value.Length > maxLength 
            ? value.Substring(0, maxLength) + truncationSuffix 
            : value;
    }

}

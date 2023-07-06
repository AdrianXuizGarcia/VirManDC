using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Dtos;

public class TestDataPanelController : MonoBehaviour
{
    // TODO: Its a test controller
    public GameObject panelGameobject;
    public GameObject prefabTest;
    public GameObject baseToSpawnPrefabTest;
    private List<List<GameObject>> listsDataElement = new List<List<GameObject>>();
    public int numberOfInstances = 15;
    private bool panelIsOpen = false;
    private int actualIndicatorPage = 0;
    private int numOfPages = -1;

    //? Delete later
    /*void Start(){
    List<List<GameObject>> listsDataElementTemp = new List<List<GameObject>>();
        listsDataElementTemp.Add(new List<GameObject>());
        listsDataElementTemp.Add(new List<GameObject>());
        listsDataElementTemp.Add(new List<GameObject>());
        Init(listsDataElementTemp);
    }*/

    public IEnumerator Init(List<List<InfoApi>> listOfElements) {
        DeactivateAnyChilds();
        ResetUIScroll();
        DeleteAllDataObjects();
        //listsDataElement = listOfElements; // TODO: Instead of gameobjects, data file
        numOfPages = listOfElements.Count;
        for (int i = 0; i < numOfPages; i++)
        {
            listsDataElement.Add(new List<GameObject>());
            for (int j = 0; j < listOfElements[i].Count; j++)
            {
                int k = j * 10;
                GameObject elementInstance = Instantiate(prefabTest, baseToSpawnPrefabTest.transform);
                listsDataElement[i].Add(elementInstance);
                //elementInstance.GetComponent<DataPanelElementController>().SetValues("Hola " + i, "Values for indicator", k.ToString());
                string valueParsed = listOfElements[i][j].lastValue;
                if (float.TryParse(valueParsed, out _))
					valueParsed = Math.Round(float.Parse(listOfElements[i][j].lastValue, CultureInfo.InvariantCulture),2).ToString();
                elementInstance.GetComponent<DataPanelElementController>().SetValues(
                    TruncateString(listOfElements[i][j].name,50), 
                    TruncateString(listOfElements[i][j].description,100), 
                    valueParsed
                    );
            }
        }
        panelGameobject.SetActive(panelIsOpen);
        SwapStateAllPageElements(false);
        SwapStatePageElements(actualIndicatorPage,true);
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
        for (int i = 0; i < listsDataElement.Count; i++){
            DeleteDataObjects(i);
        }
    }

    private void DeleteDataObjects(int page){
        foreach(GameObject element in listsDataElement[page])
            Destroy(element);
        listsDataElement[page].Clear();
    }

    public void NextPage(){
        int actualIndicatorPageTmp = actualIndicatorPage + 1;
        if (actualIndicatorPageTmp>=numOfPages)
            actualIndicatorPageTmp = 0;
        SwapPage(actualIndicatorPageTmp);
    }

    public void SwapPage(int newPage){
        if (!panelIsOpen)
            SwapPanelState();
        ResetUIScroll();
        //DeleteDataObjects(actualIndicatorPage);
        SwapStatePageElements(actualIndicatorPage, false);
        actualIndicatorPage = newPage;
        SwapStatePageElements(actualIndicatorPage, true);
    }

    private void SwapStatePageElements(int page, bool state){
        foreach(GameObject element in listsDataElement[page])
            element.SetActive(state);
    }

    private void SwapStateAllPageElements(bool state){
        for (int i = 0; i < listsDataElement.Count; i++){
            SwapStatePageElements(i,state);
        }
    }

    private void ResetUIScroll(){
        if(baseToSpawnPrefabTest)
            baseToSpawnPrefabTest.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 1f; // Patch to reset Scroll UI
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
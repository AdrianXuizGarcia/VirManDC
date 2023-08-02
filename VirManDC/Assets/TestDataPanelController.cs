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
    public int limitOfElementsToBeShown = 15;
    private List<List<GameObject>> listsDataElement = new List<List<GameObject>>();
    private bool panelIsOpen = false;
    private int actualIndicatorPage = 0;
    private int numOfPages = -1;

    public IEnumerator Init(List<List<InfoApi>> listOfElements) {
        DeactivateAnyChilds();
        ResetUIScroll();
        DeleteAllDataObjects();
        numOfPages = listOfElements.Count;
        for (int i = 0; i < numOfPages; i++)
        {
            int ElementsBeingShown = 0;
            listsDataElement.Add(new List<GameObject>());
            for (int j = 0; j < listOfElements[i].Count; j++)
            {
                GameObject elementInstance = Instantiate(prefabTest, baseToSpawnPrefabTest.transform);
                ElementsBeingShown += 1;
                listsDataElement[i].Add(elementInstance);
                string valueParsed = listOfElements[i][j].lastValue;
                if (float.TryParse(valueParsed, out _))
					valueParsed = Math.Round(float.Parse(listOfElements[i][j].lastValue, CultureInfo.InvariantCulture),2).ToString();
                elementInstance.GetComponent<DataPanelElementController>().SetValues(
                    TruncateString(listOfElements[i][j].name,50), 
                    TruncateString(listOfElements[i][j].description,100), 
                    valueParsed
                    );
                if (ElementsBeingShown >= limitOfElementsToBeShown)
                    break;
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
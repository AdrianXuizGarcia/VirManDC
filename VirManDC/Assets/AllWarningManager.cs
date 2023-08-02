using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWarningManager : MonoBehaviour
{
    private SlotDataFromAPI_Manager slotDataFromAPI_Manager;
    // Start is called before the first frame update
    void Start()
    {
      slotDataFromAPI_Manager = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>(); 
    }

    public void GetAllWarningsData()
    {
        slotDataFromAPI_Manager.GetAllWarningsData();
    }
    
}

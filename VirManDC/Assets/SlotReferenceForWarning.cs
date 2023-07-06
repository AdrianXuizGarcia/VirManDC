using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotReferenceForWarning : MonoBehaviour
{
  //! DEPRECATED
    private SlotDataFromAPI_Manager slotDataFromAPI_Manager;
    // Start is called before the first frame update
    void Start()
    {
      slotDataFromAPI_Manager = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>(); 
    }

    public void GetAllWarningsData()
    {
        Debug.Log("calling slot api");
        slotDataFromAPI_Manager.GetAllWarningsData();
    }
    
}

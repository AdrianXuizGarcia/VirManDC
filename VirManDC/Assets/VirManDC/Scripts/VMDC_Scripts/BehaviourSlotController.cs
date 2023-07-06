using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class BehaviourSlotController : MonoBehaviour
{
    private SlotDataFromAPI_Manager slotDFAM;
    public WarningController warningController;
    public LoadingButtonsUntilReadyController loadingButtonsUntilReadyController;
    public SlotData slotDataReference;
    public SlotControl slotControlReference;

    public IndicatorPanelController indicatorPanelControllerReference;
    public TestDataPanelController testDataPanelController;


    //TODO: Do a nice call, this executes all at once at first
    public void Start(){
        if (!slotDFAM)
            slotDFAM = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>();
        StartCoroutine(Initialize_Co());
        //zabbixDataPanelController.AsignSlotData(slotDataReference);
    }

    private IEnumerator Initialize_Co(){
        yield return new WaitUntil(() => StaticDataHolder.IndicatorPanelManagerIsInitialized);
        //Debug.Log(StaticDataHolder.IndicatorPanelManagerIsInitialized);
        Initialize();
    }


    private void Initialize()
    {
        // First time, so we read the semaforos values and save the application and keys configuration
       slotDataReference.dataApiSchema = indicatorPanelControllerReference.StartPanelAndSaveApiSchema(slotDataReference.type);
    }

    // WARNING DATA

    /// <summary>
    /// Can be called from gameobjects on playmode, but usually this function is referenced
    /// on the SlotComponentsReferences
    /// </summary>
    public void UpdateWarningData(){
        loadingButtonsUntilReadyController.OnStartNewDataPetition();
        slotDFAM.SetHostsDataToSlotData(slotDataReference, slotControlReference);
        //Debug.Log("Updating for host " + slotData.hostID+"...");
        StartCoroutine(UpdateWarningDataCoroutine());    
    }

    private IEnumerator UpdateWarningDataCoroutine(){
        List<WarningLastData> warningData = null;
        yield return slotDFAM.GetWarningsData(slotDataReference.hostID, (List<WarningLastData> aux) => warningData = aux);
        // Once is over, inform and check if there is a new warning //TODO: Only do warnings
        //NotificationManager.HostsWarningDataHasBeenRetrieved(true);
        if (warningData != null)
        {
            //Debug.Log("Warning found for host " + slotData.hostID);
            warningController.UpdateWarningData(warningData);
        }
        //Debug.Log("Warning searched for host " + slotDataReference.hostID);
        //NotificationManager.HostsDataHasBeenRetrieved(true);
        loadingButtonsUntilReadyController.OnDataRetrieved(true);
    }

    // MAIN DATA
	public void UpdateMainData()
	{
		StartCoroutine(UpdateDataCoroutine());
	}

    public IEnumerator UpdateDataCoroutine()
	/*
		Get the data from the API trough SlotDataFromAPI_Manager,
		and update it to be used by the canvas elements.
	*/
	{
		// TODO Call the update VM data only once, because the VM's doesnt change on execution time (shouldnt do)
		//if (!vmUpdated)
		//	yield return StartCoroutine(UpdateVMsCoroutine());
		slotDataReference.dataApiContainer = null;
        loadingButtonsUntilReadyController.OnStartNewDataPetition();
        //loadingIcon.SetActive(true);
		Debug.Log("Waiting for data...");
		// Can be null, if for example we have only VM
		if (slotDataReference.dataApiSchema!=null)
			yield return StartCoroutine(slotDFAM.GetMainDataFromApi(slotDataReference.dataApiSchema,slotDataReference.hostID, (DataApiContainer aux)=>slotDataReference.dataApiContainer=aux));
        //TODO: What happens if null
		Debug.Log("Waiting for semaforos update...");
		yield return StartCoroutine(indicatorPanelControllerReference.UpdateDataButtons(slotDataReference.dataApiContainer.keyDataList));
        Debug.Log("Waiting for data panel update...");
        yield return StartCoroutine(testDataPanelController.Init(slotDataReference.dataApiContainer.appDataList));
        Debug.Log("Data retrieved!");
        //TODO Check this:
        loadingButtonsUntilReadyController.OnDataRetrieved(true);
		
		// To inform the animation that this job is finished
		//doneWithUpdating = true;	
	}

}

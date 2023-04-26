using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VMDC.Dtos;
using VMDC.Constants;

public class SlotRackInteraction : MonoBehaviour
{
	private SlotDataAndControl slotDaC;
	private SlotDataFromAPI_Manager slotDFAM;
	
	private string originalHostID;
	private string originalHostname;
	private DataApiSchema originalDataApiSchema;
	private DataApiSchema vmDataApiSchema;
	
	public GameObject slotHoverInfo;
    public Text slotInfo;
	public WarningController warningController;
	public Text titleHeader;
	private MoveSlotAnimation slotAnimation;
	
	private bool slotIsClosed = true;
	private bool newdata = true;
	public GameObject mainCanvasPanel;
	public GameObject loadingIcon;
	public GameObject auxCanvas;
	private Component[] dataButtons;
	public Behaviour slotHalo;
	
	// WIP -> Mejor 2 distintos, o tener la misma e ir activando y desactivando mediante un controlador? Mejor eficiencia
	//public GameObject indicatorPanelControllerObject;
	//private List<ScriptSemaforo> scriptsSemaforos = new List<ScriptSemaforo>();
	public IndicatorPanelController indicatorPanelController;
	
	private bool vmUpdated = false;
	private bool vmNotInitialized = false;
	
	private bool doneWithUpdating = false;
	
	void Start()
    {
		StartCoroutine("StartSlot");
    }
	
	private IEnumerator StartSlot()
	{
		// Asigment of the slots Data components
		slotDaC = GetComponentInParent<SlotDataAndControl>();
		slotDFAM = GameObject.FindWithTag("API_Controller").GetComponent<SlotDataFromAPI_Manager>();
		originalHostID = slotDaC.hostID;
		originalHostname = slotDaC.hostname;
		
		if (!slotDaC.slotIsDeactivated)
		{
			// Ask for Warnings -- WIP
			if (slotDaC.isRefreshing)
				InvokeRepeating("UpdateWarnings", Random.Range(5.0f, 45.0f), Random.Range(40.0f,80.0f));
			//UpdateWarnings(); // for debugging, high perfomance costs
		} else 
			DeactivateSlotGraphics();
		yield return null;
	}
	
	void OnMouseDown()
	{
		// If first time we enter
		if (slotAnimation==null) {
			Initialize(slotDaC.type);
		}
		// Dont do anything if the slot is deactivated or we have UI active (like UI from another slot)
		if (!slotDaC.slotIsDeactivated && !EventSystem.current.IsPointerOverGameObject())
		{
			// Dont update data if we are closing the slot or is in Demo mode
			if (slotIsClosed && !slotDaC.checkSlotDemoMode){
				//Debug.Log("UPDATING DATA");
				UpdateAllData();
			} else {
				// Set back the original hostID when closing slot, in case
				// we changed to acces the VM
				slotDaC.hostID = originalHostID;
				slotDaC.hostname = originalHostname;
				slotDaC.dataApiSchema = originalDataApiSchema;
				//vmUpdated = false;
				indicatorPanelController.IsVmCanvas(false);
			}
			slotAnimation.DoAnimation(slotIsClosed);
			slotIsClosed=!slotIsClosed;
		}
    }
	
	private IEnumerator WaitAnimationUntilUpdateIsDone()
	{
		slotAnimation.DeactivatePanel();
		while(doneWithUpdating==false){
			//Debug.Log("Waiting");
			yield return null;
		}
		slotAnimation.ActivatePanel();
		doneWithUpdating = false;
		//Debug.Log("Done");
	}
	
	private void Initialize(string type){
		// Asigment of the canvas to use for the animation
		if (slotAnimation==null){
			slotAnimation = GetComponentInParent<MoveSlotAnimation>();
			slotAnimation.SetCanvasAnimation(mainCanvasPanel,auxCanvas);
		}
		
		bool isDataApiSchemaDefined = (slotDaC.dataApiSchema!=null);
		//Debug.Log("Defined: "+isDataApiSchemaDefined);
		
		// First time, so we read the semaforos values and save the application and keys configuration
		slotDaC.dataApiSchema = indicatorPanelController.StartPanelAndSaveApiSchema(type);
		
		if (!isDataApiSchemaDefined)
			originalDataApiSchema = slotDaC.dataApiSchema;
		
		if (type==VMDCTags.VIRTUAL_MACHINE_TAG)
			vmDataApiSchema = slotDaC.dataApiSchema;

		// If there was a problem with the initialization, we deactivate the slot
		if (slotDaC.dataApiSchema == null){
			slotDaC.slotIsDeactivated = true;
			DeactivateSlotGraphics();
		}
		
		// Make the list with the children objects of indicatorPanelControllerObject, in order to 
		// update their data later
		/*foreach (Transform child in indicatorPanelControllerObject.transform)
		{
			//Debug.Log(child.gameObject.name);
			ScriptSemaforo script = child.gameObject.GetComponent<ScriptSemaforo>();
			if (script!=null)
				scriptsSemaforos.Add(script);
		}*/
	}
	
	private void OnMouseEnter()
    {
        if (newdata)
        {
			slotInfo.text = slotDaC.name + "\n";
			if (!slotDaC.slotIsDeactivated) {
				slotInfo.text = slotInfo.text + "IP: " + slotDaC.ip;
				slotInfo.text = slotInfo.text + "\n Hostname: " + slotDaC.hostname;
				// Also update the title header
				titleHeader.text = slotDaC.hostname;
			} else
				slotInfo.text = slotInfo.text + " ERROR: No data found ";
            newdata = false;
        }
        slotHoverInfo.SetActive(true);
    }

    private void OnMouseExit()
    {
        slotHoverInfo.SetActive(false);
    }
	
	public void UpdateAllData()
	{
		StartCoroutine("WaitAnimationUntilUpdateIsDone");
		if (!slotDaC.slotIsDeactivated)
		{
			UpdateMainData();
			UpdateWarnings();
		}
	}
	
	public void UpdateMainData()
	{
		StartCoroutine("UpdateDataCoroutine");
	}
	
	public void UpdateWarnings()
	{
		StartCoroutine("UpdateWarningsCoroutine");
	}
	
	public IEnumerator UpdateDataCoroutine()
	/*
		Get the data from the API trough SlotDataFromAPI_Manager,
		and update it to be used by the canvas elements.
	*/
	{
		// Call the update VM data only once, because the VM's doesnt change on execution time (shouldnt do)
		if (!vmUpdated)
			yield return StartCoroutine("UpdateVMsCoroutine");
		slotDaC.dataApiContainer = null;
		//StartCoroutine("GetDataFromApiCoroutine");
		//auxCanvas.SetActive(false);
		loadingIcon.SetActive(true);
		//Debug.Log("Waiting for data...");
		// Can be null, if for example we have only VM
		if (slotDaC.dataApiSchema!=null)
			yield return StartCoroutine(slotDFAM.GetMainDataFromApi(slotDaC.dataApiSchema,slotDaC.hostID, (DataApiContainer aux)=>slotDaC.dataApiContainer=aux));
		//slotDaC.dataApiContainer = slotDFAM.GetMainDataFromApi(slotDaC.dataApiSchema,slotDaC.hostID);

		//Debug.Log("Waiting for semaforos update...");
		//auxCanvas.SetActive(true);
		//yield return StartCoroutine("UpdateDataButtons");
		yield return StartCoroutine(indicatorPanelController.UpdateDataButtons());
		//Debug.Log("Data retrieved!");
		
		loadingIcon.SetActive(false);
		
		// To inform the animation that this job is finished
		doneWithUpdating = true;	
	}
	
	/*private IEnumerator UpdateDataButtons()
	{
		foreach (ScriptSemaforo script in scriptsSemaforos)
		{
			//Debug.Log("READING");
			yield return StartCoroutine(script.UpdateDataButton());
		}
	}*/
	
	public IEnumerator UpdateWarningsCoroutine()
	{
		List<WarningLastData> warningData = null;
		yield return StartCoroutine(slotDFAM.GetWarningsData(slotDaC.hostID, (List<WarningLastData> aux)=>warningData=aux));
		//WarningLastData warningData = slotDFAM.GetWarningsData(slotDaC.hostID);
		if (warningData!=null)
			warningController.UpdateWarningData(warningData);
		
	}
	
	public IEnumerator UpdateVMsCoroutine()
	{
		yield return StartCoroutine(slotDFAM.GetHostGroupID(slotDaC.hostname, (string aux)=>slotDaC.hostGroupID=aux));
		yield return StartCoroutine(slotDFAM.GetVMsData(slotDaC.hostGroupID, GetVMKey(),(List<VMData> aux)=>slotDaC.virtualMachinesList=aux));
		vmUpdated = true;
	}
	
	private string GetVMKey()
	// WIP: It should be readed from the keyModel of the Virtual Schema
	{
		//return slotDaC.dataApiSchema.apiSchemaList[slotDaC.dataApiSchema.apiSchemaList.Count-1].keyModel.key;
		return "vmware.vm.cpu.usage[{$URL},{HOST.HOST}]";
	}
	
	public void EnterVM(string hostid, string vmHostname)
	/*
		WIP: When we click on a VM, we change the interface
	*/
	{
		if (!vmNotInitialized){
			Initialize(VMDCTags.VIRTUAL_MACHINE_TAG);
			vmNotInitialized = true;
		}
		slotDaC.hostID = hostid;
		slotDaC.hostname = vmHostname;
		slotDaC.dataApiSchema = vmDataApiSchema;
		UpdateAllData();
		indicatorPanelController.IsVmCanvas(true);
	}
	
	public GameObject GetPanelCanvas()
	// This is used to get the correct canvas in the ArchitectureObjectsManager
	{
		return mainCanvasPanel;
	}
	
	private void DeactivateSlotGraphics()
	{
		Color tempcolor = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
		tempcolor = Color.red;
		tempcolor.a = .2f;
		transform.GetChild(0).GetComponent<MeshRenderer>().material.color = tempcolor;
		
		tempcolor = transform.GetChild(1).GetComponent<SpriteRenderer>().material.color;
		tempcolor = Color.red;
		//tempcolor.a = .7f;
		transform.GetChild(1).GetComponent<SpriteRenderer>().material.color = tempcolor;
	}
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Constants;

public class VMButtonController : MonoBehaviour
{
    public GameObject prefabToInstance;
    public TextMeshProUGUI serverName;
    public Image imageButton;
    public Color newButtonColorIfVMActive;
    private bool isVmActive;
    private string hostId;

    public void InstanceVMInterface(){
        Transform pivotTransform = GameObject.FindGameObjectWithTag(VMDCTags.SPAWN_CAMERA_TAG).transform;
        GameObject ob = Instantiate(prefabToInstance, pivotTransform);
        ob.transform.parent = GameObject.FindGameObjectWithTag("PivotForRack").transform;
        // TODO: Need to also relaunch data server update, and instantiate indicators of vm
        //SlotData data = ob.GetComponentInChildren<SlotData>();
        //data.hostID = hostId;
    }

    public void SetValues (string serverName, string hostId, bool isVmActive)
    {
        this.serverName.text = serverName;
        this.isVmActive = isVmActive;
        this.hostId = hostId;
        AsignColorToImageButton();
    }
	
	public void AsignColorToImageButton(){
		//Debug.Log("Is active:"+IsVMActive);
		if(isVmActive)
			imageButton.color = newButtonColorIfVMActive;
	}

    
}
